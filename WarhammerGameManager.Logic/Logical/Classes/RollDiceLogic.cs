using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WarhammerGameManager.Entities.ApplicationModels;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.Contexts;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels;
using WarhammerGameManager.Entities.ViewModels;
using WarhammerGameManager.Logic.Logical.Interfaces;

namespace WarhammerGameManager.Logic.Logical.Classes
{
    public class RollDiceLogic : IRollDiceLogic
    {
        private readonly ILogger _logger;
        private readonly WarhammerNarrative_Context _context;
        private Random _rand;

        public RollDiceLogic(ILogger<RollDiceLogic> logger, WarhammerNarrative_Context context, Random random)
        {
            _logger = logger;
            _context = context;
            _rand = random;
        }

        public async Task<RollDiceViewModel> GetRollDiceViewModel()
        {
            try
            {
                var viewModel = new RollDiceViewModel();

                viewModel.RollTypes = await GetRollTypes();
                viewModel.GameRules = await GetGameRules();

                return viewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Issue getting Roll View Model!");
                return new RollDiceViewModel();
            }
        }

        public async Task<List<RollType>> GetRollTypes()
        {
            try
            {
                var data = await _context.RollTypes
                    .OrderBy(x => x.Name)
                    .ToListAsync();

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Issue getting roll types!");
                return new List<RollType>();
            }
        }

        public async Task<List<GameRule>> GetGameRules()
        {
            try
            {
                var data = await _context.GameRules
                    .Include(y => y.AppliesTo)
                    .OrderBy(x => x.Name)
                    .ToListAsync();

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Issue getting game rules!");
                return new List<GameRule>();
            }
        }

        public async Task<RollDiceBasicResponse> QuickRollDice(RollDiceBasicRequest request)
        {
            try
            {
                var diceEvent = new DiceEvent();
                var rolls = new List<DiceRoll>();

                var rollType = _context.RollTypes.Single(x => x.Id == request.RollTypeId);

                var diceRoll = (await RollDice(request.DiceCount))
                    .ConvertToDiceRoll(rollType, request.Threshold);
                rolls.AddRange(diceRoll);

                diceEvent.Rolls = rolls;

                return new RollDiceBasicResponse
                {
                    Request = request,
                    Results = diceEvent,
                    RollType = rollType
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Issues occured trying to roll basic dice!");

                return new();
            }
        }

        public async Task<DiceEvent> QuickRollDice(RollDiceRequest request)
        {
            return await GameRoll(request, -1);
        }

        public async Task<RollDiceBasicResponse> GameRoll(RollDiceBasicRequest request, long GameId)
        {
            var diceEvent = new DiceEvent();
            _context.DiceEvents.Add(diceEvent);
            diceEvent.GameRoll = _context.GameResults.Single(x => x.Id == GameId);

            var rollType = _context.RollTypes.Single(x => x.Id == request.RollTypeId);

            var rolls = new List<DiceRoll>();

            var diceRoll = (await RollDice(request.DiceCount))
                    .ConvertToDiceRoll(rollType, request.Threshold);
            rolls.AddRange(diceRoll);

            diceEvent.Rolls = rolls;

            await _context.SaveChangesAsync();

            return new RollDiceBasicResponse
            {
                Request = request,
                Results = diceEvent,
                RollType = rollType
            };
        }

        public async Task<DiceEvent> GameRoll(RollDiceRequest request, long GameId)
        {
            var diceEvent = new DiceEvent();
            if (GameId != -1)
            {
                _context.DiceEvents.Add(diceEvent);
                diceEvent.GameRoll = _context.GameResults.Single(x => x.Id == GameId);
            }
            var rolls = new List<DiceRoll>();

            var rollTypes = _context.RollTypes.ToList();
            var rulesIds = request.AppliedRules.Where(ar => ar.Selected).Select(ari => ari.Id).ToList() ?? [];
            var rulesToApply = rulesIds.Count > 0 ? _context.GameRules.Where(gr => rulesIds.Contains(gr.Id)).Include(at => at.AppliesTo).ToList() ?? [] : [];

            var hitRoll = (await RollDice(request.DiceCount))
                .ConvertToDiceRoll(rollTypes.Single(x => x.Name.ToUpper().Equals("HIT")), request.HitThreshold);
            if (rulesToApply.Count > 0 && rulesToApply.Any(x => x.AppliesTo.Name.ToUpper().Equals("HITS")))
            {
                hitRoll = await ApplyRules(hitRoll, rulesToApply.Where(x => x.AppliesTo.Name.ToUpper().Equals("HITS")).ToList());
            }
            rolls.AddRange(hitRoll);
            var hitCount = hitRoll.Count(x => x.PassResult);
            var autoWound = hitRoll.Count(x => x.IgnoreNextRoll);

            var woundType = rollTypes.Single(x => x.Name.ToUpper().Equals("WOUND"));
            var woundRoll = (await RollDice(hitCount - autoWound))
                .ConvertToDiceRoll(woundType, request.WoundThreshold);
            woundRoll.AddRange(AddAutoRolls(autoWound, true, woundType));
            if (rulesToApply.Count > 0 && rulesToApply.Any(x => x.AppliesTo.Name.ToUpper().Equals("WOUNDS")))
            {
                woundRoll = await ApplyRules(woundRoll, rulesToApply.Where(x => x.AppliesTo.Name.ToUpper().Equals("WOUNDS")).ToList());
            }
            rolls.AddRange(woundRoll);
            var woundCount = woundRoll.Count(x => x.PassResult);
            var autoSaveFail = woundRoll.Count(x => x.IgnoreNextRoll);

            var saveType = rollTypes.Single(x => x.Name.ToUpper().Equals("SAVE"));
            var saveRoll = (await RollDice(woundCount - autoSaveFail))
                .ConvertToDiceRoll(saveType, request.SaveThreshold);
            saveRoll.AddRange(AddAutoRolls(autoSaveFail, false, saveType));
            if (rulesToApply.Count > 0 && rulesToApply.Any(x => x.AppliesTo.Name.ToUpper().Equals("SAVES")))
            {
                saveRoll = await ApplyRules(saveRoll, rulesToApply.Where(x => x.AppliesTo.Name.ToUpper().Equals("SAVES")).ToList());
            }
            rolls.AddRange(saveRoll);
            var saveCount = saveRoll.Count(x => x.PassResult);

            if (request.FeelNoPainFlag)
            {
                var fnpRoll = (await RollDice(woundCount - saveCount))
                    .ConvertToDiceRoll(rollTypes.Single(x => x.Name.ToUpper().Equals("FEEL NO PAIN")), request.FeelNoPainThreshold ?? 6);
                rolls.AddRange(fnpRoll);
            }


            var modList = new List<DiceRoll>();
            modList.AddRange(rolls);

            foreach (var roll in rolls.Where(x => x.FirstResult != null))
            {
                modList.Add(roll.FirstResult);
            }

            diceEvent.Rolls = modList;

            if (diceEvent.GameRoll != null)
            {
                await _context.SaveChangesAsync();
            }

            return diceEvent;
        }

        private async Task<int[]> RollDice(int diceCount)
        {
            return await Task.Run(() =>
            {
                var diceResults = new int[diceCount];

                for (var i = 0; i < diceCount; i++)
                {
                    diceResults[i] = _rand.Next(1, 7);
                }

                return diceResults;
            });
        }

        private async Task<List<DiceRoll>> ApplyRules(List<DiceRoll> rolls, List<GameRule> rules)
        {
            var modifiedRollList = new List<DiceRoll>();
            var rerollType = _context.RollTypes.Single(x => x.Name.ToUpper().Equals("RE-ROLL"));

            if (rules.Any(x => x.ApplyFirst))
            {
                foreach (var rule in rules.Where(x => x.ApplyFirst))
                {
                    switch (rule.Name.ToUpper())
                    {
                        case "REROLL 1'S":
                            foreach (var roll in rolls)
                            {
                                if (roll.RollResult == 1)
                                {
                                    var newRoll = (await RollDice(1))[0].ConvertToDiceRoll(roll.RollType, roll.Threshold) ?? new();
                                    roll.RollType = rerollType;
                                    newRoll.FirstResult = roll;
                                    modifiedRollList.Add(newRoll);
                                }
                                else
                                {
                                    modifiedRollList.Add(roll);
                                }
                            }
                            break;
                        case "REROLL FAILS":
                            foreach (var roll in rolls)
                            {
                                if (roll.RollResult < roll.Threshold)
                                {
                                    var newRoll = (await RollDice(1))[0].ConvertToDiceRoll(roll.RollType, roll.Threshold) ?? new();
                                    roll.RollType = rerollType;
                                    newRoll.FirstResult = roll;
                                    modifiedRollList.Add(newRoll);
                                }
                                else
                                {
                                    modifiedRollList.Add(roll);
                                }
                            }
                            break;
                        case "REROLL SINGLE 1":
                            var failSingle = rolls.FirstOrDefault(x => x.RollResult == 1);
                            if (failSingle != null)
                            {
                                var newRoll = (await RollDice(1))[0].ConvertToDiceRoll(failSingle.RollType, failSingle.Threshold) ?? new();
                                failSingle.RollType = rerollType;
                                newRoll.FirstResult = failSingle;
                                modifiedRollList.Add(newRoll);
                            }
                            break;
                        case "REROLL SINGLE FAIL":
                            var failMulti = rolls.FirstOrDefault(x => x.RollResult < x.Threshold);
                            if (failMulti != null)
                            {
                                var newRoll = (await RollDice(1))[0].ConvertToDiceRoll(failMulti.RollType, failMulti.Threshold) ?? new();
                                failMulti.RollType = rerollType;
                                newRoll.FirstResult = failMulti;
                                modifiedRollList.Add(newRoll);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                modifiedRollList.AddRange(rolls);
            }

            foreach (var rule in rules.Where(x => !x.ApplyFirst && !x.ApplyLast))
            {
                switch (rule.Name.ToUpper())
                {
                    case "ANTI 4+":
                        foreach (var roll in modifiedRollList.Where(x => x.RollResult >= 4))
                        {
                            roll.Critical = true;
                        }
                        break;
                    case "ANTI 5+":
                        foreach (var roll in modifiedRollList.Where(x => x.RollResult >= 5))
                        {
                            roll.Critical = true;
                        }
                        break;
                    default:
                        break;
                }
            }

            foreach (var rule in rules.Where(x => x.ApplyLast))
            {
                switch (rule.Name.ToUpper())
                {
                    case "LETHAL HITS":
                        foreach (var roll in modifiedRollList.Where(x => x.Critical))
                        {
                            roll.IgnoreNextRoll = true;
                        }
                        break;
                    case "DEVASTATING WOUNDS":
                        foreach (var roll in modifiedRollList.Where(x => x.Critical))
                        {
                            roll.IgnoreNextRoll = true;
                        }
                        break;
                    default:
                        break;
                }
            }

            return modifiedRollList;
        }

        private List<DiceRoll> AddAutoRolls(int rolls, bool stat, RollType rollType)
        {
            var autoRolls = new List<DiceRoll>();
            for (var i = 0; i < rolls; i++)
            {
                autoRolls.Add(new DiceRoll
                {
                    RollResult = -1,
                    PassResult = stat,
                    Threshold = -1,
                    RollType = rollType
                });
            }
            return autoRolls;
        }
    }
}

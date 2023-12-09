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
            var diceEvent = new DiceEvent();
            var rolls = new List<DiceRoll>();

            var rollTypes = _context.RollTypes.ToList();

            var hitRoll = (await RollDice(request.DiceCount))
                .ConvertToDiceRoll(rollTypes.Single(x => x.Name.ToUpper().Equals("HIT")), request.HitThreshold);
            rolls.AddRange(hitRoll);
            var hitCount = hitRoll.Count(x => x.PassResult);

            var woundRoll = (await RollDice(hitCount))
                .ConvertToDiceRoll(rollTypes.Single(x => x.Name.ToUpper().Equals("WOUND")), request.WoundThreshold);
            rolls.AddRange(woundRoll);
            var woundCount = woundRoll.Count(x => x.PassResult);

            var saveRoll = (await RollDice(woundCount))
                .ConvertToDiceRoll(rollTypes.Single(x => x.Name.ToUpper().Equals("SAVE")), request.SaveThreshold);
            rolls.AddRange(saveRoll);
            var saveCount = saveRoll.Count(x => x.PassResult);

            if (request.FeelNoPainFlag)
            {
                var fnpRoll = (await RollDice(woundCount - saveCount))
                    .ConvertToDiceRoll(rollTypes.Single(x => x.Name.ToUpper().Equals("FEEL NO PAIN")), request.FeelNoPainThreshold ?? 6);
                rolls.AddRange(fnpRoll);
            }

            diceEvent.Rolls = rolls;

            return diceEvent;
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
            _context.DiceEvents.Add(diceEvent);
            diceEvent.GameRoll = _context.GameResults.Single(x => x.Id == GameId);

            var rolls = new List<DiceRoll>();

            var rollTypes = _context.RollTypes.ToList();

            var hitRoll = (await RollDice(request.DiceCount))
                .ConvertToDiceRoll(rollTypes.Single(x => x.Name.ToUpper().Equals("HIT")), request.HitThreshold);
            rolls.AddRange(hitRoll);
            var hitCount = hitRoll.Count(x => x.PassResult);

            var woundRoll = (await RollDice(hitCount))
                .ConvertToDiceRoll(rollTypes.Single(x => x.Name.ToUpper().Equals("WOUND")), request.WoundThreshold);
            rolls.AddRange(woundRoll);
            var woundCount = woundRoll.Count(x => x.PassResult);

            var saveRoll = (await RollDice(woundCount))
                .ConvertToDiceRoll(rollTypes.Single(x => x.Name.ToUpper().Equals("SAVE")), request.SaveThreshold);
            rolls.AddRange(saveRoll);
            var saveCount = saveRoll.Count(x => x.PassResult);

            if (request.FeelNoPainFlag)
            {
                var fnpRoll = (await RollDice(woundCount - saveCount))
                    .ConvertToDiceRoll(rollTypes.Single(x => x.Name.ToUpper().Equals("FEEL NO PAIN")), request.FeelNoPainThreshold ?? 6);
                rolls.AddRange(fnpRoll);
            }

            diceEvent.Rolls = rolls;

            await _context.SaveChangesAsync();

            return diceEvent;
        }

        private async Task<int[]> RollDice(int diceCount)
        {
            return await Task.Run(() => {
                var diceResults = new int[diceCount];

                for (var i = 0; i < diceCount; i++)
                {
                    diceResults[i] = _rand.Next(1, 7);
                }

                return diceResults;
            });
        }
    }
}

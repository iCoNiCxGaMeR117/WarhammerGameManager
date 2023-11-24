using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WarhammerGameManager.Entities.ApplicationModels;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.Contexts;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels;
using WarhammerGameManager.Entities.ViewModels;
using WarhammerGameManager.Logic.Logical.Interfaces;

namespace WarhammerGameManager.Logic.Logical.Classes
{
    public class GameManagerLogic : IGameManagerLogic
    {
        private readonly Random _rand;
        private readonly WarhammerNarrative_Context _context;
        private readonly ILogger<GameManagerLogic> _logger;
        private readonly IAdminLogic _al;

        public GameManagerLogic(Random rand, WarhammerNarrative_Context context, ILogger<GameManagerLogic> logger, IAdminLogic al)
        {
            _rand = rand;
            _context = context;
            _logger = logger;
            _al = al;
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

        public async Task<GameManagerViewerViewModel> GenerateGameManagerView()
        {
            try
            {
                var games = await _context.GameResults
                    .Include(gd => gd.GamePlayData)
                    .ThenInclude(p => p.PlayerData)
                    .Include(gd => gd.GamePlayData)
                    .ThenInclude(pf => pf.PlayerFaction)
                    .OrderByDescending(x => x.GameDate)
                    .ThenByDescending(x => x.Id)
                    .ToListAsync();

                return new GameManagerViewerViewModel
                {
                    Games = games,
                    Players = await _al.GetAllPlayers(),
                    Factions = await _al.GetAllFactions()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Couldn't generate GameManagerViewerViewModel!");
                return new();
            }
        }

        public async Task<DiceEvent> GameRoll(RollDiceRequest request, long GameId)
        {
            throw new NotImplementedException();
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

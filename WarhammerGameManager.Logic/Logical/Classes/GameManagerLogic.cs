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
            catch (Exception ex) {
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

        public async Task<GameManagerViewerViewModel> GenerateGameManagerView()
        {
            try
            {
                var games = await _context.GameResults
                    .Include(gd => gd.GamePlayData)
                    .ThenInclude(p => p.PlayerData)
                    .Include(gd => gd.GamePlayData)
                    .ThenInclude(pf => pf.PlayerFaction)
                    .OrderByDescending(x => x.Id)
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

        public GameResult GetGameData(long gameId)
        {
            try
            {
                var gameData = _context.GameResults
                    .Include(gd => gd.GamePlayData)
                    .ThenInclude(p => p.PlayerData)
                    .Include(gd => gd.GamePlayData)
                    .ThenInclude(f => f.PlayerFaction)
                    .Single(x => x.Id == gameId);

                return gameData;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Issue getting Game Data!");

                return new();
            }
        }

        public List<DiceEvent> GetGameDiceData(long gameId)
        {
            try
            {
                var data = _context.GameResults
                    .Include(de => de.DiceEvents)
                    .ThenInclude(dr => dr.Rolls)
                    .ThenInclude(rt => rt.RollType)
                    .Single(x => x.Id == gameId);

                return data.DiceEvents.OrderByDescending(x => x.Id).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Issue getting dice roll data!");

                return new List<DiceEvent>();
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

        public async Task UpdatePoints (List<GameData> gameData)
        {
            try
            {
                foreach (var rec in gameData)
                {
                    var curRec = _context.GameDatas.Single(x => x.Id == rec.Id);
                    curRec.Points = rec.Points;
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Issue occured trying to update points for game!");
            }
        }

        public async Task<long> CreateNewGameHost (List<KeyValueMap> gameData)
        {
            try
            {
                var newGame = new GameResult
                {
                    GameDate = DateTime.Now
                };
                _context.GameResults.Add(newGame);

                foreach (var rec in gameData.Where(x => x.Key != -1 && x.Value != -1))
                {
                    newGame.GamePlayData.Add(new GameData
                    {
                        PlayerData = _context.Players.Single(y => y.Id == rec.Key),
                        PlayerFaction = _context.Factions.Single(y => y.Id == rec.Value),
                        Points = 0
                    });
                }

                await _context.SaveChangesAsync();

                return newGame.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Issue occured creating new Game!");
                return -1;
            }
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

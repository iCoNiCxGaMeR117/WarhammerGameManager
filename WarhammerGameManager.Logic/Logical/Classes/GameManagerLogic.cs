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
        private readonly WarhammerNarrative_Context _context;
        private readonly ILogger<GameManagerLogic> _logger;
        private readonly IAdminLogic _al;

        public GameManagerLogic(WarhammerNarrative_Context context, ILogger<GameManagerLogic> logger, IAdminLogic al)
        {
            _context = context;
            _logger = logger;
            _al = al;
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
    }
}

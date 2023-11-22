using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.Contexts;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels;
using WarhammerGameManager.Entities.ViewModels;
using WarhammerGameManager.Logic.Logical.Interfaces;

namespace WarhammerGameManager.Logic.Logical.Classes
{
    public class AdminLogic : IAdminLogic
    {
        private readonly ILogger _logger;
        private readonly WarhammerNarrative_Context _context;

        public AdminLogic(ILogger<AdminLogic> logger, WarhammerNarrative_Context context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<PlayerEditorViewModel> GetPlayerEditorViewModel()
        {
            try
            {
                var viewModel = new PlayerEditorViewModel();

                var players = await GetAllPlayers();
                var factions = await GetAllFactions();

                viewModel.Players = players;
                viewModel.Factions = factions;

                return viewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Issue Occured Getting Player Editor View Model!");
                return new();
            }
        }

        public async Task<IList<Player>> GetAllPlayers()
        {
            try
            {
                _logger.LogInformation("Getting player data...");

                var players = await _context.Players.Include(f => f.SubFactions).ToListAsync();

                if (players == null)
                {
                    players = new List<Player>();
                }

                _logger.LogInformation("Got player data! {PlayersCount} players retrieved!", players.Count);

                return players;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Issue Occured Getting Players!");
                return new List<Player>();
            }
        }

        public async Task<IList<Faction>> GetAllFactions()
        {
            try
            {
                _logger.LogInformation("Getting faction data...");

                var factions = await _context.Factions.ToListAsync();

                if (factions == null)
                {
                    factions = new List<Faction>();
                }

                _logger.LogInformation("Got faction data! {FactionsCount} factions retrieved!", factions.Count);

                return factions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Issue Occured Getting Factions!");
                return new List<Faction>();
            }
        }
    }
}

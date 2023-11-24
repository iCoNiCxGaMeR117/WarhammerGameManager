using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using WarhammerGameManager.Entities.ApplicationModels;
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

                viewModel.Players = await GetAllPlayers();
                viewModel.Sub_Factions = await GetAllSubFactions();

                return viewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Issue Occured Getting Player Editor View Model!");
                return new();
            }
        }

        public async Task PlayerInfoUpdater(PlayerDataRequest request)
        {
            try
            {
                if (request.ExistingPlayerId != null)
                {
                    var curPlayer = await _context.Players.Include(x => x.SubFactions).SingleAsync(x => x.Id.Equals(request.ExistingPlayerId));
                    var curFactions = curPlayer.SubFactions.Select(x => x.Id);

                    if (curPlayer != null)
                    {
                        curPlayer.FirstName = request.FirstName;
                        curPlayer.LastName = request.LastName;
                        
                        foreach (var subFaction in request.Sub_Factions)
                        {
                            if (subFaction.Selected && (!curFactions?.Contains(subFaction.Id) ?? false))
                            {
                                curPlayer.SubFactions.Add(await _context.SubFactions.SingleAsync(y => y.Id == subFaction.Id));
                            }
                            else if (!subFaction.Selected && (curFactions?.Contains(subFaction.Id) ?? false))
                            {
                                curPlayer.SubFactions.Remove(await _context.SubFactions.SingleAsync(y => y.Id == subFaction.Id));
                            }
                        }
                    }
                }
                else
                {
                    var newPlayer = new Player();
                    _context.Players.Add(newPlayer);

                    newPlayer.FirstName = request.FirstName;
                    newPlayer.LastName = request.LastName;

                    var selectedSubFactions = request.Sub_Factions.Where(y => y.Selected).Select(y => y.Id);

                    newPlayer.SubFactions = await _context.SubFactions.Where(x => selectedSubFactions.Contains(x.Id)).ToListAsync();
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Issue Occured Updating Player Data! - {PlayerData}", JsonSerializer.Serialize(request));
            }
        }

        public async Task<IList<Player>> GetAllPlayers()
        {
            try
            {
                _logger.LogInformation("Getting player data...");

                var players = await _context.Players
                    .Include(sf => sf.SubFactions)
                    .ThenInclude(f => f.Faction)
                    .ThenInclude(pf => pf.Parent)
                    .OrderBy(nm => nm.LastName)
                    .ThenBy(nml => nml.LastName)
                    .ToListAsync();

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

        public async Task<IList<SubFaction>> GetAllSubFactions()
        {
            try
            {
                _logger.LogInformation("Getting sub-faction data...");

                var factions = await _context.SubFactions
                    .Include(f => f.Faction)
                    .ThenInclude(pf => pf.Parent)
                    .OrderBy(nm => nm.Name)
                    .ToListAsync();

                if (factions == null)
                {
                    factions = new List<SubFaction>();
                }

                _logger.LogInformation("Got sub-faction data! {FactionsCount} sub-factions retrieved!", factions.Count);

                return factions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Issue Occured Getting Sub-Factions!");
                return new List<SubFaction>();
            }
        }

        public async Task<IList<Faction>> GetAllFactions()
        {
            try
            {
                _logger.LogInformation("Getting faction data...");

                var factions = await _context.Factions
                    .Include(x => x.Parent)
                    .Include(y => y.SubFactions)
                    .OrderBy(nm => nm.Name)
                    .ToListAsync();

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

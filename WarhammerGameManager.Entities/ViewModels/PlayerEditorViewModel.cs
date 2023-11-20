using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels;

namespace WarhammerGameManager.Entities.ViewModels
{
    public class PlayerEditorViewModel
    {
        public PlayerEditorViewModel()
        {
            Players = new List<Player>();
            Factions = new List<Faction>();
        }

        public IList<Player> Players { get; set; }

        public IList<Faction> Factions { get; set; } 
    }
}

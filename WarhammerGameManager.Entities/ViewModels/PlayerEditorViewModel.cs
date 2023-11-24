using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels;

namespace WarhammerGameManager.Entities.ViewModels
{
    public class PlayerEditorViewModel
    {
        public PlayerEditorViewModel()
        {
            Players = new List<Player>();
            Sub_Factions = new List<SubFaction>();
        }

        public IList<Player> Players { get; set; }

        public IList<SubFaction> Sub_Factions { get; set; } 
    }
}

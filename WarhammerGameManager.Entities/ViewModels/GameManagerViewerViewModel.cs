using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels;

namespace WarhammerGameManager.Entities.ViewModels
{
    public class GameManagerViewerViewModel
    {
        public IList<GameResult> Games { get; set; } = new List<GameResult>();

        public IList<Player> Players { get; set; } = new List<Player>();

        public IList<Faction> Factions { get; set; } = new List<Faction>();
    }
}

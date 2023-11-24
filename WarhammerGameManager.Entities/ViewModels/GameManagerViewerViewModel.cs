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
        public IList<GameResult> Games { get; set; }

        public IList<Player> Players { get; set; }

        public IList<Faction> Factions { get; set; }
    }
}

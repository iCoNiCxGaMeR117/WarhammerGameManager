using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels
{
    public class Player
    {
        public Player()
        {
            Factions = new HashSet<Faction>();
            Games = new HashSet<GameResult>();
        }

        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Faction> Factions { get; set; }

        public ICollection<GameResult> Games { get; set; }
    }
}

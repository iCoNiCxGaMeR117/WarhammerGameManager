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
            SubFactions = new HashSet<SubFaction>();
            Games = new HashSet<GameData>();
        }

        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<SubFaction> SubFactions { get; set; }

        public virtual ICollection<GameData> Games { get; set; }
    }
}

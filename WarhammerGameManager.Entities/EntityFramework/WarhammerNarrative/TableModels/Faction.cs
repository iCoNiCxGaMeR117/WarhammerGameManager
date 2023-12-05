using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels
{
    public class Faction
    {
        public Faction()
        {
            Games = new HashSet<GameData>();
            SubFactions = new HashSet<SubFaction>();
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<GameData> Games { get; set; }

        public ParentFaction Parent { get; set; }

        public virtual ICollection<SubFaction> SubFactions { get; set; }
    }
}

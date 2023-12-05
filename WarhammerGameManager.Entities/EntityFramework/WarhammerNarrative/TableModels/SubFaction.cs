using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels
{
    public class SubFaction
    {
        public SubFaction()
        {
            PlayerFactions = new HashSet<Player>();
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public Faction Faction { get; set; }

        public virtual ICollection<Player> PlayerFactions { get; set; }
    }
}

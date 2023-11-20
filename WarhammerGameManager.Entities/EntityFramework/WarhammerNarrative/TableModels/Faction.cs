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
            PlayerFactions = new HashSet<Player>();
            Games = new HashSet<GameData>();
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Player> PlayerFactions { get; set; }

        public virtual ICollection<GameData> Games { get; set; }
    }
}

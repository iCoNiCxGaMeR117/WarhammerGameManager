using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels
{
    public class ParentFaction
    {
        public ParentFaction ()
        {
            ChildFactions = new HashSet<Faction>();
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Faction> ChildFactions { get; set; }
    }
}

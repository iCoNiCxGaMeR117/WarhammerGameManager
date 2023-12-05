using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels
{
    public class RollType
    {
        public RollType()
        {
            DiceRolls = new HashSet<DiceRoll>();
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<DiceRoll> DiceRolls { get; set; }
    }
}

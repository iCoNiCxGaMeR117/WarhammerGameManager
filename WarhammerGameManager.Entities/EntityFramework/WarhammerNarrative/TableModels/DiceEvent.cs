using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels
{
    public class DiceEvent
    {
        public DiceEvent()
        {
            HitRolls = new HashSet<DiceRoll>();
            WoundRolls = new HashSet<DiceRoll>();
            SaveRolls = new HashSet<DiceRoll>();
            FeelNoPainRolls = new HashSet<DiceRoll>();
        }

        public long Id { get; set; }

        public virtual ICollection<DiceRoll> HitRolls { get; set; }

        public virtual ICollection<DiceRoll> WoundRolls { get; set; }

        public virtual ICollection<DiceRoll> SaveRolls { get; set; }

        public bool FeelNoPainFlag { get; set; }

        public virtual ICollection<DiceRoll> FeelNoPainRolls { get; set; }
    }
}

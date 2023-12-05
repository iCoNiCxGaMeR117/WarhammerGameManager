using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels
{
    public class DiceEvent
    {
        public DiceEvent()
        {
            Rolls = new HashSet<DiceRoll>();
        }

        public long Id { get; set; }

        public GameResult GameRoll { get; set; }

        public virtual ICollection<DiceRoll> Rolls { get; set; }

        [NotMapped]
        public IEnumerable<DiceRoll> Hits
        {
            get
            {
                return Rolls.Where(x => x.RollType.Name.ToUpper().Equals("HIT"));
            }
        }

        [NotMapped]
        public IEnumerable<DiceRoll> Wounds
        {
            get
            {
                return Rolls.Where(x => x.RollType.Name.ToUpper().Equals("WOUND"));
            }
        }

        [NotMapped]
        public IEnumerable<DiceRoll> Saves
        {
            get
            {
                return Rolls.Where(x => x.RollType.Name.ToUpper().Equals("SAVE"));
            }
        }

        [NotMapped]
        public IEnumerable<DiceRoll> FeelNoPains
        {
            get
            {
                return Rolls.Where(x => x.RollType.Name.ToUpper().Equals("FEEL NO PAIN"));
            }
        }

        [NotMapped]
        public int TotalDamagingAttacks
        {
            get
            {
                return Wounds.Count(x => x.PassResult) - (Saves.Count(x => x.PassResult) + FeelNoPains.Count(x => x.PassResult));
            }
        }
    }
}

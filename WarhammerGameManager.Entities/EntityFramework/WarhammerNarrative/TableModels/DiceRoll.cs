using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels
{
    public class DiceRoll
    {
        public long Id { get; set; }

        public int RollResult { get; set; }

        public bool PassResult { get; set; }

        public int Threshold { get; set; }

        public bool Critical { get; set; }

        public bool IgnoreNextRoll { get; set; }

        public DiceRoll? FirstResult { get; set; }

        public DiceEvent Event { get; set; }

        public RollType RollType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhammerGameManager.Entities.ApplicationModels
{
    public class RollDiceRequest
    {
        public int DiceCount { get; set; }

        public int HitThreshold { get; set; }

        public int SaveThreshold { get; set; }

        public int WoundThreshold { get; set; }

        public bool FeelNoPainFlag { get; set; }

        public int? FeelNoPainThreshold { get; set; } = null;
    }
}

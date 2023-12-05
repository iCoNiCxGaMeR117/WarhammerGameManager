using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels;

namespace WarhammerGameManager.Entities.ApplicationModels
{
    public class RollDiceBasicRequest
    {
        public int DiceCount { get; set; }

        public int Threshold { get; set; }

        public int RollTypeId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels;

namespace WarhammerGameManager.Entities.ApplicationModels
{
    public class RollDiceResponse
    {
        public RollDiceRequest Request { get; set; }

        public DiceEvent Results { get; set; }
    }
}

﻿using System;
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
            Rolls = new HashSet<DiceRoll>();
        }

        public long Id { get; set; }

        public GameResult GameRoll { get; set; }

        public virtual ICollection<DiceRoll> Rolls { get; set; }
    }
}

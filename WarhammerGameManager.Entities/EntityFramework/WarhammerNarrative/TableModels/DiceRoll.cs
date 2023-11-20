﻿using System;
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

        public DiceEvent Event { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels
{
    public class GameData
    {
        public long Id { get; set; }

        public Player PlayerData { get; set; }

        public Faction PlayerFaction { get; set; }

        public int Points { get; set; }
    }
}

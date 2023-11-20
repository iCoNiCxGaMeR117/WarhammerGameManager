using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels
{
    public class GameResult
    {
        public GameResult()
        {
            GamePlayData = new HashSet<GameData>();
            DiceEvents = new HashSet<DiceEvent>();
        }

        public long Id { get; set; }

        public virtual ICollection<GameData> GamePlayData { get; set; }

        public virtual ICollection<DiceEvent> DiceEvents { get; set; }
    }
}

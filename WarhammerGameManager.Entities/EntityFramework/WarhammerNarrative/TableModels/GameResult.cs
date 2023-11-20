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
        }

        public ICollection<GameData> GamePlayData { get; set; }

        public Player Winner { get; set; }
    }
}

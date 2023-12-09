using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels
{
    public class RuleAppliesTo
    {
        public RuleAppliesTo()
        {
            GameRules = new HashSet<GameRule>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<GameRule> GameRules { get; set; }
    }
}

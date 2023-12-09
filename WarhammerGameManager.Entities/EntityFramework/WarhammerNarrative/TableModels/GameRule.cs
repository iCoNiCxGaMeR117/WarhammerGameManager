using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels
{
    public class GameRule
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool ApplyFirst { get; set; }
        public bool ApplyLast { get; set; }

        public RuleAppliesTo AppliesTo { get; set; }
    }
}

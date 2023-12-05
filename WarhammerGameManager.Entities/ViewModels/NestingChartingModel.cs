using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhammerGameManager.Entities.ViewModels
{
    public class NestingChartingModel
    {
        public string Key { get; set; }

        public IList<ChartingModel> Value { get; set; } = new List<ChartingModel>();
    }
}

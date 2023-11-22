using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WarhammerGameManager.Entities.ViewModels;

namespace WarhammerGameManager.Logic
{
    public static class Extensions
    {
        public static string GenerateBasicChartData(this Dictionary<string, decimal> data)
        {
                return JsonSerializer.Serialize(data
                    .Select(x =>
                        new ChartingModel
                        {
                            Key = x.Key,
                            Value = x.Value
                        })
                    .Where(x => x.Value > 0)
                    .ToArray());
        }

        public static string GenerateListChartData<T>(this Dictionary<string, IList<T>> data)
        {
            return JsonSerializer.Serialize(data
                .Select(x =>
                    new ChartingModel
                    {
                        Key = x.Key,
                        Value = x.Value.Count
                    })
                .Where(x => x.Value > 0)
                .ToArray());
        }
    }
}

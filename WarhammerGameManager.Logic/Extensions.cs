using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels;
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

        public static List<DiceRoll> ConvertToDiceRoll(this int[] rolls, RollType rollType, int threshold)
        {
            var diceRolls = new List<DiceRoll>();

            foreach (var roll in rolls) 
            {
                diceRolls.Add(new DiceRoll
                {
                    RollResult = roll,
                    RollType = rollType,
                    PassResult = (roll >= threshold)
                });
            }

            return diceRolls;
        }
    }
}

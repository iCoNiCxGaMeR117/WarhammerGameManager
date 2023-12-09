using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels;

namespace WarhammerGameManager.Entities.ViewModels
{
    public class RollDiceViewModel
    {
        public List<RollType> RollTypes { get; set; }

        public List<GameRule> GameRules { get; set; }
    }
}

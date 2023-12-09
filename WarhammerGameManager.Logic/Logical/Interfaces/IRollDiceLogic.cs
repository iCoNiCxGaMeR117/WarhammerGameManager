using WarhammerGameManager.Entities.ApplicationModels;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels;
using WarhammerGameManager.Entities.ViewModels;

namespace WarhammerGameManager.Logic.Logical.Interfaces
{
    public interface IRollDiceLogic
    {
        Task<RollDiceBasicResponse> GameRoll(RollDiceBasicRequest request, long GameId);
        Task<DiceEvent> GameRoll(RollDiceRequest request, long GameId);
        Task<List<GameRule>> GetGameRules();
        Task<RollDiceViewModel> GetRollDiceViewModel();
        Task<List<RollType>> GetRollTypes();
        Task<RollDiceBasicResponse> QuickRollDice(RollDiceBasicRequest request);
        Task<DiceEvent> QuickRollDice(RollDiceRequest request);
    }
}

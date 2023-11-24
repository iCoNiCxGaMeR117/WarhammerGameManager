using WarhammerGameManager.Entities.ApplicationModels;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels;
using WarhammerGameManager.Entities.ViewModels;

namespace WarhammerGameManager.Logic.Logical.Interfaces
{
    public interface IGameManagerLogic
    {
        Task<DiceEvent> GameRoll(RollDiceRequest request, long GameId);
        Task<GameManagerViewerViewModel> GenerateGameManagerView();
        Task<DiceEvent> QuickRollDice(RollDiceRequest request);
    }
}

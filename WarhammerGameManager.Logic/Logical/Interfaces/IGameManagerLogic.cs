using WarhammerGameManager.Entities.ApplicationModels;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels;

namespace WarhammerGameManager.Logic.Logical.Interfaces
{
    public interface IGameManagerLogic
    {
        Task<DiceEvent> GameRoll(RollDiceRequest request, long GameId);
        Task<DiceEvent> QuickRollDice(RollDiceRequest request);
    }
}

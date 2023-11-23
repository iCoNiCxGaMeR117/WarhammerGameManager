using WarhammerGameManager.Entities.ApplicationModels;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels;

namespace WarhammerGameManager.Logic.Logical.Interfaces
{
    public interface IGameManagerLogic
    {
        DiceEvent GameRoll(RollDiceRequest request, long GameId);
        DiceEvent QuickRollDice(RollDiceRequest request);
    }
}

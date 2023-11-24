using WarhammerGameManager.Entities.ApplicationModels;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels;
using WarhammerGameManager.Entities.ViewModels;

namespace WarhammerGameManager.Logic.Logical.Interfaces
{
    public interface IGameManagerLogic
    {
        Task<long> CreateNewGameHost(List<KeyValueMap> gameData);
        Task<DiceEvent> GameRoll(RollDiceRequest request, long GameId);
        Task<GameManagerViewerViewModel> GenerateGameManagerView();
        GameResult GetGameData(long gameId);
        List<DiceEvent> GetGameDiceData(long gameId);
        Task<DiceEvent> QuickRollDice(RollDiceRequest request);
        Task UpdatePoints(List<GameData> gameData);
    }
}

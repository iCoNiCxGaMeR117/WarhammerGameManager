using WarhammerGameManager.Entities.ApplicationModels;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels;
using WarhammerGameManager.Entities.ViewModels;

namespace WarhammerGameManager.Logic.Logical.Interfaces
{
    public interface IGameManagerLogic
    {
        Task<long> CreateNewGameHost(List<KeyValueMap> gameData);
        Task<GameManagerViewerViewModel> GenerateGameManagerView();
        GameResult GetGameData(long gameId);
        List<DiceEvent> GetGameDiceData(long gameId);
        Task UpdatePoints(List<GameData> gameData);
    }
}

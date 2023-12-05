using WarhammerGameManager.Entities.ApplicationModels;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels;
using WarhammerGameManager.Entities.ViewModels;

namespace WarhammerGameManager.Logic.Logical.Interfaces
{
    public interface IGameManagerLogic
    {
        Task<long> CreateNewGameHost(List<KeyValueMap> gameData);
        Task<DiceEvent> GameRoll(RollDiceRequest request, long GameId);
        Task<RollDiceBasicResponse> GameRoll(RollDiceBasicRequest request, long GameId);
        Task<GameManagerViewerViewModel> GenerateGameManagerView();
        GameResult GetGameData(long gameId);
        List<DiceEvent> GetGameDiceData(long gameId);
        Task<List<RollType>> GetRollTypes();
        Task<DiceEvent> QuickRollDice(RollDiceRequest request);
        Task<RollDiceBasicResponse> QuickRollDice(RollDiceBasicRequest request);
        Task UpdatePoints(List<GameData> gameData);
    }
}

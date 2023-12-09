using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WarhammerGameManager.Entities.ApplicationModels;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels;
using WarhammerGameManager.Frontend.Hubs;
using WarhammerGameManager.Logic.Logical.Interfaces;

namespace WarhammerGameManager.Frontend.Controllers
{
    public class GameTrackerController : Controller
    {
        private readonly IGameManagerLogic _gml;
        private readonly IHubContext<GameUpdateHub> _hubContext;

        public GameTrackerController(IGameManagerLogic gml, IHubContext<GameUpdateHub> hub)
        {
            _gml = gml;
            _hubContext = hub;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _gml.GenerateGameManagerView());
        }

        public IActionResult OpenGame(long gameId)
        {
            return View(_gml.GetGameData(gameId));
        }

        public async Task<IActionResult> SavePointsUpdates(long GameId, List<GameData> GameData)
        {
            await _gml.UpdatePoints(GameData);
            await _hubContext.Clients.All.SendAsync("ReceiveUpdatedPoints", GameData.ToArray(), GameId);
            return RedirectToAction("OpenGame", new { gameId = GameId });
        }

        [HttpGet]
        public IActionResult GetRollData (long gameId)
        {
            return PartialView("~/Views/GameTracker/Partials/RollsHistoryPartialView.cshtml", _gml.GetGameDiceData(gameId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewGame(List<KeyValueMap> gameData)
        {
            //Key is player id, Value is faction id
            var newGameId = await _gml.CreateNewGameHost(gameData);

            return RedirectToAction("OpenGame", new { gameId = newGameId });
        }
    }
}

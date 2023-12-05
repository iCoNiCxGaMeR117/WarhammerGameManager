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

        [HttpPost]
        public async Task<IActionResult> RollDice(RollDiceRequest request, long? GameId = null)
        {
            DiceEvent data;

            //If there is no provided GameId, then this is a quick roll
            if (GameId == null)
            {
                data = await _gml.QuickRollDice(request);
            }
            else
            {
                data = await _gml.GameRoll(request, GameId.Value);
                await _hubContext.Clients.All.SendAsync("ReceiveRollsUpdates", GameId.Value);
            }

            var response = new RollDiceResponse()
            {
                Request = request,
                Results = data
            };

            return PartialView("~/Views/GameTracker/Partials/RollDiceResultsPartialView.cshtml", response);
        }

        [HttpPost]
        public async Task<IActionResult> RollDiceBasic(RollDiceBasicRequest request, long? GameId = null)
        {
            RollDiceBasicResponse response;
            //If there is no provided GameId, then this is a quick roll
            if (GameId == null)
            {
                response = await _gml.QuickRollDice(request);
            }
            else
            {
                response = await _gml.GameRoll(request, GameId.Value);
                await _hubContext.Clients.All.SendAsync("ReceiveRollsUpdates", GameId.Value);
            }

            return PartialView("~/Views/GameTracker/Partials/RollDiceResultsBasicPartialView.cshtml", response);
        }
    }
}

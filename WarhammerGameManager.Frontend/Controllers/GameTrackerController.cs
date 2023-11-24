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

        [HttpGet]
        public IActionResult GetRollData (long gameId)
        {
            return PartialView("~/Views/GameTracker/Partials/RollsHistoryPartialView.cshtml", _gml.GetGameDiceData(gameId));
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
                await _hubContext.Clients.All.SendAsync("ReceiveUpdates", GameId.Value);
            }

            var response = new RollDiceResponse()
            {
                Request = request,
                Results = data
            };

            return PartialView("~/Views/GameTracker/Partials/RollDiceResultsPartialView.cshtml", response);
        }
    }
}

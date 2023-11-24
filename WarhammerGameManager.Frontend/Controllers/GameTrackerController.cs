using Microsoft.AspNetCore.Mvc;
using WarhammerGameManager.Entities.ApplicationModels;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels;
using WarhammerGameManager.Logic.Logical.Interfaces;

namespace WarhammerGameManager.Frontend.Controllers
{
    public class GameTrackerController : Controller
    {
        private readonly IGameManagerLogic _gml;

        public GameTrackerController(IGameManagerLogic gml)
        {
            _gml = gml;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _gml.GenerateGameManagerView());
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

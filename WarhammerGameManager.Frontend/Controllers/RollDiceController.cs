using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WarhammerGameManager.Entities.ApplicationModels;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels;
using WarhammerGameManager.Frontend.Hubs;
using WarhammerGameManager.Logic.Logical.Interfaces;

namespace WarhammerGameManager.Frontend.Controllers
{
    public class RollDiceController : Controller
    {
        private readonly IGameManagerLogic _gml;
        private readonly IHubContext<GameUpdateHub> _hubContext;

        public RollDiceController(IGameManagerLogic gml, IHubContext<GameUpdateHub> hub)
        {
            _gml = gml;
            _hubContext = hub;
        }

        [HttpGet]
        public async Task<IActionResult> GenerateRollDiceForm()
        {
            return PartialView("~/Views/RollDice/Partials/RollDiceFormsPartial.cshtml", await _gml.GetRollTypes());
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

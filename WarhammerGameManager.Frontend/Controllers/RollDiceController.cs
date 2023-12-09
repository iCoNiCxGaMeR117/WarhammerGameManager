using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WarhammerGameManager.Entities.ApplicationModels;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels;
using WarhammerGameManager.Frontend.Hubs;
using WarhammerGameManager.Logic.Logical.Interfaces;

namespace WarhammerGameManager.Frontend.Controllers
{
    [Authorize]
    public class RollDiceController : Controller
    {
        private readonly IRollDiceLogic _rdl;
        private readonly IHubContext<GameUpdateHub> _hubContext;

        public RollDiceController(IRollDiceLogic rdl, IHubContext<GameUpdateHub> hub)
        {
            _rdl = rdl;
            _hubContext = hub;
        }

        [HttpGet]
        public async Task<IActionResult> GenerateRollDiceForm()
        {
            return PartialView("~/Views/RollDice/Partials/RollDiceFormsPartial.cshtml", await _rdl.GetRollDiceViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> RollDice(RollDiceRequest request, long? GameId = null)
        {
            DiceEvent data;

            //If there is no provided GameId, then this is a quick roll
            if (GameId == null)
            {
                data = await _rdl.QuickRollDice(request);
            }
            else
            {
                data = await _rdl.GameRoll(request, GameId.Value);
                await _hubContext.Clients.All.SendAsync("ReceiveRollsUpdates", GameId.Value);
            }

            var response = new RollDiceResponse()
            {
                Request = request,
                Results = data
            };

            return PartialView("~/Views/RollDice/Partials/RollDiceResultsPartialView.cshtml", response);
        }

        [HttpPost]
        public async Task<IActionResult> RollDiceBasic(RollDiceBasicRequest request, long? GameId = null)
        {
            RollDiceBasicResponse response;
            //If there is no provided GameId, then this is a quick roll
            if (GameId == null)
            {
                response = await _rdl.QuickRollDice(request);
            }
            else
            {
                response = await _rdl.GameRoll(request, GameId.Value);
                await _hubContext.Clients.All.SendAsync("ReceiveRollsUpdates", GameId.Value);
            }

            return PartialView("~/Views/RollDice/Partials/RollDiceResultsBasicPartialView.cshtml", response);
        }
    }
}

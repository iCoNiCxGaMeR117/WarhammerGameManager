using Microsoft.AspNetCore.Mvc;
using WarhammerGameManager.Logic.Logical.Interfaces;

namespace WarhammerGameManager.Frontend.Controllers
{
    public class RollDiceController : Controller
    {
        private readonly IGameManagerLogic _gml;

        public RollDiceController(IGameManagerLogic gml)
        {
            _gml = gml;
        }

        [HttpGet]
        public async Task<IActionResult> GenerateRollDiceForm()
        {
            return PartialView("~/Views/RollDice/Partials/RollDiceFormsPartial.cshtml", await _gml.GetRollTypes());
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace WarhammerGameManager.Frontend.Controllers
{
    public class DiceController : Controller
    {
        public IActionResult RollDice()
        {
            return PartialView();
        }
    }
}

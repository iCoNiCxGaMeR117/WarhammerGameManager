using Microsoft.AspNetCore.Mvc;

namespace WarhammerGameManager.Frontend.Controllers
{
    public class DiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

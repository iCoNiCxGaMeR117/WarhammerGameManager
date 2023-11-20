using Microsoft.AspNetCore.Mvc;

namespace WarhammerGameManager.Frontend.Controllers
{
    public class GameTrackerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

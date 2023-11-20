using Microsoft.AspNetCore.Mvc;

namespace WarhammerGameManager.Frontend.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

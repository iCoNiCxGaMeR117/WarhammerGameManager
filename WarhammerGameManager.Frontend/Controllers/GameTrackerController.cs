using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View();
        }
    }
}

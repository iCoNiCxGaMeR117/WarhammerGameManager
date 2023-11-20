using Microsoft.AspNetCore.Mvc;
using WarhammerGameManager.Logic.Logical.Interfaces;

namespace WarhammerGameManager.Frontend.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminLogic _adminLogic;

        public AdminController(IAdminLogic adminLogic)
        {
            _adminLogic = adminLogic;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PlayerEditor()
        {
            return View(await _adminLogic.GetPlayerEditorViewModel());
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Mentat.UI.Controllers
{
    public class MentorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

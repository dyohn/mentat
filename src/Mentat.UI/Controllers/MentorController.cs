using Microsoft.AspNetCore.Mvc;

namespace Mentat.UI.Controllers
{
    public class MentorController : Controller
    {
        public IActionResult MentorView()
        {
            return View();
        }
    }
}

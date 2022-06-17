using Microsoft.AspNetCore.Mvc;

namespace Mentat.UI.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult StudentView()
        {
            return View();
        }
    }
}

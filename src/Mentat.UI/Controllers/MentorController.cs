using Mentat.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mentat.UI.Controllers
{
    public class MentorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddAssignment(Assignment assignment)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(Assignment assignment)
        {
            // If form is correct then moves on to the next view 
            // otherwise view is reloaded and user must retry filling the form
            if (ModelState.IsValid)
            {
                // On Form Validation Success Go To This View
                return RedirectToAction("Success");
            }
            return View("AddAssignment");
        }

        public ActionResult Success(Assignment assignment)
        {
            return View();
        }
    }
}

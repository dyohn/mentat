using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Mentat.UI.Controllers
{
    public class MentorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddAssignment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(Models.Assignment assignment)
        {
            var mentorName = assignment.mentorName;
            var assignmentName = assignment.assignmentName;
            var assignmentType = assignment.assignmentType;
            var sampleExecutableName = assignment.sampleExecutableName;
            var testFileNames = assignment.testFileNames;
            return View();
        }
    }
}

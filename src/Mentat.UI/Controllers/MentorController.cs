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
            var mentorName = assignment.MentorName;
            var assignmentName = assignment.AssignmentName;
            var assignmentType = assignment.AssignmentType;
            var sampleExecutableName = assignment.SampleExecutableName;
            var testFileNames = assignment.TestFileNames;
            return View();
        }
    }
}

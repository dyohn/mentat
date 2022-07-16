using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;

namespace Mentat.UI.Controllers
{
    public class MentorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult AddAssignment(Models.Assignment assignment)
        {
 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(Models.Assignment assignment)
        {

            //DEMO OF HOW VALUES ARE STORED
            var mentorName = assignment.MentorName;
            var assignmentName = assignment.AssignmentName;
            var assignmentType = assignment.AssignmentType;
            var sampleExecutableName = assignment.SampleExecutableName;
            var testFileNames = assignment.TestFileNames;

            // If form is correct then moves on to the next view 
            // otherwise view is reloaded and user must retry filling the form
            if (ModelState.IsValid)
            {
                return RedirectToAction("SendData", assignment);
            }
 
            return View();
        }

        // On Form Validation Success Go To This View
        public IActionResult SendData(Models.Assignment assignment)
        {
            return View("SubmitForm");
        }


    }
}

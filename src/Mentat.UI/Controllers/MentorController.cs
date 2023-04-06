using System.IO;
using System.Text;
using Mentat.Domain.Bash;
using System.Collections.Generic;
using Mentat.UI.Models;
using Mentat.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public ActionResult SubmitForm(Assignment assignment, List<string> testFileNames)
        {
            List<string> list = testFileNames;
            string sampleExecutableName = assignment.SampleExecutableName;
            string assignmentType = assignment.AssignmentType;
            int id = assignment.Id;
            // If form is correct then moves on to the next view 
            // otherwise view is reloaded and user must retry filling the form
            if (ModelState.IsValid)
            {
                // On Form Validation Success Go To This View
                return RedirectToAction("Success");
            }
            return View("AddAssignment");
        }

        /*[HttpPost]
        public IActionResult ConfigureTestFiles(List<string> testFileNames)
        {
            // Create a new BashTestConfig object and configure it with the received test file names
            var config = new BashTestConfig("bash", testFileNames, "sample_executable_name", 1, "diff_command");

            //TODO: create bash driver object ....something else  ..... call build.

            return Ok();
        }*/

        public ActionResult Success(Assignment assignment)
        {
            return View();
        }

        // ********************************************
        // *             Download method              *
        // ********************************************
        // Download a file to the user's local machine.
        public IActionResult Download()
        {
            string content = "Hello, World!";
            string fileName = "file.txt";

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

            return new FileStreamResult(stream, "text/plain")
            {
                FileDownloadName = fileName
            };
        }
    }
}

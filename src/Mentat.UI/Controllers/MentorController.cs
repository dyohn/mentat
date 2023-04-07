using System.IO;
using System.Text;
using Mentat.Domain.Bash;
using System.Collections.Generic;
using Mentat.UI.Models;
using Mentat.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

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
            string sampleExecutableName = assignment.SampleExecutableName;
            string assignmentType = assignment.AssignmentType;
            string concatenatedTestFileNames = assignment.TestFiles;
            List<string> testFileNames = concatenatedTestFileNames.Split(',').ToList();
            int id = assignment.Id;
            string language = assignment.AssignmentType.ToString();
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

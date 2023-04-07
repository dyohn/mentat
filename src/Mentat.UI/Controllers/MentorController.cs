using System.IO;
using System.Text;
using Mentat.Domain.Bash;
using System.Collections.Generic;
using Mentat.UI.Models;
using Mentat.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Mentat.Domain.Interfaces;

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
                string language = assignment.AssignmentType.ToString();
                string sampleExecutableName = assignment.SampleExecutableName;
                string assignmentType = assignment.AssignmentType;
                string concatenatedTestFileNames = assignment.TestFiles;
                List<string> testFileNames = concatenatedTestFileNames.Split(',').ToList();
                int id = assignment.Id;
                bool colorText = assignment.ColorText;
                bool highlightText = assignment.HighlightText;
                bool applyTextModifiers = assignment.ApplyTextModifiers;
                BashTestConfig configs = new BashTestConfig(language, testFileNames, sampleExecutableName, 10, "diff", colorText, highlightText, applyTextModifiers);
                BashTestDriver bashDriver = new BashTestDriver(new FileManagerService());
                bashDriver.Configure(configs);
                FileInfo fileInfo = bashDriver.Build();
                // On Form Validation Success Go To This View
                TempData["fileInfo"] = fileInfo.FullName;
                return RedirectToAction("download");
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
        public ActionResult Download()
        {
            //string content = "Hello, World!";
            //string fileName = "file.txt";
            string path = Convert.ToString(TempData["fileInfo"]);
            string content = System.IO.File.ReadAllText(path);

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

            return new FileStreamResult(stream, "text/plain")
            {
                FileDownloadName = path
            };
        }
    }
}

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
using Microsoft.AspNetCore.Authorization;

namespace Mentat.UI.Controllers
{
    [Authorize(Policy = "Mentor")]
    public class MentorController : Controller
    {
        /// <summary>
        /// Returns the current view.
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Returns the current view.
        /// </summary>
        /// <param name="assignment">Model holding info on an Assignment.</param>
        public IActionResult AddAssignment(Assignment assignment)
        {
            return View();
        }

        /// <summary>
        /// ActionResult that creates a BashTestConfig object and a BashTestDriver Object.
        /// Builds the BashScript and then calls the Download ActionResult.
        /// </summary>
        /// <param name="assignment">Model holding info on an Assignment.</param>
        public ActionResult SubmitForm(Assignment assignment)
        {
            // If form is correct then moves on to the next view 
            // otherwise view is reloaded and user must retry filling the form
            if (ModelState.IsValid)
            {
                /// <value>Holds the language the assignment was made in.</value>
                string language = assignment.AssignmentType.ToString();
                /// <value>Holds the name of the mentors version of the assignment.</value>
                string sampleExecutableName = assignment.SampleExecutableName;
                /// <value>Holds the assignment type.</value>
                string assignmentType = assignment.AssignmentType;
                /// <value>Holds the list of test file names as one string.</value>
                string concatenatedTestFileNames = assignment.TestFiles;
                /// <value>Creates/holds a list of split test file names.</value>
                List<string> testFileNames = concatenatedTestFileNames.Split(',').ToList();
                /// <value>Holds the ID of an assignment.</value>
                int id = assignment.Id;
                /// <value>Holds the bool for color text.</value>
                bool colorText = assignment.ColorText;
                /// <value>Holds the bool for highlighting text.</value>
                bool highlightText = assignment.HighlightText;
                /// <value>Holds the bool for modifying text.</value>
                bool applyTextModifiers = assignment.ApplyTextModifiers;
                /// <value>Creates and holds a configs object.</value>
                BashTestConfig configs = new BashTestConfig(language, testFileNames, sampleExecutableName, 10, "diff", colorText, highlightText, applyTextModifiers);
                /// <value>Creates and holds a BashTestDriver object.</value>
                BashTestDriver bashDriver = new BashTestDriver(new FileManagerService());

                // Set up and build the Bash script.
                bashDriver.Configure(configs);
                FileInfo fileInfo = bashDriver.Build();


                // Store the data between ActionResults.
                TempData["fileInfo"] = fileInfo.FullName;
                TempData["fileName"] = fileInfo.Name;
                return RedirectToAction("download");
            }
            return View("AddAssignment");
        }


        /// <summary>
        /// Returns the success view.
        /// </summary>
        /// <param name="assignment">Model holding info on an Assignment.</param>
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
            /// <value>Holds the path of the returned script.</value>
            string path = Convert.ToString(TempData["fileInfo"]);
            /// <value>Holds the name of the returned script.</value>
            string name = Convert.ToString(TempData["fileName"]);
            /// <value>Holds the content of the returned script.</value>
            string content = System.IO.File.ReadAllText(path);
            /// <value>Holds the stream of the returned script.</value>
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

            // Return the stream of the script with the given file name.
            return new FileStreamResult(stream, "text/plain")
            {
                FileDownloadName = name
            };
        }
    }
}

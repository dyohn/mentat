using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Mentat.UI.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mentat.UI.Controllers
{
    public class MentorController : Controller
    {  
        public IActionResult GradingScriptForm()
        {
            // default languages for checkboxes on form
            var languages = new List<Language>()
            {
                new Language(){ name = "C", id = 1, selected = false},
                new Language(){ name = "C++", id = 2, selected = false},
                new Language(){ name = "Python", id = 3, selected = false},
                new Language(){ name = "Java", id = 4, selected = false},

            };

            var model = new GradingScriptFormModel();
            model.languages = languages;
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateGradingScriptForm(GradingScriptFormModel gradingScriptFormModel)
        {
            // just writing to console for now
            foreach (Language language in gradingScriptFormModel.languages)
            {
                if (language.selected)
                {
                    Console.WriteLine(language.name);
                }
               
            }
            Console.WriteLine(gradingScriptFormModel.file_names);
            Console.WriteLine(gradingScriptFormModel.exec_name);
            Console.WriteLine(gradingScriptFormModel.duration);
            Console.WriteLine(gradingScriptFormModel.diff_cmd);
            return View("/Views/Home/Index.cshtml");
        }
    }
}


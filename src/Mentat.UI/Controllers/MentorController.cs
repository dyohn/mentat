using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mentat.UI.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mentat.UI.Controllers
{
    public class MentorController : Controller
    {  
        public IActionResult GradingScriptForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetFormData(string language, string file_names, string exec_name, string duration, string diff_cmd)
        {
            GradingScriptFormModel form = new GradingScriptFormModel();
            form.language = language;
            form.file_names = file_names;
            form.exec_name = exec_name;
            form.duration = duration;
            form.diff_cmd = diff_cmd;
            Console.WriteLine(form.language);
            Console.WriteLine(form.file_names);
            Console.WriteLine(form.exec_name);
            Console.WriteLine(form.duration);
            Console.WriteLine(form.diff_cmd);
            return View("/Views/Home/Index.cshtml");
        }
    }
}


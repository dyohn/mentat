using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Mentat.UI.Controllers
{
    public class GradingScriptFormController : Controller
    {
        public IActionResult GradingScriptForm()
        {
            //doesnt currently workk
            //testing push
            string test = this.Request.Form["language"];          
            Console.WriteLine(test);
            return View();
        }
    }
}


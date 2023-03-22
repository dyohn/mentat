using System;
using Microsoft.AspNetCore.Mvc;
using Mentat.UI.ViewModels;
using Mentat.UI.Controllers;
using Microsoft.Extensions.Logging;


namespace Mentat.UI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ILogger<RegisterController> _logger;


        public RegisterController(ILogger<RegisterController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            // create + return new instance of VM:
            var model = new RegisterViewModel();
            return View(model);
        }


        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            
            _logger.LogInformation("Name: {nm}, \nPW: {pw}, \nType: {type}", model.UserName, model.Password,
                model.UserType);


            if (model.UserType == 0)
            {
                return RedirectToAction("Index", "Student");
            }

            else if (model.UserType == 1)
            {
                return RedirectToAction("Index", "Mentor");
            }

            return RedirectToAction("Index", "Home"); 
        }
    }
}

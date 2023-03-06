using System;
using Microsoft.AspNetCore.Mvc;

namespace Mentat.UI.Controllers
{
    public class RegisterController : Controller
    {

        [HttpGet]
        public string Index()
        {
            return "test";
        }
    }
}

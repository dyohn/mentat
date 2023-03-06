using System;
using Microsoft.AspNetCore.Mvc;

namespace Mentat.UI.Controllers
{
    public class LoginController : Controller
    {

        [HttpGet]
        public string Index()
        {
            return "test";
        }
    }
}

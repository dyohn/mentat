using Mentat.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mentat.UI.Controllers
{
    public class StudentController : Controller
    {
        public ActionResult Index()
        {
            //todo: replace dummy data with service call when DB is available
            var vm = new FlashCardVM
            {
                CardID = 1,
                CardQuestion = "This is a test Question???",
                HiddenCardAnswer = "Yep, sure is.",
                CardAnswer = "",
                CardColor = "221,160,221"
            };
            
            return View(vm);
        }
    }
}

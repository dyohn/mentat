using Mentat.Domain.IService;
using Microsoft.AspNetCore.Mvc;

namespace Mentat.UI.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public ActionResult Index()
        {
            var vm = _studentService.GetStudentVM();
            return View(vm);
        }
    }
}

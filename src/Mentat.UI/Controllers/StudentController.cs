using Mentat.Domain.IService;
using Mentat.Domain.Models;
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

        public ActionResult Index(StudentVM studentVM = null)
        {
            var vm = _studentService.GetStudentVM(studentVM.SelectedDifficulties);
            return View(vm);
        }
    }
}

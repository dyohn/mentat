using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Mentat.Domain.IService;
using Mentat.Domain.Models;
using Mentat.UI.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using MongoDB.Driver;

namespace Mentat.UI.Controllers
{
    [Authorize(Policy = "Student")]
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

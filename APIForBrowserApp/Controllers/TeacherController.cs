using APIForBrowserApp.Models.Teacher;
using APIForBrowserApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIForBrowserApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            this.teacherService = teacherService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Teacher")]
        public IActionResult Get()
        {
            return Ok("kek");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreateTeacher(CreateTeacherRequest createTeacherRequest)
        {
            return Ok(teacherService.CreateTeacher(createTeacherRequest));
        }
    }
}

using APIForBrowserApp.Models;
using APIForBrowserApp.Models.Student;
using APIForBrowserApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIForBrowserApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService studentService;

        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost]
        public AppResult<CreateStudentResponse> CreateStudent([FromBody] CreateStudentRequest createStudentRequest)
        {
            var result = studentService.CreateStudent(createStudentRequest);
            HttpContext.Response.StatusCode = result.Status;
            return result;
        }

        [Authorize(Roles = "Admin,Teacher,Student")]
        [HttpGet("{studentId}")]
        public AppResult<GetStudentResponse> GetStudent([FromRoute] int studentId)
        {
            var result = studentService.GetStudent(studentId);
            HttpContext.Response.StatusCode = result.Status;
            return result;
        }
    }
}

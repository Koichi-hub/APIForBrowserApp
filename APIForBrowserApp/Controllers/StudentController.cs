using APIForBrowserApp.Constants;
using APIForBrowserApp.Entities;
using APIForBrowserApp.Helpers;
using APIForBrowserApp.Models;
using APIForBrowserApp.Models.Student;
using APIForBrowserApp.Models.Teacher;
using APIForBrowserApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            var user = HttpContext.User;
            if (user.IsInRole("Student") && !(user.FindFirstValue(ClaimsNames.UserId) == studentId.ToString()))
            {
                HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                return AppResultFactory.Create<GetStudentResponse>(StatusCodes.Status403Forbidden, string.Empty);
            }

            var result = studentService.GetStudent(studentId);
            HttpContext.Response.StatusCode = result.Status;
            return result;
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost("filter")]
        public AppResult<FilterStudentsResponse> FilterStudents([FromBody] FilterStudentsRequest filterStudentsRequest)
        {
            var result = studentService.FilterStudents(filterStudentsRequest);
            HttpContext.Response.StatusCode = result.Status;
            return result;
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPut]
        public AppResult<UpdateStudentResponse> UpdateStudent([FromBody] UpdateStudentRequest updateStudentRequest)
        {
            var result = studentService.UpdateStudent(updateStudentRequest);
            HttpContext.Response.StatusCode = result.Status;
            return result;
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpDelete("{studentId}")]
        public AppResult<DeleteStudentResponse> DeleteStudent([FromRoute] int studentId)
        {
            var result = studentService.DeleteStudent(studentId);
            HttpContext.Response.StatusCode = result.Status;
            return result;
        }
    }
}

using APIForBrowserApp.Constants;
using APIForBrowserApp.Helpers;
using APIForBrowserApp.Models;
using APIForBrowserApp.Models.Teacher;
using APIForBrowserApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public AppResult<CreateTeacherResponse> CreateTeacher([FromBody] CreateTeacherRequest createTeacherRequest)
        {
            var result = teacherService.CreateTeacher(createTeacherRequest);
            HttpContext.Response.StatusCode = result.Status;
            return result;
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("{teacherId}")]
        public AppResult<GetTeacherResponse> GetTeacher([FromRoute] int teacherId)
        {
            var user = HttpContext.User;
            if (user.IsInRole("Teacher") && !(user.FindFirstValue(ClaimsNames.UserId) == teacherId.ToString()))
            {
                HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                return AppResultFactory.Create<GetTeacherResponse>(StatusCodes.Status403Forbidden, string.Empty);
            }

            var result = teacherService.GetTeacherOrDefault(teacherId);
            HttpContext.Response.StatusCode = result.Status;
            return result;
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPut]
        public AppResult<UpdateTeacherResponse> UpdateTeacher([FromBody] UpdateTeacherRequest updateTeacherRequest)
        {
            var user = HttpContext.User;
            if (user.IsInRole("Teacher") && !(user.FindFirstValue(ClaimsNames.UserId) == updateTeacherRequest.UserId.ToString()))
            {
                HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                return AppResultFactory.Create<UpdateTeacherResponse>(StatusCodes.Status403Forbidden, string.Empty);
            }

            var result = teacherService.UpdateTeacher(updateTeacherRequest);
            HttpContext.Response.StatusCode = result.Status;
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{teacherId}")]
        public AppResult<DeleteTeacherResponse> DeleteTeacher([FromRoute] int teacherId)
        {
            var result = teacherService.DeleteTeacher(teacherId);
            HttpContext.Response.StatusCode = result.Status;
            return result;
        }
    }
}

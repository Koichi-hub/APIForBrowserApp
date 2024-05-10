using APIForBrowserApp.Entities;
using APIForBrowserApp.Models.Teacher;

namespace APIForBrowserApp.Services.Interfaces
{
    public interface ITeacherService
    {
        Teacher CreateTeacher(CreateTeacherRequest createTeacherRequest);
    }
}

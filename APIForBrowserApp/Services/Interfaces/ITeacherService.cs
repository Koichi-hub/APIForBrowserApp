using APIForBrowserApp.Entities;
using APIForBrowserApp.Models;
using APIForBrowserApp.Models.Teacher;

namespace APIForBrowserApp.Services.Interfaces
{
    public interface ITeacherService
    {
        AppResult<GetTeacherResponse> GetTeacherOrDefault(int teacherId);

        AppResult<CreateTeacherResponse> CreateTeacher(CreateTeacherRequest createTeacherRequest);

        AppResult<UpdateTeacherResponse> UpdateTeacher(UpdateTeacherRequest updateTeacherRequest);

        AppResult<DeleteTeacherResponse> DeleteTeacher(int teacherId);
    }
}

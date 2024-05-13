using APIForBrowserApp.Models;
using APIForBrowserApp.Models.Student;

namespace APIForBrowserApp.Services.Interfaces
{
    public interface IStudentService
    {
        AppResult<CreateStudentResponse> CreateStudent(CreateStudentRequest createStudentRequest);

        AppResult<GetStudentResponse> GetStudent(int studentId);

        AppResult<FilterStudentsResponse> FilterStudents(FilterStudentsRequest filterStudentsRequest);

        AppResult<UpdateStudentResponse> UpdateStudent(UpdateStudentRequest updateStudentRequest);

        AppResult<DeleteStudentResponse> DeleteStudent(int studentId);
    }
}

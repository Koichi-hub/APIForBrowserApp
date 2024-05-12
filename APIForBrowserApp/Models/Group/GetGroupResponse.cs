using APIForBrowserApp.Models.Student;
using APIForBrowserApp.Models.Teacher;

namespace APIForBrowserApp.Models.Group
{
    public class GetGroupResponse
    {
        public int Id { get; set; }

        public int Grade { get; set; }

        public List<GetStudentResponse> Students { get; set; } = new List<GetStudentResponse>();

        public List<GetTeacherResponse> Teachers { get; set; } = new List<GetTeacherResponse>();
    }
}

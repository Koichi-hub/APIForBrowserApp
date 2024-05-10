using APIForBrowserApp.Entities;
using APIForBrowserApp.Services.Interfaces;

namespace APIForBrowserApp.Services
{
    public class StudentService : IStudentService
    {
        public Student CreateStudent()
        {
            return new Student();
        }
    }
}

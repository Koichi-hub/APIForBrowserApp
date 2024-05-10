using APIForBrowserApp.Database;
using APIForBrowserApp.Entities;
using APIForBrowserApp.Models.Teacher;
using APIForBrowserApp.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace APIForBrowserApp.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly DatabaseContext databaseContext;

        public TeacherService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Teacher CreateTeacher(CreateTeacherRequest createTeacherRequest)
        {
            var md5 = MD5.Create();
            var hashedPassword = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(createTeacherRequest.Password))).Replace("-", "");

            var user = new User
            {
                Login = createTeacherRequest.Login,
                Password = hashedPassword,
                Role = Enums.RolesEnum.Teacher
            };
            databaseContext.Users.Add(user);
            databaseContext.SaveChanges();

            var teacher = new Teacher
            {
                UserId = user.Id,
                FirstName = createTeacherRequest.FirstName,
                LastName = createTeacherRequest.LastName,
                Age = createTeacherRequest.Age
            };
            databaseContext.Teachers.Add(teacher);
            databaseContext.SaveChanges();

            return teacher;
        }
    }
}

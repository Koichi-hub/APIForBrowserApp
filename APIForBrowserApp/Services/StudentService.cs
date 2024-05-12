using APIForBrowserApp.Database;
using APIForBrowserApp.Entities;
using APIForBrowserApp.Helpers;
using APIForBrowserApp.Models;
using APIForBrowserApp.Models.Student;
using APIForBrowserApp.Services.Interfaces;
using AutoMapper;

namespace APIForBrowserApp.Services
{
    public class StudentService : IStudentService
    {
        private readonly DatabaseContext databaseContext;
        private readonly IMapper mapper;

        public StudentService(
            DatabaseContext databaseContext,
            IMapper mapper
        )
        {
            this.databaseContext = databaseContext;
            this.mapper = mapper;
        }

        public AppResult<CreateStudentResponse> CreateStudent(CreateStudentRequest createStudentRequest)
        {
            var result = AppResultFactory.Create<CreateStudentResponse>();

            var user = new User
            {
                Login = createStudentRequest.Login,
                Password = UserPasswordHelper.HashPassword(createStudentRequest.Password),
                Role = Enums.RolesEnum.Student
            };
            databaseContext.Users.Add(user);
            databaseContext.SaveChanges();

            var student = new Student
            {
                UserId = user.Id,
                FirstName = createStudentRequest.FirstName,
                LastName = createStudentRequest.LastName,
                Age = createStudentRequest.Age
            };
            databaseContext.Students.Add(student);
            databaseContext.SaveChanges();

            result.Data = mapper.Map<CreateStudentResponse>(student);
            return result;
        }

        public AppResult<GetStudentResponse> GetStudent(int studentId)
        {
            var result = AppResultFactory.Create<GetStudentResponse>();

            var student = databaseContext.Students.FirstOrDefault(x => x.UserId == studentId);
            if (student is null)
            {
                result.Status = StatusCodes.Status404NotFound;
                result.Message = $"student is not found, studentId = {studentId}";
                return result;
            }

            result.Data = mapper.Map<GetStudentResponse>(student);
            return result;
        }
    }
}

using APIForBrowserApp.Database;
using APIForBrowserApp.Entities;
using APIForBrowserApp.Helpers;
using APIForBrowserApp.Models;
using APIForBrowserApp.Models.Student;
using APIForBrowserApp.Models.Teacher;
using APIForBrowserApp.Services.Interfaces;
using AutoMapper;

namespace APIForBrowserApp.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly DatabaseContext databaseContext;
        private readonly IMapper mapper;

        public TeacherService(
            DatabaseContext databaseContext,
            IMapper mapper
        )
        {
            this.databaseContext = databaseContext;
            this.mapper = mapper;
        }

        public AppResult<GetTeacherResponse> GetTeacherOrDefault(int teacherId)
        {
            var result = AppResultFactory.Create<GetTeacherResponse>();

            var teacher = databaseContext.Teachers.FirstOrDefault(x => x.UserId == teacherId);
            if (teacher is null)
            {
                result.Status = StatusCodes.Status404NotFound;
                result.Message = $"teacher is not found, teacherId = {teacherId}";
                return result;
            }

            result.Data = mapper.Map<GetTeacherResponse>(teacher);
            return result;
        }

        public AppResult<CreateTeacherResponse> CreateTeacher(CreateTeacherRequest createTeacherRequest)
        {
            var result = AppResultFactory.Create<CreateTeacherResponse>();

            var isLoginExisted = databaseContext.Users.Any(x => x.Login == createTeacherRequest.Login);
            if (isLoginExisted)
            {
                result.Status = StatusCodes.Status400BadRequest;
                result.Message = "login is already taken";
                return result;
            }

            var user = new User
            {
                Login = createTeacherRequest.Login,
                Password = UserPasswordHelper.HashPassword(createTeacherRequest.Password),
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

            result.Data = mapper.Map<CreateTeacherResponse>(teacher);
            return result;
        }

        public AppResult<UpdateTeacherResponse> UpdateTeacher(UpdateTeacherRequest updateTeacherRequest)
        {
            var result = AppResultFactory.Create<UpdateTeacherResponse>();

            var teacher = databaseContext.Teachers.FirstOrDefault(x => x.UserId == updateTeacherRequest.UserId);
            if (teacher is null)
            {
                result.Status = StatusCodes.Status404NotFound;
                result.Message = $"teacher is not found, teacherId = {updateTeacherRequest.UserId}";
                return result;
            }

            mapper.Map(updateTeacherRequest, teacher);
            databaseContext.Teachers.Update(teacher);
            databaseContext.SaveChanges();
            result.Data = mapper.Map<UpdateTeacherResponse>(teacher);
            return result;
        }

        public AppResult<DeleteTeacherResponse> DeleteTeacher(int teacherId)
        {
            var result = AppResultFactory.Create<DeleteTeacherResponse>();
            var teacher = databaseContext.Teachers.FirstOrDefault(x => x.UserId == teacherId);
            if (teacher is null)
            {
                result.Status = StatusCodes.Status404NotFound;
                result.Message = $"teacher is not found, teacherId = {teacherId}";
                return result;
            }
            teacher.IsDeleted = true;
            teacher.DeletedAt = DateTime.UtcNow;
            databaseContext.Teachers.Update(teacher);
            databaseContext.SaveChanges();
            result.Data = mapper.Map<DeleteTeacherResponse>(teacher);
            return result;
        }
    }
}

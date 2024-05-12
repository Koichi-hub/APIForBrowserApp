using APIForBrowserApp.Entities;
using APIForBrowserApp.Models.Student;
using APIForBrowserApp.Models.Teacher;
using AutoMapper;

namespace APIForBrowserApp.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            TeacherMapping();
            StudentMapping();
            GroupMapping();
        }

        private void TeacherMapping()
        {
            //create
            CreateMap<Teacher, CreateTeacherResponse>();
            //get
            CreateMap<Teacher, GetTeacherResponse>();
            //update
            CreateMap<UpdateTeacherRequest, Teacher>();
            CreateMap<Teacher, UpdateTeacherResponse>();
        }

        private void StudentMapping()
        {
            //create
            CreateMap<Student, CreateStudentResponse>();
            //get
            CreateMap<Student, GetStudentResponse>();
        }

        private void GroupMapping()
        {
            //create
            CreateMap<Group, CreateStudentResponse>();
        }
    }
}

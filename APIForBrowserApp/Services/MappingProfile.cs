using APIForBrowserApp.Entities;
using APIForBrowserApp.Models;
using APIForBrowserApp.Models.Group;
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

            CreateMap<User, LoginResponse>();
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
            //delete
            CreateMap<Teacher, DeleteTeacherResponse>();
        }

        private void StudentMapping()
        {
            //create
            CreateMap<Student, CreateStudentResponse>();
            //get
            CreateMap<Student, GetStudentResponse>();
            //update
            CreateMap<UpdateStudentRequest, Student>();
            CreateMap<Student, UpdateStudentResponse>();
            //delete
            CreateMap<Student, DeleteStudentResponse>();
        }

        private void GroupMapping()
        {
            //create
            CreateMap<Group, CreateGroupResponse>();
            //get
            CreateMap<Group, GetGroupResponse>();
            //update
            CreateMap<UpdateGroupRequest, Group>();
            CreateMap<Group, UpdateGroupResponse>();
        }
    }
}

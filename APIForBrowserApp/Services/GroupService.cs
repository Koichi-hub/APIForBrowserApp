using APIForBrowserApp.Database;
using APIForBrowserApp.Helpers;
using APIForBrowserApp.Models;
using APIForBrowserApp.Models.Group;
using APIForBrowserApp.Services.Interfaces;
using AutoMapper;

namespace APIForBrowserApp.Services
{
    public class GroupService : IGroupService
    {
        private readonly DatabaseContext databaseContext;
        private readonly IMapper mapper;

        public GroupService(
            DatabaseContext databaseContext,
            IMapper mapper
        )
        {
            this.databaseContext = databaseContext;
            this.mapper = mapper;
        }

        public AppResult<CreateGroupResponse> CreateGroup(CreateGroupRequest createGroupRequest)
        {
            var result = AppResultFactory.Create<CreateGroupResponse>();

            var group = new Entities.Group
            {
                Grade = createGroupRequest.Grade,
            };
            databaseContext.Groups.Add(group);
            databaseContext.SaveChanges();

            result.Data = mapper.Map<CreateGroupResponse>(group);
            return result;
        }

        public AppResult<GetGroupResponse> GetGroup(int groupId)
        {
            var result = AppResultFactory.Create<GetGroupResponse>();

            var group = databaseContext.Groups.FirstOrDefault(x => x.Id == groupId);
            if (group is null)
            {
                result.Status = StatusCodes.Status404NotFound;
                result.Message = $"group is not found, groupId = {groupId}";
                return result;
            }

            result.Data = mapper.Map<GetGroupResponse>(group);
            return result;
        }

        public AppResult<UpdateGroupResponse> UpdateGroup(UpdateGroupRequest updateGroupRequest)
        {
            var result = AppResultFactory.Create<UpdateGroupResponse>();

            var group = databaseContext.Groups.FirstOrDefault(x => x.Id == updateGroupRequest.Id);
            if (group is null)
            {
                result.Status = StatusCodes.Status404NotFound;
                result.Message = $"group is not found, groupId = {updateGroupRequest.Id}";
                return result;
            }

            mapper.Map(updateGroupRequest, group);
            databaseContext.Groups.Update(group);
            databaseContext.SaveChanges();
            result.Data = mapper.Map<UpdateGroupResponse>(group);
            return result;
        }

        public AppResult<object> DeleteGroup(int groupId)
        {
            var result = AppResultFactory.Create();
            databaseContext.Groups.Remove(new Entities.Group { Id = groupId });
            databaseContext.SaveChanges();
            return result;
        }
    }
}

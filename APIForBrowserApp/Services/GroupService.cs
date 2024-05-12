using APIForBrowserApp.Database;
using APIForBrowserApp.Entities;
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

            var group = new Group
            {
                Grade = createGroupRequest.Grade,
            };
            databaseContext.Groups.Add(group);
            databaseContext.SaveChanges();

            result.Data = mapper.Map<CreateGroupResponse>(group);
            return result;
        }
    }
}

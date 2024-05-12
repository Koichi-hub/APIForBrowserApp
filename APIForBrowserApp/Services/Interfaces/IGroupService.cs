using APIForBrowserApp.Models;
using APIForBrowserApp.Models.Group;

namespace APIForBrowserApp.Services.Interfaces
{
    public interface IGroupService
    {
        AppResult<CreateGroupResponse> CreateGroup(CreateGroupRequest createGroupRequest);

        AppResult<GetGroupResponse> GetGroup(int groupId);

        AppResult<UpdateGroupResponse> UpdateGroup(UpdateGroupRequest updateGroupRequest);

        AppResult<object> DeleteGroup(int groupId);
    }
}

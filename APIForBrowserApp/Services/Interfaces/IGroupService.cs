using APIForBrowserApp.Models;
using APIForBrowserApp.Models.Group;

namespace APIForBrowserApp.Services.Interfaces
{
    public interface IGroupService
    {
        AppResult<CreateGroupResponse> CreateGroup(CreateGroupRequest createGroupRequest);
    }
}

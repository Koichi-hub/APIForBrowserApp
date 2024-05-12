using APIForBrowserApp.Models;
using APIForBrowserApp.Models.Group;
using APIForBrowserApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIForBrowserApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService groupService;

        public GroupController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost]
        public AppResult<CreateGroupResponse> CreateGroup([FromBody] CreateGroupRequest createGroupRequest)
        {
            var result = groupService.CreateGroup(createGroupRequest);
            HttpContext.Response.StatusCode = result.Status;
            return result;
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("{groupId}")]
        public AppResult<GetGroupResponse> GetGroup([FromRoute] int groupId)
        {
            var result = groupService.GetGroup(groupId);
            HttpContext.Response.StatusCode = result.Status;
            return result;
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPut]
        public AppResult<UpdateGroupResponse> UpdateGroup([FromBody] UpdateGroupRequest updateGroupRequest)
        {
            var result = groupService.UpdateGroup(updateGroupRequest);
            HttpContext.Response.StatusCode = result.Status;
            return result;
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpDelete("{groupId}")]
        public AppResult<object> DeleteGroup([FromRoute] int groupId)
        {
            var result = groupService.DeleteGroup(groupId);
            HttpContext.Response.StatusCode = result.Status;
            return result;
        }
    }
}

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
    }
}

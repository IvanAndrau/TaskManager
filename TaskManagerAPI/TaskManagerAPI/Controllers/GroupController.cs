using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskManagerAPI.AppServices;
using TaskManagerAPI.ViewModel;

namespace TaskManagerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class GroupController(GroupService groupService) : ControllerBase
{
    [HttpGet]
    public IEnumerable<GroupModel> Get()
    {
        return groupService.GetAllGroups();
    }

    [HttpGet("{id:guid}")]
    public GroupModel GetGroup([FromRoute] Guid id)
    {
        return groupService.GetGroupById(id);
    }

    [HttpPut("{id:guid}")]
    public GroupModel CreateGroup([FromRoute] Guid id, [FromBody] GroupModel group)
    {
        return groupService.AddGroup(group);
    }

    [HttpPost("{id:guid}")]
    [ProducesResponseType(typeof(GroupModel), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public IActionResult UpdateGroup([FromRoute] Guid id, [FromBody] GroupModel group)
    {
        try
        {
            groupService.UpdateGroup(group);
        }
        catch (Exception ex)
        {
            return NotFound();
        }
        return Ok(group);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public IActionResult DeleteGroup([FromRoute] Guid id)
    {
        try
        {
            groupService.DeleteGroup(id);
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok();
    }
}

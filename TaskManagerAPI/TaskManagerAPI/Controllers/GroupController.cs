using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskManagerAPI.AppServices;
using TaskManagerAPI.EF;
using TaskManagerAPI.ViewModel;

namespace TaskManagerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class GroupController(GroupService groupService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public IActionResult Get()
    {
        IEnumerable<GroupModel> groups = new List<GroupModel>();
        try
        {
            var userId = HttpContextHelper.GetUserId(User);
            groups = groupService.GetAllGroups(userId);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok(groups);

    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GroupModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public IActionResult GetGroup([FromRoute] Guid id)
    {
        var userId = HttpContextHelper.GetUserId(User);
        var group = groupService.GetGroupById(id, userId);
        if (group == null) return NotFound();
        return Ok(group);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GroupModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public IActionResult CreateGroup([FromBody] GroupModel group)
    {
        var userId = HttpContextHelper.GetUserId(User);
        var result = groupService.AddGroup(group, userId);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(GroupModel), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public IActionResult UpdateGroup([FromBody] GroupModel group)
    {
        try
        {
            var userId = HttpContextHelper.GetUserId(User);
            groupService.UpdateGroup(group, userId);
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
            var userId = HttpContextHelper.GetUserId(User);
            groupService.DeleteGroup(id, userId);
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

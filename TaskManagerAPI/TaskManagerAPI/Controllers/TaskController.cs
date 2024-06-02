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
public class TaskController(TaskService taskService) : ControllerBase
{
    [HttpGet("get-by-groupId")]
    public IEnumerable<TaskModel> Get([FromQuery] Guid groupId)
    {
        return taskService.GetTasksByGroupId(groupId);
    }

    [HttpGet("{id:guid}")]
    public TaskModel GetTask([FromRoute] Guid id)
    {
        return taskService.GetTaskById(id);
    }

    [HttpPut("{id:guid}")]
    public TaskModel CreateTask([FromRoute] Guid id, [FromBody] TaskModel task)
    {
        return taskService.AddTask(task);
    }

    [HttpPost("{id:guid}")]
    [ProducesResponseType(typeof(TaskModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public IActionResult UpdateTask([FromRoute] Guid id, [FromBody] TaskModel task)
    {
        try
        {
            taskService.UpdateTask(task);
        }
        catch (Exception ex)
        {
            return NotFound();
        }
        return Ok(task);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public IActionResult DeleteTask([FromRoute] Guid id)
    {
        try
        {
            taskService.DeleteTask(id);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok();
    }
}

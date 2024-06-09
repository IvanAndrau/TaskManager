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
public class TaskController(TaskService taskService) : ControllerBase   //TaskControllerBase : ControllerBase
{
    [HttpGet("get-by-groupId")]
    [ProducesResponseType(typeof(TaskModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public IActionResult Get([FromQuery] Guid groupId)
    {
        var userId = HttpContextHelper.GetUserId(User);
        var group = taskService.GetTasksByGroupId(groupId, userId);
        if(group == null)   //
        {
            return NotFound();
        }
        return Ok(group);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public IActionResult GetTask([FromRoute] Guid id)
    {
        TaskModel newTask = new TaskModel();
        try
        {
            var userId = HttpContextHelper.GetUserId(User);
            newTask = taskService.GetTaskById(id, userId);
        }
        catch (Exception ex)
        {
            return NotFound();
        }
        return Ok(newTask);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public IActionResult CreateTask([FromBody] TaskModel task)
    {
        try
        {
            var userId = HttpContextHelper.GetUserId(User);
            var taskCreated = taskService.AddTask(task, userId);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok();
    }

    [HttpPut]
    [ProducesResponseType(typeof(TaskModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public IActionResult UpdateTask([FromBody] TaskModel task)
    {
        try
        {
            var userId = HttpContextHelper.GetUserId(User);
            taskService.UpdateTask(task, userId);
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
            var userId = HttpContextHelper.GetUserId(User);
            taskService.DeleteTask(id, userId);
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

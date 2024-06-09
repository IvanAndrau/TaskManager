using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskManagerAPI.AppServices;
using TaskManagerAPI.EF;
using TaskManagerAPI.ViewModel;
using System.Text.RegularExpressions;

namespace TaskManagerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TaskControllerBase : ControllerBase
{
     //public string userId = HttpContextHelper.GetUserId(User);
}

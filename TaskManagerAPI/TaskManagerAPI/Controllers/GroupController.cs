using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        [HttpGet(Name = "GetGroups")]
        public IEnumerable<string> Get()
        {
            var groups = new[] { "IvanGr", "LarryGr", "PavelGr" };
            return groups;
        }

        [HttpPut(Name = "AddGroups")]
        public IEnumerable<string> AddGroups()
        {
            var groups = new[] { "IvanGr", "LarryGr", "PavelGr" };
            return groups;
        }
    }
}

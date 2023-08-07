using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskManagerController : ControllerBase
    {
        [HttpGet]
        public string Get() 
        {
            return "Hello Task Manager";
        }
    }
}

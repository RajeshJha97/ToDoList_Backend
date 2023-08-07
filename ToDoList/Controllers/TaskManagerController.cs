using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskManagerController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private APIResponse _resp;
        private IMapper _mapper;

        public TaskManagerController(ApplicationDbContext db,  IMapper mapper)
        {
            _db = db;
            _resp = new();
            _mapper = mapper;

        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> GetTasks() 
        {
            var data = await _db.TaskLists.ToListAsync();

            if (data == null)
            {
                _resp.StatusCode = HttpStatusCode.NotFound;
                _resp.Message = "No Data is available";
                return NotFound(_resp);
            }
            _resp.StatusCode=HttpStatusCode.OK;
            _resp.isSuccess=true;
            _resp.Result= data;
            return Ok(_resp);

        }
    }
}

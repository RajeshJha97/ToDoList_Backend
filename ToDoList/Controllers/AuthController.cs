using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoList.DTO;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private string secretKey;
        private readonly IMapper _mapper; 

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,IConfiguration config, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;  
            _mapper = mapper;
            secretKey=config.GetValue<string>("secretKey");
        }

        [HttpGet]
        public IActionResult Get() 
        {
            return Ok(secretKey);
        }

        [HttpPost(Name = "Signup")]
        public async Task<ActionResult<APIResponse>> Signup([FromBody] SignUpDTO signup)
        {
            return Ok();
        }


    }
}

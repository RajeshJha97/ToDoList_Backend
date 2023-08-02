using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using ToDoList.DTO;
using ToDoList.Models;
using ToDoList.Models.Accounts;
using ToDoList.Utilities;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region variables
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private string secretKey;
        private readonly IMapper _mapper;
        private APIResponse _resp;
        private Token token;
        #endregion

        #region Constructor
        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,IConfiguration config, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;  
            _mapper = mapper;
            _resp = new();
            secretKey=config.GetValue<string>("secretKey");

        }

        #endregion

        #region KeyTesting
        [HttpGet]
        public IActionResult Get() 
        {
            return Ok(secretKey);
        }

        #endregion

        #region UserRegistration

        [HttpPost("SignUp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Signup([FromBody] SignUpDTO signup)
        {
            try
            {
                if (signup == null || (signup.Password != signup.ConfirmPassword))
                {
                    _resp.StatusCode = HttpStatusCode.BadRequest;
                    if (signup == null)
                    {
                        _resp.Error = new { Message = "Details required" };
                    }
                    else
                    {
                        _resp.Error = new { Message = "Password and Confirm Password must be same." };
                    }
                    return BadRequest(_resp);
                }
                var user = new IdentityUser
                {
                    UserName = signup.UserName,
                    Email = signup.Email,
                };

                var result = await _userManager.CreateAsync(user, signup.Password);
                if (result.Succeeded)
                {
                    _resp.StatusCode = HttpStatusCode.Created;
                    _resp.Message = $"{signup.UserName} has been created successfully";
                    return Created("", _resp);

                }
                _resp.StatusCode = HttpStatusCode.BadRequest;
                _resp.Error = new { Message = result.Errors };
                return BadRequest(_resp);

            }

            catch (Exception ex)
            {
                _resp.StatusCode = HttpStatusCode.BadRequest;
                _resp.Error = new { Message = ex.Message };
                return BadRequest(_resp);
            }

        }

        #endregion

        #region SignIn
        [HttpPost("SignIn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> SignIn(SignIn signIn)
        {
            try
            {
                if (signIn.Email == null || signIn.Password == null)
                {
                    _resp.StatusCode = HttpStatusCode.BadRequest;
                    _resp.Error = new { Message = "Please enter username and password" };
                    return BadRequest(_resp);
                }

                //signing in user 
                var result = await _signInManager.PasswordSignInAsync(signIn.Email, signIn.Password, false, false);
                if (result.Succeeded)
                {
                    List<Claim> claims = new()
                    {
                        new Claim (ClaimTypes.Name, signIn.Email ),
                        new Claim(ClaimTypes.Email,signIn.Email)

                    };
                    var expiresAt = DateTime.Now.AddMinutes(10);
                    _resp.Result = new
                    {
                        Tokens = token.GenerateToken(claims, expiresAt, secretKey),
                        Expires = expiresAt
                    };
                    return Ok(_resp);
                }

                _resp.StatusCode = HttpStatusCode.Unauthorized;
                _resp.Error = new { Message = "Please enter a valid username and password" };
                return Unauthorized(_resp);
            }

            catch (Exception ex)
            {
                _resp.StatusCode = HttpStatusCode.BadRequest;
                _resp.Error = new { Message = ex.Message };
                return BadRequest(_resp);
            }

        }

        #endregion

    }
}

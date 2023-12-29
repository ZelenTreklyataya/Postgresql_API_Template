using Microsoft.AspNetCore.Mvc;
using Postgresql_API_Template.Entities.Dto;
using Postgresql_API_Template.Services.Handler;

namespace Postgresql_API_Template.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserHandler _userHandler;
        public AuthController(IConfiguration configuration, IUserHandler userHandler)
        {
            _configuration = configuration;
            _userHandler = userHandler;
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto request)
        {
            try
            {
                var result = await _userHandler.DoLogin(request);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("registration")]
        public async Task<ActionResult<string>> Registration(UserRegistrationDto request)
        {
            try
            {
                var result = await _userHandler.DoRegistration(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

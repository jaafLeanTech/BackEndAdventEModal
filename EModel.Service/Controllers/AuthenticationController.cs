using AutoMapper;
using EModel.Contracts.Repository;
using EModel.Core.V1;
using EModel.Entities.DTOs;
using EModel.Entities.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EModel.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationCore _core;

        public AuthenticationController(ILogger<User> userLogger, IMapper mapper, IUserRepository userRepository, IConfiguration configuration)
        {
            _core = new(userRepository, userLogger, mapper, configuration);
        }
        // POST api/<AuthenticationController>
        [HttpPost]
        public async Task<ActionResult<UserLoginDto>> Login([FromBody] UserLoginRequestDto request)
        {
            var response = await _core.AuthUser(request);
            return StatusCode((int)response.StatusHttp, response);

        }

        [HttpPost("password")]
        public async Task<ActionResult<bool>> AddPassword([FromBody] UserLoginRequestDto request)
        {
            var response = await _core.AddPassword(request);
            return StatusCode((int)response.StatusHttp, response);
        }

        [HttpPut("password")]
        public async Task<ActionResult<bool>> ResetPassword([FromBody] UserPasswordChangeDto request)
        {
            var response = await _core.ResetPassword(request);
            return StatusCode((int)response.StatusHttp, response);
        }

    }
}

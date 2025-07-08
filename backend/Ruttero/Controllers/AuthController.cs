using Microsoft.AspNetCore.Mvc;
using Ruttero.Dtos.Auth;
using Ruttero.Interfaces.Services;
using Ruttero.Models;
using Ruttero.Services;

// Login / SignUp
namespace Ruttero.Controllers
{
    // SignUp
    [ApiController]
    [Route("api/signup")]
    public class SignUpController : ControllerBase
    {
        private readonly IAuthService _iAuthService;

        public SignUpController(IAuthService iAuthService)
        {
            _iAuthService = iAuthService;
        }

        [HttpPost]
        public async Task<ActionResult<SignUpResponseDto>> Post([FromBody] SignUpRequestDto requestDto)
        {
            if (string.IsNullOrWhiteSpace(requestDto.Username) ||
            string.IsNullOrWhiteSpace(requestDto.Email) ||
            string.IsNullOrWhiteSpace(requestDto.Password))
            {
                return BadRequest("Complete todos los campos");
            }

            var responseDto = await _iAuthService.SignUpAsync(requestDto);

            if (!responseDto.Success)
                return BadRequest(responseDto.Message);

            return Ok(responseDto);
        }
    }

    // Login
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService _iAuthService;

        public LoginController(IAuthService iAuthService)
        {
            _iAuthService = iAuthService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LoginRequestDto requestDto)
        {
            if (string.IsNullOrWhiteSpace(requestDto.Username) || string.IsNullOrWhiteSpace(requestDto.Password))
                return BadRequest("Complete todos los campos");

            var responseDto = await _iAuthService.LoginAsync(requestDto);

            if (responseDto == null)
                return Unauthorized("Usuario o contraseña incorrectos.");

            return Ok(responseDto);
        }
    }
}
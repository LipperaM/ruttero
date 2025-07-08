using Microsoft.AspNetCore.Mvc;
using Ruttero.Dtos.Drivers;
using Ruttero.Interfaces.Services;
using Ruttero.Models;

namespace Ruttero.Controllers
{
    // SignUp
    [ApiController]
    [Route("api/drivers")]
    public class DriversController : ControllerBase
    {
        private readonly IDriverService _iDriverService;

        public DriversController(IDriverService iDriverService)
        {
            _iDriverService = iDriverService;
        }

        [HttpPost]
        public async Task<ActionResult<DriverResponseDto>> Post([FromBody] CreateDriverRequestDto requestDto)
        {
            if (string.IsNullOrWhiteSpace(requestDto.Name) ||
            string.IsNullOrWhiteSpace(requestDto.Surname) ||
            string.IsNullOrWhiteSpace(requestDto.NationalId))
            {
                return BadRequest("Complete todos los campos");
            }

            var responseDto = await _iDriverService.CreateDriverAsync(requestDto);

            if (!responseDto.Success)
                return BadRequest(responseDto.Message);

            return Ok(responseDto);
        }

        [HttpPatch]
        public async Task<ActionResult<DriverResponseDto>> Patch([FromBody] UpdateDriverRequestDto requestDto)
        {
            if (requestDto.Id <= 0)
                return BadRequest("ID inválido.");

            var responseDto = await _iDriverService.UpdateDriverAsync(requestDto);

            if (!responseDto.Success)
                return NotFound(responseDto.Message);

            return Ok(responseDto);
        }
    }
}
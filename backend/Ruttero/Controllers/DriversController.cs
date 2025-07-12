/*
| Acción                | Método  | Ruta                |
| --------------------- | ------  | --------------------|
| Obtener por ID        | GET     | `/api/drivers/{id}` |
*/

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Ruttero.Dtos.Drivers;
using Ruttero.Interfaces.Services;
using Ruttero.Models;

namespace Ruttero.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/drivers")]
    public class DriversController : ControllerBase
    {
        private readonly IDriverService _iDriverService;

        public DriversController(IDriverService iDriverService)
        {
            _iDriverService = iDriverService;
        }

        // Create a driver
        [HttpPost]
        public async Task<ActionResult<DriverResponseDto>> Post([FromBody] CreateDriverRequestDto requestDto)
        {
            // Find userId in JWT token claims
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userIdString == null)
                return Unauthorized();

            int userId = int.Parse(userIdString);

            if (string.IsNullOrWhiteSpace(requestDto.Name) ||
            string.IsNullOrWhiteSpace(requestDto.Surname) ||
            string.IsNullOrWhiteSpace(requestDto.NationalId))
            {
                return BadRequest("Complete todos los campos");
            }

            var responseDto = await _iDriverService.CreateDriverAsync(requestDto, userId);

            if (!responseDto.Success)
                return BadRequest(responseDto.Message);

            return Ok(responseDto);
        }

        // Update a driver status
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

        // Get all drivers created by the user
        [HttpGet]
        public async Task<ActionResult<GetAllDriversDto>> Get()
        {
            // Find userId in JWT token claims
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userIdString == null)
                return Unauthorized();

            int userId = int.Parse(userIdString);

            var responseDto = await _iDriverService.GetAllDriversAsync(userId);

            return Ok(responseDto);
        }
    }
}
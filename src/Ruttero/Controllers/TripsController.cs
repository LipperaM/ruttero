/*
| Acción                | Método  | Ruta              |
| --------------------- | ------  | ------------------|
| Obtener por ID        | GET     | `/api/trips/{id}` |
*/

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ruttero.Dtos.Trips;
using Ruttero.Interfaces.Services;

namespace Ruttero.Controllers
{
    [Authorize]
    [ApiController]
    [Route("trips")]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _iTripService;

        public TripsController(ITripService iTripService)
        {
            _iTripService = iTripService;
        }

        // Create a trip
        [HttpPost]
        public async Task<ActionResult<TripResponseDto>> Post([FromBody] CreateTripRequestDto requestDto)
        {
            // Find userId in JWT token claims
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userIdString == null)
                return Unauthorized();

            int userId = int.Parse(userIdString);

            if (requestDto.DriverId <= 0 ||
            requestDto.VehicleId <= 0 ||
            requestDto.FareId <= 0 ||
            string.IsNullOrWhiteSpace(requestDto.Origin) ||
            string.IsNullOrWhiteSpace(requestDto.Destination) ||
            requestDto.Date == DateTime.MinValue)
            {
                return BadRequest("Complete todos los campos");
            }

            var responseDto = await _iTripService.CreateTripAsync(requestDto, userId);

            if (!responseDto.Success)
                return BadRequest(responseDto.Message);

            return Ok(responseDto);
        }

        // Update a trip status
        [HttpPatch]
        public async Task<ActionResult<TripResponseDto>> Patch([FromBody] UpdateTripRequestDto requestDto)
        {
            if (requestDto.Id <= 0)
                return BadRequest("ID inválido.");

            var responseDto = await _iTripService.UpdateTripAsync(requestDto);

            if (!responseDto.Success)
                return NotFound(responseDto.Message);

            return Ok(responseDto);
        }

        // Get all trips created by the user
        [HttpGet]
        public async Task<ActionResult<GetAllTripsDto>> Get()
        {
            // Find userId in JWT token claims
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userIdString == null)
                return Unauthorized();

            int userId = int.Parse(userIdString);

            var responseDto = await _iTripService.GetAllTripsAsync(userId);

            return Ok(responseDto);
        }
    }
}
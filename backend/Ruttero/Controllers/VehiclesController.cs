/*
| Acción                | Método  | Ruta                 |
| --------------------- | ------  | -------------------- |
| Obtener todos         | GET     | `/api/vehicles`      |
| Obtener por ID        | GET     | `/api/vehicles/{id}` |
| Crear uno             | POST    | `/api/vehicles`      |
| Eliminar uno          | DELETE  | `/api/vehicles/{id}` |
| (Opcional) Actualizar |PUT/PATCH| `/api/vehicles/{id}` |
*/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ruttero.Dtos.Vehicles;
using Ruttero.Models;
using Ruttero.Interfaces.Services;
using System.Security.Claims;

namespace Ruttero.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/vehicles")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _iVehicleService;

        public VehiclesController(IVehicleService iVehicleService)
        {
            _iVehicleService = iVehicleService;
        }

        // Create a vehicle
        [HttpPost]
        public async Task<ActionResult<VehicleResponseDto>> Post([FromBody] CreateVehicleRequestDto requestDto)
        {
            // Find userId in JWT token claims
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userIdString == null)
                return Unauthorized();

            int userId = int.Parse(userIdString);

            if (string.IsNullOrWhiteSpace(requestDto.LicensePlate) ||
            string.IsNullOrWhiteSpace(requestDto.Model) ||
            string.IsNullOrWhiteSpace(requestDto.Brand))
            {
                return BadRequest("Complete todos los campos");
            }

            var responseDto = await _iVehicleService.CreateVehicleAsync(requestDto, userId);

            if (!responseDto.Success)
                return BadRequest(responseDto.Message);

            return Ok(responseDto);
        }

        // Update a vehicle status
        [HttpPatch]
        public async Task<ActionResult<VehicleResponseDto>> Patch([FromBody] UpdateVehicleRequestDto requestDto)
        {
            if (requestDto.Id <= 0)
                return BadRequest("ID inválido.");

            var responseDto = await _iVehicleService.UpdateVehicleAsync(requestDto);

            if (!responseDto.Success)
                return NotFound(responseDto.Message);

            return Ok(responseDto);
        }
    }
}
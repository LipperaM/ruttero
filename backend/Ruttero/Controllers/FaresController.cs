/*
| Acción                | Método  | Ruta                 |
| --------------------- | ------  | -------------------- |
| Obtener todos         | GET     | `/api/fares`      |
| Obtener por ID        | GET     | `/api/fares/{id}` |
| Crear uno             | POST    | `/api/fares`      |
| Eliminar uno          | DELETE  | `/api/fares/{id}` |
| Actualizar            |PUT/PATCH| `/api/fares/{id}` |
*/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ruttero.Dtos.Fares;
using Ruttero.Interfaces.Services;

namespace Ruttero.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/fares")]
    public class FaresController : ControllerBase
    {
        private readonly IFareService _iFareService;

        public FaresController(IFareService iFareService)
        {
            _iFareService = iFareService;
        }

        // Create a fare
        [HttpPost]
        public async Task<ActionResult<FareResponseDto>> Post([FromBody] CreateFareRequestDto requestDto)
        {
            if (string.IsNullOrWhiteSpace(requestDto.Description) ||
            requestDto.Price <= 0)
            {
                return BadRequest("Complete todos los campos");
            }

            var responseDto = await _iFareService.CreateFareAsync(requestDto);

            if (!responseDto.Success)
                return BadRequest(responseDto.Message);

            return Ok(responseDto);
        }

        // Update a fare status
        [HttpPatch]
        public async Task<ActionResult<FareResponseDto>> Patch([FromBody] UpdateFareRequestDto requestDto)
        {
            if (requestDto.Id <= 0)
                return BadRequest("ID inválido.");

            var responseDto = await _iFareService.UpdateFareAsync(requestDto);

            if (!responseDto.Success)
                return NotFound(responseDto.Message);

            return Ok(responseDto);
        }
    }
}
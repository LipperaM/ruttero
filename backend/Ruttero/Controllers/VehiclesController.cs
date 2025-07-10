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
//using Ruttero.Dtos.Vehicles;
using Ruttero.Models;
using Ruttero.Services;

namespace Ruttero.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/vehicles")]
    public class VehiclesController : ControllerBase
    {

    }
}
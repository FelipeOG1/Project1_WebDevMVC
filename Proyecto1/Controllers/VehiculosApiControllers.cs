
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Proyecto1.Repositiories;
using Proyecto1.Models;
namespace Proyecto1.Controllers
{
    [ApiController]
    [Route("api/vehiculos")]
    public class VehiculosApiController : ControllerBase
    {
        [HttpPost]
        public IActionResult CrearVehiculo([FromBody] Vehiculo vehiculo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(vehiculo.Placa))
                return BadRequest(new { Message = "El campo 'placa' es requerido" });

            int response = VehiculoRepository.AgregarVehiculo(vehiculo);

            if (response == -1)
                return Conflict(new { Message = "Este vehículo ya existe" });

            return Ok(new { Message = "Vehículo agregado con éxito" });
        }

        [HttpGet]
        public IActionResult MostrarVehiculos()
        {
            var lista = VehiculoRepository.MostrarVehiculos();

            if (lista.Count > 0)
            {
                return Ok(new
                {
                    Status = 200,
                    Count = lista.Count,
                    Vehiculos = lista
                });
            }

            return Accepted(new { Message = "Todavía no se registra ningún vehículo" });
        }

        [HttpDelete]
        public IActionResult EliminarVehiculo([FromQuery] string placa)
        {
            if (string.IsNullOrWhiteSpace(placa))
                return BadRequest(new { Message = "Se esperaba la placa como query param" });

            int res = VehiculoRepository.EliminarVehiculo(placa);

            if (res != -1)
                return Ok(new { Message = "Vehículo eliminado correctamente" });

            return Conflict(new { Message = $"No existe ningún vehículo con la placa {placa}" });
        }

        [HttpPut]
        public IActionResult EditarVehiculo([FromBody] Vehiculo vehiculo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(vehiculo.Placa))
                return BadRequest(new { Message = "El campo 'placa' es requerido" });

            if (!VehiculoRepository.ExisteVehiculo(vehiculo.Placa))
                return Conflict(new { Message = "No existe un vehículo con esa placa" });

            VehiculoRepository.ReemplazarVehiculo(vehiculo);
            return Ok(new { Message = "Vehículo editado correctamente" });
        }

        [HttpGet("buscar")]
        public IActionResult BuscarVehiculo([FromQuery] string placa)
        {
            if (string.IsNullOrWhiteSpace(placa))
                return BadRequest(new { Message = "Se esperaba la placa como query param" });

            var vehiculo = VehiculoRepository.BuscarVehiculoPorPlaca(placa);

            if (vehiculo != null)
                return Ok(vehiculo);

            return NotFound(new { Message = "Vehículo no encontrado" });
        }
    }
}



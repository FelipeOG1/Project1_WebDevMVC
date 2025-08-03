
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Proyecto1.Repositiories;
using Proyecto1.Models;
using Proyecto1.Repositories;
namespace Proyecto1.Controllers
{
    [ApiController]
    [Route("api/vehiculos")]
    public class VehiculosApiController : ControllerBase
    {

        private readonly IVehiculoRepository _vehiculoRepository;

        public VehiculosApiController(IVehiculoRepository vehiculoRepository)
        {
            _vehiculoRepository = vehiculoRepository;
        }

            [HttpPost]
    public async Task<IActionResult> CrearVehiculo([FromBody] Vehiculo vehiculo)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (string.IsNullOrWhiteSpace(vehiculo.Placa))
            return BadRequest(new { Message = "El campo 'placa' es requerido" });

        int response = await _vehiculoRepository.AgregarVehiculo(vehiculo);

        if (response == (int)ErroresVehiculo.VehiculoYaExiste)
            return Conflict(new { Message = "Este vehículo ya existe" });

        return Ok(new { Message = "Vehículo agregado con éxito" });
    }

    [HttpGet]
    public async Task<IActionResult> MostrarVehiculos()
    {
        var lista = await _vehiculoRepository.MostrarVehiculos();

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
    public async Task<IActionResult> EliminarVehiculo([FromQuery] string placa)
    {
        if (string.IsNullOrWhiteSpace(placa))
            return BadRequest(new { Message = "Se esperaba la placa como query param" });

        int res = await _vehiculoRepository.EliminarVehiculo(placa);

        if (res != (int)ErroresVehiculo.VehiculoNoEncontrado)
            return Ok(new { Message = "Vehículo eliminado correctamente" });

        return Conflict(new { Message = $"No existe ningún vehículo con la placa {placa}" });
    }

    [HttpPut]
    public async Task<IActionResult> EditarVehiculo([FromBody] Vehiculo vehiculo)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (string.IsNullOrWhiteSpace(vehiculo.Placa))
            return BadRequest(new { Message = "El campo 'placa' es requerido" });

        int resultado = await _vehiculoRepository.ActualizarVehiculo(vehiculo);
        if (resultado == 0)
            return StatusCode(500, new { Message = "No se pudo actualizar el vehículo" });

        return Ok(new { Message = "Vehículo editado correctamente" });
    }

    [HttpGet("buscar")]
    public async Task<IActionResult> BuscarVehiculo([FromQuery] string placa)
    {
        if (string.IsNullOrWhiteSpace(placa))
            return BadRequest(new { Message = "Se esperaba la placa como query param" });

        var vehiculo = await _vehiculoRepository.ObtenerVehiculoPorPlaca(placa);

        if (vehiculo != null)
            return Ok(vehiculo);

        return NotFound(new { Message = "Vehículo no encontrado" });
    }
}







    }



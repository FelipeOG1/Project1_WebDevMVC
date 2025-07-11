using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using Proyecto1.Repositiories;

namespace Proyecto1.Controllers
{
    [ApiController]
    [Route("api/empleados")]
    public class EmpleadosApiController : ControllerBase
    {
        [HttpPost]
        public IActionResult CrearEmpleado([FromBody] Empleado empleado)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!empleado.Cedula.HasValue)
                return BadRequest(new { Message = "El campo cédula es requerido" });

            int response = EmpleadoRepository.AgregarEmpleado(empleado);

            if (response == -1)
                return Conflict(new { Message = "Este empleado ya existe" });

            return Ok(new { Message = "Empleado agregado con éxito" });
        }

        [HttpGet]
        public IActionResult MostrarEmpleados()
        {
            var lista = EmpleadoRepository.MostrarEmpleados();

            if (lista.Count > 0)
            {
                return Ok(new
                {
                    Status = 200,
                    Count = lista.Count,
                    Empleados = lista
                });
            }

            return Accepted(new { Message = "Todavía no se registra ningún empleado" });
        }

        [HttpDelete]
        public IActionResult EliminarEmpleado([FromQuery] int? id)
        {
            if (!id.HasValue)
                return BadRequest(new { Message = "Se esperaba un id como query param" });

            int res = EmpleadoRepository.EliminarEmpleado(id.Value);

            if (res != -1)
                return Ok(new { Message = "Empleado eliminado correctamente" });

            return Conflict(new { Message = $"No existe ningún empleado con la cédula {id}" });
        }

        [HttpPut]
        public IActionResult EditarEmpleado([FromBody] Empleado empleado)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!empleado.Cedula.HasValue)
                return BadRequest(new { Message = "El campo cédula es requerido" });

            if (!EmpleadoRepository.ExisteEmpleado(empleado.Cedula.Value))
                return Conflict(new { Message = "No existe un empleado con esa cédula" });

            EmpleadoRepository.ReemplazarEmpleado(empleado);
            return Ok(new { Message = "Empleado editado correctamente" });
        }

        [HttpGet("buscar")]
        public IActionResult BuscarEmpleado([FromQuery] int id)
        {
            var emp = EmpleadoRepository.BuscarEmpleadoPorCedula(id);

            if (emp != null)
                return Ok(emp);

            return NotFound(new { Message = "Empleado no encontrado" });
        }
    }
}




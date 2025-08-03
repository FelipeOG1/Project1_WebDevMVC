using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using Proyecto1.Repositiories;

namespace Proyecto1.Controllers
{
    [ApiController]
    [Route("api/empleados")]
    public class EmpleadosApiController : ControllerBase
    {

         private readonly IEmpleadoRepository _empleadoRepository;
        
          public EmpleadosApiController(IEmpleadoRepository empleadoRepository)
            {
                _empleadoRepository = empleadoRepository;
            }

        [HttpPost]
        public async Task<IActionResult>CrearEmpleado([FromBody] Empleado empleado)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!empleado.Cedula.HasValue)
                return BadRequest(new { Message = "El campo cédula es requerido" });

            int response = await _empleadoRepository.AgregarEmpleado(empleado);

            if (response == -1)
                return Conflict(new { Message = "Este empleado ya existe" });

            return Ok(new { Message = "Empleado agregado con éxito" });
        }

        [HttpGet]
        public async Task<IActionResult> MostrarEmpleados()
        {
             List <Empleado> lista = await _empleadoRepository.MostrarEmpleados();

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
        public async Task<IActionResult> EliminarEmpleado([FromQuery] int? id)
        {
            if (!id.HasValue)
                return BadRequest(new { Message = "Se esperaba un id como query param" });

            int res = await _empleadoRepository.EliminarEmpleado(id.Value);

            if (res != -1)
                return Ok(new { Message = "Empleado eliminado correctamente" });

            return Conflict(new { Message = $"No existe ningún empleado con la cédula {id}" });
        }

        [HttpPut]
        public async Task<IActionResult> EditarEmpleado([FromBody] Empleado empleado)
        {
            if (!ModelState.IsValid)return BadRequest(ModelState);

            Empleado tempEmpleado = await _empleadoRepository.ObtenerEmpleadoPorId(empleado.Cedula.Value);

            if (tempEmpleado == null)
            {

                return BadRequest(new { Message = "No existe un empleado con ese id" });
                
            }

            int res = await _empleadoRepository.ActualizarEmpleado(empleado);

            if (res == 0)
            {
                return Ok(new { Message = "Empleado no fue ingresado de manera correcta"});
            }
            
            return Ok(new { Message = "Empleado editado correctamente" });
        }

        [HttpGet("buscar")]
        public IActionResult BuscarEmpleado([FromQuery] int id)
        {
            var emp = _empleadoRepository.ObtenerEmpleadoPorId(id);

            if (emp != null)
                return Ok(emp);

            return NotFound(new { Message = "Empleado no encontrado" });
        }
        
    }
}




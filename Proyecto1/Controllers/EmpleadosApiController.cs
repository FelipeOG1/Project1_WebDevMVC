using System.Threading.Tasks;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using Proyecto1.Repositiories;
using Proyecto1.Repositories;

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
            if (response ==(int)ErroresEmpleado.EmpleadoYaExiste)
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
            //En caso de exito res retorna el numero de filas afectadas
            if (res > 0)
                return Ok(new { Message = "Empleado eliminado correctamente" });
            else if (res == (int)ErroresEmpleado.EmpleadoNoFueEliminado)
            {
                return Conflict(new { Message = "El empleado no pudo ser eliminado" });

            }
            else
            {

                return Conflict(new { Message = $"No existe ningún empleado con la cédula {id}" });
            }

        }

        [HttpPut]
        public async Task<IActionResult> EditarEmpleado([FromBody] Empleado nuevoEmpleado)
        {
    
             if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            int res = await _empleadoRepository.ActualizarEmpleado(nuevoEmpleado);

            if (res != (int)ErroresEmpleado.EmpleadoNoEncontrado)
            {

                return Ok(new { Message = "Empleado editado de manera correcta" });
            }
            else
            {
                return Conflict(new { Message = "No existe un Empleado con ese id" });
            }
       }

        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarEmpleado([FromQuery] int id)
        {
             Empleado emp = await _empleadoRepository.ObtenerEmpleadoPorId(id);

            if (emp != null)
                return Ok(emp);

            return NotFound(new { Message = "Empleado no encontrado" });
        }
        
    }
}




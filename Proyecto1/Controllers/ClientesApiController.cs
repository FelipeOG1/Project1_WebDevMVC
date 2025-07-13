using System.ComponentModel; using System.Diagnostics; using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Proyecto1.Models;
using Proyecto1.Repositiories;

namespace Proyecto1.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClientesApiController : ControllerBase
    {

        [HttpPost]
        public IActionResult CrearCliente([FromBody] Cliente cliente)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            if (!cliente.Identificacion.HasValue)
            {

                return BadRequest(new { Message = "El campo identificación es requerido" });
            }


            int response = ClienteRepository.AgregarCliente(cliente);


            if (response == -1)
            {
                return Conflict(new
                {
                    Message = "Este cliente ya existe",
                });
            }


            return Ok(new
            {
                Message = "Cliente Agregado con exito",
            });


        }

        [HttpGet]
        public IActionResult MostrarClientes()
        {
            List<Cliente> listaClientes = ClienteRepository.MostrarClientes();

            if (listaClientes.Count() > 0)
            {

                var response = new
                {
                    Status = 200,
                    Count = listaClientes.Count(),

                    Clientes = listaClientes,




                };

                return Ok(response);
            }
            else
            {


                return Accepted(new { Message = "Todavía no se registra ningún cliente" });

            }


        }
        [HttpDelete]
        public IActionResult EliminarCliente([FromQuery] int? id)
        {

            if (id.HasValue)
            {
                int res = ClienteRepository.EliminarCliente(id.Value);

                if (res != -1)
                {


                    return Ok(new { Message = "Cliente Eliminado correctamente" });


                }
                else
                {

                    return Conflict(new { Message = $"No existe ningun cliente con el id {id}" });
                }

            }
            else
            {

                return BadRequest(new { Message = "Se esperaba un id como query param" });
            }



        }

        [HttpPut]
        public IActionResult EditarCliente([FromBody] Cliente cliente)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            int res = ClienteRepository.ReemplazarCliente(cliente);

            if (res != -1)
            {

                return Ok(new { Message = "Usuario editado de manera correcta" });
            }
            else
            {

                return Conflict(new { Message = "No existe un usuario con ese id" });


            }



        }
        
        

        [HttpGet("buscar")]
        public IActionResult BuscarEmpleado([FromQuery] int id)
        {
            var cliente = ClienteRepository.BuscarCliente(id);

            if (cliente != null)
                return Ok(cliente);

            return NotFound(new { Message = "Cliente no encontrado" });
        }
 
        


    }

    

}
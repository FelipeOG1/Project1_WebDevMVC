using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using Proyecto1.Repositiories;

namespace Proyecto1.Controllers
{
    [ApiController]
    [Route("api/lavados")]
    public class LavadosApiController : ControllerBase
    {
        [HttpPost]
        public IActionResult CrearLavado([FromBody] Lavado lavado)
        {

            if (ModelState.IsValid)
            {
                string nombreLavado = lavado.nombreTipo;

                TipoLavado tipo = LavadoRepository.inicializarTipo(nombreLavado);

                string resultado = tipo.nombre;

                lavado.Tipo = tipo;
                 
                int res = LavadoRepository.AgregarLavado(lavado,tipo);

                switch (res)
                {
                    
                     case >0 :
                     return Ok(new { message = "El lavado fue agregado con exito", count = res });

                    case 0:

                         return BadRequest(new { message = "No se pudo agregar el lavado" });

                    case -1:

                    return BadRequest(new { message = "En la Joya se necesita el precio" }); 

                }

                   
            }


                return BadRequest(new { me = ModelState });
            


        }

        [HttpGet]
        public IActionResult MostrarLavados()
        {
            List<Lavado>lavados = LavadoRepository.MostrarLavados();

            if (lavados.Count > 0)
            {
                return Ok(new
                {
                    Count = lavados.Count,
                    Lavados = lavados,

                    
                });
            }

            return Accepted(new { Message = "Todavía no se registra ningún lavado." });
        }

        [HttpDelete]
        public IActionResult EliminarLavado([FromQuery] int? id)
        {
            if (!id.HasValue)
                return BadRequest(new { Message = "Se esperaba un ID como query param." });

            int res = LavadoRepository.EliminarLavado(id.Value);

            if (res == -1)
                return Conflict(new { Message = $"No existe ningún lavado con el ID {id}" });

            return Ok(new { Message = "Lavado eliminado correctamente." });
        }

        [HttpPut]
        public IActionResult EditarLavado([FromBody] Lavado lavado)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int res = LavadoRepository.ReemplazarLavado(lavado);

            if (res == -1)
                return Conflict(new { Message = "No existe un lavado con ese ID." });

            return Ok(new { Message = "Lavado editado correctamente." });
        }

        [HttpGet("buscar")]
        public IActionResult BuscarLavadoPorId([FromQuery] int id)
        {
            var lavado = LavadoRepository.BucarLavadoPorId(id);

            if (lavado == null)
                return NotFound(new { Message = "No se encontró ningún lavado con ese ID." });

            return Ok(lavado);
        }
    }
}
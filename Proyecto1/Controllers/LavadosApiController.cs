using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using Proyecto1.Repositiories;
using Proyecto1.Repositories;

namespace Proyecto1.Controllers
{
    [ApiController]
    [Route("api/lavados")]
    public class LavadosApiController : ControllerBase
    {

         private readonly ILavadoRepository _lavadoRepository;
        
          public LavadosApiController(ILavadoRepository lavadoRepository )
            {
                _lavadoRepository = lavadoRepository;
            }

        [HttpPost]
        public async Task<IActionResult>CrearLavado([FromBody] Lavado lavado)
        {

            if (ModelState.IsValid)
            {
                string nombreLavado = lavado.nombreTipo;

                TipoLavado tipo = LavadoRepository.inicializarTipo(nombreLavado);

                lavado.Tipo = tipo;
                 
                int res = await  _lavadoRepository.AgregarLavado(lavado,tipo);

                Console.WriteLine(res);

                switch (res)
                {

                    case > 0:
                        return Ok(new { message = "El lavado fue agregado con exito"});

                    case (int)ErroresLavado.LavadoNoFueAgregado:

                        return BadRequest(new { message = "No se pudo agregar el lavado" });

                    case (int)ErroresLavado.LavadoNecesitaPrecio:

                        return BadRequest(new { message = "En la Joya se necesita el precio" });

                    case (int)ErroresLavado.ClienteOVehiculoNoExiste:
                        return BadRequest(new { message = "La placa o el id cliente son incorrectos" });
                }  

                   
            }


                return BadRequest(new { me = ModelState });
   

        }

        [HttpGet]
        public async Task<IActionResult> MostrarLavados()
        {
            List<Lavado>lavados = await _lavadoRepository.MostrarLavados();
            foreach (var lavado in lavados)
            {
                lavado.Tipo = LavadoRepository.inicializarTipo(lavado.nombreTipo);
                
            }

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
        public async Task<IActionResult> EliminarLavado([FromQuery] int? id)
        {
            if (!id.HasValue)
                return BadRequest(new { Message = "Se esperaba un ID como query param." });

            int res = await _lavadoRepository.EliminarLavado(id.Value);

            if (res == (int)ErroresLavado.LavadoNoEncontrado)
                return Conflict(new { Message = $"No existe ningún lavado con el ID {id}" });

            if (res > 0) return Ok(new { Message = "Lavado eliminado correctamente." });
            return Conflict(new { Message = "Lavado no pudo ser eliminado" });
        }

        [HttpPut]
        public async Task<IActionResult> EditarLavado([FromBody] Lavado lavado)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

                string nombreLavado = lavado.nombreTipo;

                TipoLavado tipo = LavadoRepository.inicializarTipo(nombreLavado);

                lavado.Tipo = tipo;
                 
                int res = await _lavadoRepository.ActualizarLavado(lavado,tipo);

            if (res == (int)ErroresLavado.LavadoNoEncontrado)
                return Conflict(new { Message = "No existe un lavado con ese ID." });

            return Ok(new { Message = "Lavado editado correctamente." });
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarLavadoPorId([FromQuery] int id)
        {
             Lavado lavado = await _lavadoRepository.ObtenerLavadoPorId(id);
            TipoLavado tipo =LavadoRepository.inicializarTipo(lavado.nombreTipo);

            lavado.Tipo = tipo;

            if (lavado == null)
                return NotFound(new { Message = "No se encontró ningún lavado con ese ID." });

            return Ok(lavado);
        }

       
    }
}
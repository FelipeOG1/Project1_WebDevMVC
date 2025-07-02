using System.ComponentModel;
using System.Diagnostics;
using System.Reflection.Metadata;
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
        public IActionResult AddClientes([FromBody] Cliente cliente)
        {


            if (!ModelState.IsValid)
            {
                var tipo = cliente.GetType();
                var clientePorDefecto = Activator.CreateInstance(tipo);
                List<string> propiedadesEsperadas = new List<string>();
                List<string> propiedadesObtenidas = new List<string>();
                foreach (var propiedad in tipo.GetProperties())
                {

                    var valorObtenido = propiedad.GetValue(cliente);
                    var valorEsperado = propiedad.GetValue(clientePorDefecto);

                    propiedadesEsperadas.Add($"{propiedad.Name}: {valorEsperado}");
                    propiedadesObtenidas.Add($"{propiedad.Name}: {valorObtenido}");
                }

                return BadRequest(new
                {

                    Mensaje = "Objeto no valido",
                    ValoresEsperados = propiedadesEsperadas,
                    ValoresObtenidos = propiedadesObtenidas,
                });
            }

            int response = ClienteRepository.AgregarCliente(cliente);

            if (response < 0)
            {
                return BadRequest(new
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

            return Ok("mama");
        }

        

       
  

    }

    

}
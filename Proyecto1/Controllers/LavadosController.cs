using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;


namespace Proyecto1.Controllers
{
    public class LavadosController: Controller
    {


        [HttpGet]
        public IActionResult Index()
        {

            List<Lavado>lavado=Lavado.MostrarLavados();

            



            

            return View(lavado);


        }

        [HttpGet]
        public IActionResult CrearLavado()
        {


            return View();



        }

        [HttpPost]
       
        
        public IActionResult CrearLavado(string TipoNombre, string Placa, int IdCliente, EstadoLavado Estado, int ?Precio=null){
            TipoLavado tipo = Lavado.inicializarTipo(TipoNombre);

            Lavado nuevoLavado= new Lavado(Placa,IdCliente,Estado,tipo,Precio);

            Lavado.AgregarLavado(nuevoLavado);


            return RedirectToAction("Index");
        }





    }
}

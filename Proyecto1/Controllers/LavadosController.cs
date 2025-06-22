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


        [HttpPost]
        public IActionResult BuscarLavado(int idLavado )
        {

            if (Lavado.ExisteLavado(idLavado))
            {

                return RedirectToAction("EditarLavado", new { id = idLavado });

            }

            TempData["Mensaje"] = "No existe ningun lavado con ese id";

            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult EditarLavado(int id)
        {

           Lavado lavado = Lavado.BucarLavadoPorId(id);

           return View(lavado); 


        }

        [HttpPost]
        public IActionResult ReemplazarLavado(int id,string TipoNombre, string Placa, int IdCliente, EstadoLavado Estado, int? Precio = null)
        {
            TipoLavado tipo = Lavado.inicializarTipo(TipoNombre);


            Lavado nuevoLavado = new Lavado(Placa, IdCliente, Estado, tipo, Precio);

            nuevoLavado.Id = id;
       
            Lavado.ReemplazarLavado(nuevoLavado);


            return RedirectToAction("Index");
        }

        public IActionResult EliminarLavado(int id)
        {

            int res = Lavado.EliminarLavado(id);

            return RedirectToAction("Index");


       
        }




    }
}

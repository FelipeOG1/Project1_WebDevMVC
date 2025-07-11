using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;

namespace Proyecto1.Controllers
{
    public class VehiculosController: Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
   




            return View();
        }



        [HttpGet]

        public IActionResult CrearVehiculo()
        {

            return View();
        }




        [HttpPost]
        public IActionResult CrearVehiculo(Vehiculo ve )
        {

          
            if (!ModelState.IsValid)
            {
                return View(ve); 
            }



            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult EditarVehiculo(string placa)
        {



            return View();


        }

 
        public IActionResult ReemplazarVehiculo(Vehiculo vehiculoActualizado)
        {
            if (!ModelState.IsValid)
            {
                return View("EditarEmpleado", vehiculoActualizado); // En caso de error de validación
             }

          

            return RedirectToAction("Index");

        }



        [HttpPost]
        public IActionResult BuscarVehiculo(string placa)
        {

        
            TempData["Mensaje"] = "No existe ningun vehiculo con esa placa";

            return RedirectToAction("Index");
        }



        public IActionResult EliminarVehiculo(string placa)
        {




            return RedirectToAction("Index");
        }








    }
}

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
   
           List<Vehiculo>lista=Vehiculo.MostrarVehiculos();

            




            return View(lista);
        }



        [HttpGet]

        public IActionResult CrearVehiculo()
        {

            return View();
        }




        [HttpPost]
        public IActionResult CrearVehiculo(Vehiculo ve )
        {

            if (Vehiculo.ExisteVehiculo(ve.Placa)) {

                ModelState.AddModelError("Placa", "Ya existe un Vehiculo con esa placa");

            }
            if (!ModelState.IsValid)
            {
                return View(ve); 
            }

            Vehiculo.AgregarVehiculo(ve);


            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult EditarVehiculo(string placa)
        {


            Vehiculo ve = Vehiculo.BuscarVehiculoPorPlaca(placa);

            return View(ve);


        }

 
        public IActionResult ReemplazarVehiculo(Vehiculo vehiculoActualizado)
        {
            if (!ModelState.IsValid)
            {
                return View("EditarEmpleado", vehiculoActualizado); // En caso de error de validación
             }

            Vehiculo original = Vehiculo.BuscarVehiculoPorPlaca(vehiculoActualizado.Placa); 



            if (original == null)
            {
                return NotFound();
            }

            Vehiculo.ReemplazarVehiculo(vehiculoActualizado); // Este método debes tenerlo tú

            return RedirectToAction("Index");

        }



        [HttpPost]
        public IActionResult BuscarVehiculo(string placa)
        {

            if (Vehiculo.ExisteVehiculo(placa))
            {

                return RedirectToAction("EditarVehiculo", new { placa = placa });

            }

            TempData["Mensaje"] = "No existe ningun vehiculo con esa placa";

            return RedirectToAction("Index");
        }



        public IActionResult EliminarVehiculo(string placa)
        {



            int res = Vehiculo.EliminarVehiculo(placa);

            return RedirectToAction("Index");



        }








    }
}

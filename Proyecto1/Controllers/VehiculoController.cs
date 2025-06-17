using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;

namespace Proyecto1.Controllers
{
    public class VehiculoController: Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
   
           List<Vehiculo>lista=Vehiculo.MostrarVehiculos();

            




            return View(lista);
        }

    
        [HttpPost]
        public IActionResult CrearEmpleado(Empleado emp)
        {
            int response=Empleado.AgregarEmpleado(emp);
                          

          
            return RedirectToAction("Index");  
        }



        

        



    }
}

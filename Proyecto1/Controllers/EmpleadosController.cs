using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;

namespace Proyecto1.Controllers
{
    public class EmpleadosController: Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
   
           List<Empleado>lista=Empleado.MostrarEmpleados();



            return View(lista);
        }

        [HttpGet]
        public IActionResult CrearEmpleado()
        {


            return View();

            
        }

    
        [HttpPost]
        public IActionResult CrearEmpleado(Empleado emp)
        {
            int response=Empleado.AgregarEmpleado(emp);
                          

          
            return RedirectToAction("Index");  
        }



        

        



    }
}

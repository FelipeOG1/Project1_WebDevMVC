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

           [HttpGet]
        public IActionResult EditarEmpleado(int cedula)
        {



            Empleado em = Empleado.BuscarEmpleadoPorCedula(cedula);


            return View(em);

        }


       

        public IActionResult ReemplazarEmpleado(Empleado empleadoActualizado)
            {
            if (!ModelState.IsValid)
            {
                return View("EditarEmpleado", empleadoActualizado);             }

            Empleado original = Empleado.BuscarEmpleadoPorCedula(empleadoActualizado.Cedula);

            if (original == null)
            {
                return NotFound(); 
            }

            Empleado.ReemplazarEmpleado(empleadoActualizado);
            return RedirectToAction("Index");





        }

        [HttpPost]
        public IActionResult BuscarEmpleado(int cedula)
        {

            if (Empleado.ExisteEmpleado(cedula))
            {

                return RedirectToAction("EditarEmpleado", new { cedula = cedula });

            }

            TempData["Mensaje"] = "No existe ningun empleado con esa cedula";

            return RedirectToAction("Index");   
        }


        [HttpPost]
        public IActionResult CrearEmpleado(Empleado emp)
        {

            if (Empleado.ExisteEmpleado(emp.Cedula))
            {

                ModelState.AddModelError("Cedula", "Ya existe un Empleado con esa cedula");
                
            }
            if (!ModelState.IsValid)
            {
                return View(emp); // Retorna a la misma vista con los errores
            }

            Empleado.AgregarEmpleado(emp);





            return RedirectToAction("Index");  
        }

        




        public IActionResult EliminarEmpleado(int cedula)
        {



           int res = Empleado.EliminarEmpleado(cedula);

           return RedirectToAction("Index");






        }

        



    }
}

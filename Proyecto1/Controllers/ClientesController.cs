using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;



namespace Proyecto1.Controllers
{
    public class ClientesController:Controller
    {



         [HttpGet]
        public IActionResult Index()
        {
   
           List<Cliente>lista=Cliente.MostrarClientes();



            return View(lista);
        }

        [HttpGet]
        public IActionResult CrearCliente()
        {


            return View();

            
        }

        [HttpGet]
        public IActionResult EditarCliente(int identificacion)
        {



             Cliente cl = Cliente.BuscarCliente(identificacion);






            return View(cl);


        }




        public IActionResult ReemplazarCliente(Cliente ClienteActualizado)
        {
            if (!ModelState.IsValid)
            {
                return View("EditarCliente", ClienteActualizado);
            }

            Cliente original = Cliente.BuscarCliente(ClienteActualizado.Identificacion);

            if (original == null)
            {
                return NotFound();
            }

            Cliente.ReemplazarCliente(ClienteActualizado);

            return RedirectToAction("Index");


        }



      

        [HttpPost]
        public IActionResult BuscarCliente(int identificacion)
        {

            if (Cliente.ExisteCliente(identificacion))
            {

                return RedirectToAction("EditarCLiente", new {identificacion = identificacion});
            }

            TempData["Mensaje"] = "No existe un usuario con esa cedula";
            return RedirectToAction("Index");

            
        }


       [HttpPost]
        public IActionResult CrearCliente(Cliente cl)
        {

            if (Cliente.ExisteCliente(cl.Identificacion))
            {

                ModelState.AddModelError("Identificacion", "Ya existe un Cliente con esa Identificación");

                
                
            }
            if (!ModelState.IsValid)
            {
                return View(cl); // Retorna a la misma vista con los errores
            }

            Cliente.AgregarCliente(cl);





            return RedirectToAction("Index");  
        }

        
 
        public IActionResult EliminarCliente(int identificacion)
        {


           
           int res = Cliente.EliminarCliente(identificacion);


           return RedirectToAction("Index");






        }

        


    }
}

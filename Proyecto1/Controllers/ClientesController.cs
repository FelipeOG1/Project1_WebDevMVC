using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using System.Text.Json.Nodes;
using System.Net;
using System.Text.Json;

namespace Proyecto1.Controllers
{
     
    public class ClientesController:Controller
    {

        private readonly HttpClient _httpClient;

        public ClientesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


            [HttpGet]
            public async Task<IActionResult> MostrarClientes()
            {
                string url = "http://localhost:5055/api/clientes";

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                try
                {
                    if (response.IsSuccessStatusCode)
                    {                   
                        var listaClientes = JsonSerializer.Deserialize<List<Cliente>>(json, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        return View(listaClientes);
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        JsonNode node = JsonNode.Parse(json);
                        string mensaje = node["message"]?.ToString();
                        ViewData["Message"] = mensaje ?? "Ocurrió un error desconocido.";
                        return View(new List<Cliente>()); 
                    }
                    else
                    {
                        ViewData["Message"] = "Error inesperado del servidor.";
                        return View(new List<Cliente>());
                    }
                }
                catch (Exception ex)
                {
                    ViewData["Message"] = "Error procesando la respuesta: " + ex.Message;
                    return View(new List<Cliente>());
                }
            }


       

        [HttpGet]
        public IActionResult CrearCliente()
        {


            return View();

            
        }

        [HttpGet]
        public IActionResult EditarCliente(int identificacion)
        {



            return View();


        }

        public IActionResult ReemplazarCliente(Cliente ClienteActualizado)
        {

            return RedirectToAction("Index");


        }



      

        [HttpPost]
        public IActionResult BuscarCliente(int identificacion)
        {

            TempData["Mensaje"] = "No existe un usuario con esa cedula";
            return RedirectToAction("Index");

            
        }


       [HttpPost]
        public IActionResult CrearCliente(Cliente cl)
        {



                


            return RedirectToAction("Index");  
        }

        
        
        public IActionResult EliminarCliente(int identificacion)
        {


           

           return RedirectToAction("Index");




        }

        
    }
}

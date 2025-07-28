using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using System.Text.Json.Nodes;
using System.Net;
using System.Text.Json;
using System.Runtime.InteropServices;
using System.Threading.Tasks.Dataflow;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Proyecto1.Controllers
{
     
    public class ClientesController:Controller
    {

        private readonly HttpClient _httpClient;
    
        private readonly string URL = "http://localhost:5055/api/clientes";

          private JsonSerializerOptions? options = new JsonSerializerOptions
          {
              PropertyNameCaseInsensitive = true
            
            };



        public ClientesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            options.Converters.Add(new JsonStringEnumConverter());

        }


        [HttpGet]
        public async Task<IActionResult> MostrarClientes()
        {

            HttpResponseMessage response = await _httpClient.GetAsync(URL);

            List<Cliente>? lista_clientes = new List <Cliente>();
            int response_status_code= (int)response.StatusCode;
            switch (response_status_code)
            {
                case 200:
                    string json_content = await response.Content.ReadAsStringAsync();
                    JsonNode? root = JsonNode.Parse(json_content);
                    if (root != null && root["clientes"] is JsonArray clientesNode)
                    {

                         lista_clientes = JsonSerializer.Deserialize<List<Cliente>>(clientesNode.ToJsonString(), options);

                        Console.WriteLine($"Cantidad de empleados en la lista {lista_clientes.Count()}");

                        Console.WriteLine(lista_clientes[0].Identificacion);
                    }

                    break;

                case 202:
                    ViewData["Message"] = "Todavia no se agregan clientes";

                    break;
            }

            return View(lista_clientes);
        }      

        [HttpGet]
        public IActionResult CrearCliente()
        {

            


            return View();

            
        }

        [HttpGet]
       async public Task<IActionResult> EditarCliente(int identificacion)
        {

            
            HttpResponseMessage response = await _httpClient.GetAsync($"{URL}/buscar/?id={identificacion}");

            
            int res = (int)response.StatusCode;

            Cliente? cliente = new Cliente();

            switch (res)
            {

                case 200:
                    string json_string = await response.Content.ReadAsStringAsync();

                    cliente = JsonSerializer.Deserialize<Cliente>(json_string, options);

                    Console.WriteLine("mama");


                    break;

                case 404:
                    Console.WriteLine("aaaa");

                    break;

            }
            return View(cliente);




        }

        public IActionResult ReemplazarCliente(Cliente ClienteActualizado)
        {

            return RedirectToAction("Index");


        }



      

        [HttpPost]
        async public Task<IActionResult> BuscarCliente(int identificacion)
        {


            Console.WriteLine(identificacion);
            HttpResponseMessage response = await _httpClient.GetAsync($"{URL}/buscar/?id={identificacion}");

            int res = (int)response.StatusCode;

            Cliente? cliente = new Cliente();

            switch (res)
            {

                case 200:
                    string json_string = await response.Content.ReadAsStringAsync();

                     cliente = JsonSerializer.Deserialize<Cliente>(json_string, options);


                    return RedirectToAction("EditarCliente", new { identificacion =cliente.Identificacion});

                default:
                    TempData["Mensaje"] = "No existe ningun vehiculo con esa placa";

                    return RedirectToAction("MostrarCLientes");


            }

        }


       [HttpPost]
        public IActionResult CrearCliente(Cliente cl)
        {



            return RedirectToAction("Index");  
        }

        
        
        async public Task<IActionResult> EliminarCliente(int identificacion)
        {


            HttpResponseMessage response = await _httpClient.DeleteAsync($"{URL}/?id={identificacion}");

            int res = (int)response.StatusCode;

            switch (res)
            {

                case 200:


                    break;

                case 404:
                    Console.WriteLine("aaaa");

                    break;

            }
            return RedirectToAction("MostrarClientes");


  





        }

        
    }
}

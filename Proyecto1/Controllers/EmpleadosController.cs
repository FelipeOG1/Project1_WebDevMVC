using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using System.Text.Json.Nodes;
using System.Net;
using System.Text.Json;
using System.Reflection.Metadata.Ecma335;


namespace Proyecto1.Controllers
{
    public class EmpleadosController : Controller
    {


        private readonly string URL = "http://localhost:5055/api/empleados";
        
       private readonly HttpClient _httpClient;

      private JsonSerializerOptions? options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

       public EmpleadosController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public async Task<IActionResult> MostrarEmpleados()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(URL);

            int res = (int)response.StatusCode;

            List<Empleado>? lista_empleados = new List<Empleado>();
            switch (res)
            {
                case 202:

                    ViewData["Message"] = "Todavia no se registran clients";

                    break;
                case 200:
                    string json_string = await response.Content.ReadAsStringAsync();
                    JsonNode? root = JsonNode.Parse(json_string);


                    if (root != null && root["empleados"] is JsonArray empleadosNode)
                    {

                        lista_empleados = JsonSerializer.Deserialize<List<Empleado>>(empleadosNode.ToJsonString(), options);

                        Console.WriteLine($"Cantidad de empleados en la lista {lista_empleados.Count()}");

                        Console.WriteLine(lista_empleados[0].Cedula);


                    }
                    break;

            }

            return View(lista_empleados);

        }
        [HttpGet]
        public IActionResult CrearEmpleado()
        {
           
            return View();

        }

        [HttpGet]
        async public Task<IActionResult> EditarEmpleado(int cedula)
        {

            string currentUrl = $"{URL}/buscar/?id={cedula}";
            HttpResponseMessage response = await _httpClient.GetAsync($"{URL}/buscar/?id={cedula}");

            
            string mama = await response.Content.ReadAsStringAsync();
            
            int res = (int)response.StatusCode;

            Empleado? empleado = new Empleado();

            switch (res)
            {

                case 200:
                    string json_string = await response.Content.ReadAsStringAsync();

                    empleado = JsonSerializer.Deserialize<Empleado>(json_string, options);

                    Console.WriteLine("mama");


                    break;

                case 404:
                    Console.WriteLine("aaaa");

                    break;

            }
            return View(empleado);

        }

        [HttpPost]
        async public Task<IActionResult> BuscarEmpleado(int cedula)
        {
            string currentUrl = $"{URL}/buscar/?id={cedula}";
            HttpResponseMessage response = await _httpClient.GetAsync($"{URL}/buscar/?id={cedula}");



            int res = (int)response.StatusCode;

            Empleado? empleado = new Empleado();

            switch (res)
            {

                case 200:
                    string json_string = await response.Content.ReadAsStringAsync();

                    empleado = JsonSerializer.Deserialize<Empleado>(json_string, options);


                    return RedirectToAction("EditarEmpleado", new { cedula = empleado.Cedula });

                default :
                    TempData["Mensaje"] = "No existe ningun empleado con esa cedula";

                    return RedirectToAction("MostrarEmpleados");


            }           

     }
     
        async public Task<IActionResult> EliminarEmpleado(int cedula)
        {

            HttpResponseMessage response = await _httpClient.DeleteAsync($"{URL}/?id={cedula}");

            int res = (int)response.StatusCode;


            switch (res)
            {

                case 200:


                    break;

                case 404:
                    Console.WriteLine("aaaa");

                    break;

            }
            return RedirectToAction("MostrarEmpleados");


        }


    }
}

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using System.Text.Json.Nodes;
using System.Net;
using System.Text.Json;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Antiforgery;




namespace Proyecto1.Controllers
{
    public class VehiculosController : Controller
    {

        private readonly string URL = "http://localhost:5055/api/vehiculos";

        private readonly HttpClient _httpClient;

        private JsonSerializerOptions? options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public VehiculosController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
       async public Task<IActionResult>  MostrarVehiculos()
        {

            HttpResponseMessage response = await _httpClient.GetAsync(URL);

            int res = (int)response.StatusCode;

            List<Vehiculo>? lista_vehiculos = new List<Vehiculo>();
            switch (res)
            {
                case 202:

                    ViewData["Message"] = "Todavia no se registran clients";

                    break;
                case 200:
                    string json_string = await response.Content.ReadAsStringAsync();
                    JsonNode? root = JsonNode.Parse(json_string);


                    if (root != null && root["vehiculos"] is JsonArray VehiculosNode)
                    {

                        lista_vehiculos = JsonSerializer.Deserialize<List<Vehiculo>>(VehiculosNode.ToJsonString(), options);

                    }
                    break;

            }

            return View(lista_vehiculos);



        }


        [HttpGet]

         public IActionResult CrearVehiculo()
        {

            return View();
        }


        [HttpGet]
        async public Task<IActionResult> EditarVehiculo(string placa)
        {

            HttpResponseMessage response = await _httpClient.GetAsync($"{URL}/buscar/?placa={placa}");



            int res = (int)response.StatusCode;

            Vehiculo? vehiculo = new Vehiculo ();

            switch (res)
            {

                case 200:
                    string json_string = await response.Content.ReadAsStringAsync();

                    vehiculo = JsonSerializer.Deserialize<Vehiculo>(json_string, options);



                    break;

                case 404:

                    break;

            }
            return View(vehiculo);

        }

        [HttpPost]
        async public Task<IActionResult> BuscarVehiculo(string placa)
        {

            Console.WriteLine(placa);


            HttpResponseMessage response = await _httpClient.GetAsync($"{URL}/buscar/?placa={placa}");

            int res = (int)response.StatusCode;

            Vehiculo? vehiculo = new Vehiculo();

            switch (res)
            {

                case 200:
                    string json_string = await response.Content.ReadAsStringAsync();

                    vehiculo = JsonSerializer.Deserialize<Vehiculo>(json_string, options);


                    return RedirectToAction("EditarVehiculo", new { placa =vehiculo.Placa });

                default:
                    TempData["Mensaje"] = "No existe ningun vehiculo con esa placa";

                    return RedirectToAction("MostrarVehiculos");

            }

        }

        async public Task<IActionResult> EliminarVehiculo(string placa)
        {

            HttpResponseMessage response = await _httpClient.DeleteAsync($"{URL}/?placa={placa}");
            int res = (int)response.StatusCode;

            switch (res)
            {

                case 200:


                    break;

                case 404:

                    break;

            }
            return RedirectToAction("MostrarVehiculos");





        }








    }
}

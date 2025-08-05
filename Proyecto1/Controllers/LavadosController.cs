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
    public class LavadosController: Controller
    {

        private readonly HttpClient _httpClient;

         private readonly string URL = "http://localhost:5055/api/lavados";

          private JsonSerializerOptions? options = new JsonSerializerOptions
          {
              PropertyNameCaseInsensitive = true
            
            };


        public LavadosController(HttpClient httpClient)
        {

            _httpClient = httpClient;
            options.Converters.Add(new JsonStringEnumConverter());

        }

        [HttpGet]
        async public Task<IActionResult> MostrarLavados()
        {


            HttpResponseMessage response = await _httpClient.GetAsync(URL);

            List<Lavado>? lista_lavados = new List <Lavado>();
            int response_status_code= (int)response.StatusCode;
            switch (response_status_code)
            {
                case 200:
                    string json_content = await response.Content.ReadAsStringAsync();
                    JsonNode? root = JsonNode.Parse(json_content);
                    if (root != null && root["lavados"] is JsonArray lavadosNode)
                    {

                         lista_lavados = JsonSerializer.Deserialize<List<Lavado>>(lavadosNode.ToJsonString(), options);


                    }

                    break;

                case 202:
                    ViewData["Message"] = "Todavia no se agregan clientes";

                    break;
            }

            return View(lista_lavados);

       }

        [HttpGet]
        public IActionResult CrearLavado()
        {

            return View();

        }


        [HttpPost]
        async public Task<IActionResult> BuscarLavado(int id)
        {

            Console.WriteLine(id);
            HttpResponseMessage response = await _httpClient.GetAsync($"{URL}/buscar/?id={id}");

            int res = (int)response.StatusCode;

            Lavado?  lavado = new Lavado();

            switch (res)
            {

                case 200:
                    string json_string = await response.Content.ReadAsStringAsync();

                    lavado = JsonSerializer.Deserialize<Lavado>(json_string, options);


                    return RedirectToAction("EditarLavado", new { id = lavado.Id});

                default :
                    TempData["Mensaje"] = "No existe ningun lavado con ese Id";

                    return RedirectToAction("MostrarLavados");


            }           

            
            
            

        }

        [HttpGet]
       async public Task<IActionResult> EditarLavado(int id)
        {

            HttpResponseMessage response = await _httpClient.GetAsync($"{URL}/buscar/?id={id}");

            
            int res = (int)response.StatusCode;

            Lavado? lavado  = new Lavado();

            switch (res)
            {

                case 200:
                    string json_string = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(json_string);

                    lavado = JsonSerializer.Deserialize<Lavado>(json_string, options);



                    break;

                case 404:

                    break;

            }
            return View(lavado);


        }



       async public Task<IActionResult> EliminarLavado(int id)
        {
            
             HttpResponseMessage response = await _httpClient.DeleteAsync($"{URL}/?id={id}");
            int res = (int)response.StatusCode;

            switch (res)
            {

                case 200:


                    break;

                case 404:
                    Console.WriteLine("aaaa");

                    break;

            }
            return RedirectToAction("MostrarLavados");

           

       
        }




    }
}

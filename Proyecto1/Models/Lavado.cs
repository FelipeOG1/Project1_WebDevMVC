using AspNetCoreGeneratedDocument;
using Microsoft.Net.Http.Headers;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Proyecto1.Models
{
    public enum EstadoLavado
    {
        EnProceso,
        Facturado,
        Agendado
    }

    public class  TipoLavado
    {
    public string nombre { get; set; }
    public List<string> prestaciones { get; set; }
    public decimal? precio { get; set; }

    }
    public class Lavado
    { 

    private static int idCounter = 1;

    public int Id { get; set; }

    private const decimal Iva = 0.13m;
    [Required(ErrorMessage = "La placa es obligatoria")]
    public string Placa { get; set; }

    [Required(ErrorMessage = "El id del cliente es obligatorio")]
    public int IdCLiente { get; set; }

    [Required(ErrorMessage = "El estado del lavado es obligatorio")]
    public EstadoLavado Estado { get; set; }

    [Required(ErrorMessage = "El nombre del tipo es obligatorio")]
    public string nombreTipo { get; set; }
    [BindNever]
    public TipoLavado? Tipo{ get; set; }
    public decimal? precio { get; set; }

    public decimal? precio_con_iva{ get; set; }

      
        private static Dictionary<int, Lavado> _lavados = new Dictionary<int, Lavado>();
        private static HashSet<int> _ids = new HashSet<int>();

      
        

        public Lavado()
        {
        
        
       } 
        public static TipoLavado inicializarTipo(string name)
        {
            TipoLavado tipo = new TipoLavado();

            switch (name.ToLower())
            {
                case "básico":
                    tipo.nombre = "Básico";
                    tipo.prestaciones = new List<string> { "Lavado", "Aspirado", "Encerado" };
                    tipo.precio = 5000m;
                    break;

                case "premium":
                    tipo.nombre = "Premium";
                    tipo.prestaciones = new List<string> { "Lavado", "Aspirado", "Encerado", "Limpieza profunda de asientos" };
                    tipo.precio = 8000m;
                    break;

                case "deluxe":
                    tipo.nombre = "Deluxe";
                    tipo.prestaciones = new List<string> { "Lavado", "Aspirado", "Encerado", "Limpieza profunda de asientos", "Corrección de pintura", "Tratamiento nanocerámico" };
                    tipo.precio = 12000m;
                    break;

                case "la joya":
                    tipo.nombre = "La Joya";
                    tipo.prestaciones = new List<string> { "Todo lo anterior", "Pulidos", "Tratamientos hidrofóbicos", "Extras a convenir" };
                    tipo.precio = 0m;
                    break;

                default:
                    tipo.nombre = "Desconocido";
                    tipo.prestaciones = new List<string>();
                    tipo.precio = 0m;
                    break;
            }

            return tipo;
        }


        public static int AgregarLavado(Lavado lav,TipoLavado tipo)
        {
                            
            if (_ids.Contains(lav.Id))
            {

                return -1;
            }

             int currentId = idCounter++;
            _lavados.Add(currentId, lav);
            _ids.Add(currentId);

            return _lavados.Count;
           
        }
             
        public static bool ExisteLavado(int id)
        {

            if (_ids.Contains(id)) return true;

            return false;   
        
       
        }

        public static List<Lavado> MostrarLavados()
        {

           return _lavados.Values.ToList();
            
        }

        public static Lavado BucarLavadoPorId(int id)
        {  

          


            if (ExisteLavado(id))
            {

                return _lavados[id];

            }
            else
            {

                return null;
            }
        }


        public static int ReemplazarLavado(Lavado nuevoLavado) {

            int id=nuevoLavado.Id;
            if (ExisteLavado(id)){

                _lavados[id] = nuevoLavado;

                return _lavados.Count();


            }

            return -1;
        
        }

        public static int EliminarLavado(int id)
        {

            if (ExisteLavado(id))
            {
                _lavados.Remove(id);

                return _lavados.Count(); 
            }
            else
            {

                return -1;
            }


        }
        public static decimal PrecioConIva(decimal? precio)
        {
            {
                if (precio.HasValue == true)
                    return precio.Value * (1 + Iva);
                return 0;
            }
        }
       

        
}


}
    


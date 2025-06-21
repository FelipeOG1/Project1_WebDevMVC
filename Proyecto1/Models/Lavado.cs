using AspNetCoreGeneratedDocument;
using Microsoft.Net.Http.Headers;
using System.Globalization;
using System.Security.Cryptography;

namespace Proyecto1.Models
{
    public enum EstadoLavado
    {
        EnProceso,
        Facturado,
        Agendado
    }

    public struct TipoLavado
    {
        public string nombre;
        public List<string> prestaciones;
        public int? precio;

    }
    public class Lavado
    {

        private static int idCounter = 0;

        public string Placa { get; set; }

        public int IdCLiente { get; set; }

        public EstadoLavado Estado { get; set; }

        public TipoLavado Tipo { get; set; }
        public int Id { get; }
        



        private static Dictionary<int, Lavado> _lavados = new Dictionary<int, Lavado>();
        private static HashSet<int> _ids = new HashSet<int>();


        public Lavado(string placa, int idCLiente, EstadoLavado estado, TipoLavado tipo,int ?precio)
        {

            if (precio != null)
            {
                tipo.precio = precio;
            }


            Placa = placa;
            IdCLiente = idCLiente;
            Estado = estado;
            Tipo = tipo;
            Id=idCounter++;


                             
        
        } 
        public Lavado() { }
        
        
        public static TipoLavado inicializarTipo(string name)
        {
            TipoLavado tipo = new TipoLavado();

            switch (name.ToLower())
            {
                case "básico":
                    tipo.nombre = "Básico";
                    tipo.prestaciones = new List<string> { "Lavado", "Aspirado", "Encerado" };
                    tipo.precio = 5000;
                    break;

                case "premium":
                    tipo.nombre = "Premium";
                    tipo.prestaciones = new List<string> { "Lavado", "Aspirado", "Encerado", "Limpieza profunda de asientos" };
                    tipo.precio = 8000;
                    break;

                case "deluxe":
                    tipo.nombre = "Deluxe";
                    tipo.prestaciones = new List<string> { "Lavado", "Aspirado", "Encerado", "Limpieza profunda de asientos", "Corrección de pintura", "Tratamiento nanocerámico" };
                    tipo.precio = 12000;
                    break;

                case "la joya":
                    tipo.nombre = "La Joya";
                    tipo.prestaciones = new List<string> { "Todo lo anterior", "Pulidos", "Tratamientos hidrofóbicos", "Extras a convenir" };
                    tipo.precio = 0;
                    break;

                default:
                    tipo.nombre = "Desconocido";
                    tipo.prestaciones = new List<string>();
                    tipo.precio = 0;
                    break;
            }

            return tipo;
        }


        public static int AgregarLavado(Lavado lav)
        {


            if (_ids.Contains(lav.Id))
            {

                return -1;
            }

            _lavados.Add(lav.Id, lav);
            _ids.Add(lav.Id);


            return _lavados.Count;

        }


        public bool ExisteLavado(int id)
        {

            if (_ids.Contains(id)) return true;

            return false;   
        
       
        }

        public static List<Lavado> MostrarLavados()
        {

           return _lavados.Values.ToList();
            
        }






    }
    

}
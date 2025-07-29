using System.ComponentModel.DataAnnotations;

namespace Proyecto1.Models
{
    public class Cliente
    {

        public enum FrecuenciaLavado
        {
            Semanal,
            Quincenal,
            Mensual,
        }

            [Required][Key]
            public int? Identificacion { get; set; }
            [Required]
            public string NombreCompleto { get; set; }
            [Required]
            public string Provincia { get; set; }
            [Required]
            public string Canton { get; set; }
            [Required]
            public string Distrito { get; set; }
            [Required]
            public string DireccionExacta { get; set; }
            [Required]
            public string Telefono { get; set; }

            [Required]
            public FrecuenciaLavado? PreferenciaLavado { get; set; }
        


       private static Dictionary<int,Cliente> _clientes= new Dictionary<int,Cliente>();
       //Ayuda a encontrar cedulas en  O(1) y pasamos cedula por parametro a funcion buscar Empleado Por cedula
       private static HashSet<int>_identificaciones=new HashSet<int>();
          

        public Cliente() {}



    }
}

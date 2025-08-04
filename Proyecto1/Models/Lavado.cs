using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;
namespace Proyecto1.Models
{
    public enum EstadoLavado
    {
        EnProceso,
        Facturado,
        Agendado
    }
    public class TipoLavado
    {
        public string nombre { get; set; }
        public List<string> prestaciones { get; set; }
        public decimal? precio { get; set; }
        public TipoLavado() { } 

    }
    public class Lavado
    {

    
    public int Id{ get; set; }

    [Required(ErrorMessage = "La placa del vehículo es obligatoria")]
    public string VehiculoPlaca { get; set; } // FK a Vehiculo.Placa

    [Required(ErrorMessage = "La identificación del cliente es obligatoria")]
    public int? ClienteIdentificacion { get; set; } // FK a Cliente.Identificacion
   
    [Required(ErrorMessage = "El estado del lavado es obligatorio")]
    public EstadoLavado Estado { get; set; }

    [Required(ErrorMessage = "El nombre del tipo es obligatorio")]
    public string nombreTipo { get; set; }
    [BindNever][NotMapped]
    public TipoLavado? Tipo{ get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? precio { get; set; }
    public Cliente? Cliente { get; set; }
    public Vehiculo? Vehiculo { get; set;}
    [Column(TypeName = "decimal(18,2)")]
    public decimal? precio_con_iva { get; set; }

        public Lavado()
        {


        } 
      


        
}


}
    


using AspNetCoreGeneratedDocument;
using Microsoft.Net.Http.Headers;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Proyecto1.Repositiories;
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

    public Cliente? cliente { get; set; }
    public Vehiculo? vehiculo { get; set;}
    public decimal? precio_con_iva { get; set; }

    


        public Lavado()
        {


        } 
        

        
}


}
    


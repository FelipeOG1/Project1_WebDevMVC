using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Proyecto1.Models
{
    public class Empleado
    {
        
        [Required]
        public int? Cedula { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
          [Required]
        public DateTime FechaIngreso { get; set; }
          [Required]
        public decimal SalarioPorDia { get; set; }
          [Required]
        public int DiasVacacionesAcumulados { get; set; }
          [Required]
        public DateTime? FechaRetiro { get; set; }
          [Required]
       public decimal MontoLiquidacion { get; set; }
       
        public Empleado() { }

        


            
       
        



    }
}


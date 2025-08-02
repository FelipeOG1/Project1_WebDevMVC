using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace Proyecto1.Models
{
    public class Empleado
    {
        
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? Cedula { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        public DateTime FechaIngreso { get; set; }
        [Required][Column(TypeName = "decimal(18,2)")]

        public decimal SalarioPorDia { get; set; }
        [Required]
        public int DiasVacacionesAcumulados { get; set; }
        [Required]
        public DateTime? FechaRetiro { get; set; }
        [Required][Column(TypeName = "decimal(18,2)")]

       public decimal MontoLiquidacion { get; set; }
       
        public Empleado() { }

        


            
       
        



    }
}


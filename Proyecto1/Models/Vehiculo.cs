
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Vehiculo
{
    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Placa { get; set; }

    [Required]
    public string Marca { get; set; }

    [Required]
    public string Modelo { get; set; }

    [Required]
    public string Traccion { get; set; } // Por ejemplo: "4x4", "Delantera"

    [Required]
    public string Color { get; set; }

    [Required]
    public DateTime UltimaFechaAtencion { get; set; }

    [Required]
    public bool TratamientoEspecialNano { get; set; }

    public Vehiculo() { }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Proyecto1.Utils;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
namespace Proyecto1.Models
{
    public class Vehiculo 
    {
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Traccion { get; set; } // Por ejemplo: "4x4", "Delantera"
        public string Color { get; set; }
        public DateTime UltimaFechaAtencion { get; set; }
        public bool TratamientoEspecialNano { get; set; } // true = sí, false = no


        public Vehiculo() { }
            
    }

}


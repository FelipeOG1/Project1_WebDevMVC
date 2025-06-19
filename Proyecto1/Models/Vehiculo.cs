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


        public Vehiculo(string placa, string marca, string modelo, string traccion, string color, DateTime ultimaFechaAtencion, bool tratamientoEspecialNano)
        {
            Placa = placa;
            Marca = marca;
            Modelo = modelo;
            Traccion = traccion;
            Color = color;
            UltimaFechaAtencion = ultimaFechaAtencion;
            TratamientoEspecialNano = tratamientoEspecialNano;
        }


        public Vehiculo() { }


        private static Dictionary<string, Vehiculo> _vehiculos = new Dictionary<string, Vehiculo>();
        private static HashSet<string> _placas = new HashSet<string>();


        public static int AgregarVehiculo(Vehiculo vehiculo)
        {
            if (_placas.Contains(vehiculo.Placa))
            {
                return -1;
            }

            _vehiculos.Add(vehiculo.Placa, vehiculo);
            _placas.Add(vehiculo.Placa);

            return _vehiculos.Count;
        }

        public static int EliminarVehiculo(string placa)
        {
            if (_placas.Contains(placa))
            {
                _placas.Remove(placa);
                _vehiculos.Remove(placa);
                return _vehiculos.Count;
            }

            return -1;
        }

        public static List<Vehiculo> MostrarVehiculos()
        {
            return _vehiculos.Values.ToList();
        }

        public static Vehiculo BuscarVehiculoPorPlaca(string placa)
        {

            if (ExisteVehiculo(placa))
            {

                return _vehiculos[placa];
            }

            return null;
        
        
        }


        public static bool ExisteVehiculo(string placa)
        {

            if (_placas.Contains(placa)) return true;

            return false;

            
        }



       public static Vehiculo EditarVehiculo(Vehiculo ve)
        {

            if (_placas.Contains(ve.Placa))
            {
                _vehiculos[ve.Placa] = ve;

                return ve;


            }


            return null;
            

            

                       


        }

            
    }

}


using System.Net.Sockets;
using Proyecto1.Models;

namespace Proyecto1.Repositiories
{
    public class VehiculoRepository
    {

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



        public static Vehiculo ReemplazarVehiculo(Vehiculo ve)
        {

            if (_placas.Contains(ve.Placa))
            {
                _vehiculos[ve.Placa] = ve;

                return ve;


            }


            return null;

            

        }
        

         public static void inicializarVehiculoorDefecto()
        {

            Vehiculo vehiculo = new Vehiculo
        {
            Placa = "ABC123",
            Marca = "Toyota",
            Modelo = "RAV4",
            Traccion = "4x4",
            Color = "Rojo",
            UltimaFechaAtencion = new DateTime(2024, 12, 10),
            TratamientoEspecialNano = true
        };

            VehiculoRepository.AgregarVehiculo(vehiculo);
        }



          



        





    }








}


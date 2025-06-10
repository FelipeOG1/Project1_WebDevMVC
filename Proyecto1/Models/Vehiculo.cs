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

        public static Response<Vehiculo> BuscarVehiculoPorPlaca(string placa)
        {
            var response = new Response<Vehiculo>
            {
                Status = false,
                Message = "No existe un vehículo con esa placa",
                Content = null
            };

            if (_placas.Contains(placa))
            {
                response.Status = true;
                response.Message = "Vehículo encontrado";
                response.Content = _vehiculos[placa];
            }

            return response;
        }
    }

}


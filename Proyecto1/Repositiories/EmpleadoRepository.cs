using Proyecto1.Models;

namespace Proyecto1.Repositiories
{
    public class EmpleadoRepository
    {

        private readonly Dictionary<string, Vehiculo> _vehiculos = new();
        private readonly HashSet<string> _placas = new();

        public int AgregarVehiculo(Vehiculo v)
        {

            if (existePlaca(v.Placa)){

                _vehiculos.Add(v.Placa,v);

                return 1;
            }

            return 0;

            
        }

        





        //Utils para este repository


        private bool existePlaca(string placa)
        {
            if (_placas.Contains(placa))
            {
                return true;
            }
            return false;
        }



    }
}

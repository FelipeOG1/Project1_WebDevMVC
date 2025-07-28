using Proyecto1.Models;

namespace Proyecto1.Repositiories
{
    public class EmpleadoRepository
    {

        private static Dictionary<int, Empleado> _empleados = new Dictionary<int, Empleado>();
        //Ayuda a encontrar cedulas en  O(1) y pasamos cedula por parametro a funcion buscar Empleado Por cedula
        private static HashSet<int> _cedulas = new HashSet<int>();

        //El model Binding de MVC me pide tener un constructor vacio




        public static int AgregarEmpleado(Empleado empleado)
        {


            if (_cedulas.Contains(empleado.Cedula.Value))
            {

                return -1;
            }
            _empleados.Add(empleado.Cedula.Value, empleado);
            _cedulas.Add(empleado.Cedula.Value);


            return _empleados.Count;

        }
        public static int EliminarEmpleado(int cedula)
        {

            bool cedulaExiste = _cedulas.Contains(cedula);

            if (cedulaExiste)
            {
                _empleados.Remove(cedula);

                return _empleados.Count();

            }

            return -1;


        }
        public static List<Empleado> MostrarEmpleados()
        {

            List<Empleado> listaEmpleado = new List<Empleado>();

            if (_empleados.Count > 0)
            {

                foreach (var emp in _empleados.Values)
                {

                    listaEmpleado.Add(emp);
                }

            }

            return listaEmpleado;

        }

        public static Empleado BuscarEmpleadoPorCedula(int cedula)
        {


            if (_cedulas.Contains(cedula))
            {

                return _empleados[cedula];
            }

            return null;

        }


        public static void ReemplazarEmpleado(Empleado nuevo)
        {
            //Todo esto tan solo funciona si se respeta la manera en la que se hizo el cliente.
            if (ExisteEmpleado(nuevo.Cedula.Value))
            {
                _empleados[nuevo.Cedula.Value] = nuevo;

            }

        }

        public static bool ExisteEmpleado(int cedula)
        {

            if (_cedulas.Contains(cedula))
            {

                return true;
            }


            return false;


        }

         public static void inicializarEmpleadoPorDefecto()
        {
            Empleado empleado = new Empleado 
            {
                Cedula = 121333289,
                FechaNacimiento = new DateTime(1995, 5, 20),
                FechaIngreso = new DateTime(2020, 1, 15),
                SalarioPorDia = 35000.00m,
                DiasVacacionesAcumulados = 10,
                FechaRetiro = new DateTime(2025, 6, 30),
                MontoLiquidacion = 750000.00m
            };

      
            EmpleadoRepository.AgregarEmpleado(empleado);
        }
        





    }
}

using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Proyecto1.Models
{
    public class Empleado
    {

        public int  Cedula { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaIngreso { get; set; }
        public decimal SalarioPorDia { get; set; }
        public int DiasVacacionesAcumulados { get; set; }
        public DateTime? FechaRetiro { get; set; } // Puede ser null si aún trabaja
        public decimal MontoLiquidacion { get; set; }


       
    


        private static Dictionary<int,Empleado> _empleados = new Dictionary<int,Empleado>();
       //Ayuda a encontrar cedulas en  O(1) y pasamos cedula por parametro a funcion buscar Empleado Por cedula
       private static HashSet<int>_cedulas=new HashSet<int>();
          
        //El model Binding de MVC me pide tener un constructor vacio

        public Empleado() { }

        

        public static int AgregarEmpleado(Empleado empleado)
        {


            if (_cedulas.Contains(empleado.Cedula))
            {

                return -1;
            }
            _empleados.Add(empleado.Cedula, empleado);
            _cedulas.Add(empleado.Cedula);
           

            return _empleados.Count;
            
        }
        public static int EliminarEmpleado(int cedula)
        {

            bool cedulaExiste=_cedulas.Contains(cedula);

            if (cedulaExiste)
            {
                _empleados.Remove(cedula);

                return _empleados.Count();
                
            }

            return -1;

            
        }
        public static List<Empleado> MostrarEmpleados()
        {

            List<Empleado>listaEmpleado = new List<Empleado>();

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
            if (ExisteEmpleado(nuevo.Cedula))
            {
                _empleados[nuevo.Cedula] = nuevo;

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



            
       
        



    }
}

// cédula, fecha de nacimiento, fecha de ingreso a la empresa, salario x día, días de vacaciones acumulados, fecha de retiro, monto de liquidación.

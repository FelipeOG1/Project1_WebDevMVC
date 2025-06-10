using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Proyecto1.Models
{
    public class Empleado
    {

        public int cedula { get; set; }

        public string nombre { get; set; }

      
        public decimal salarioDia { get; set;}



        
       

        private static Dictionary<int,Empleado> _empleados = new Dictionary<int,Empleado>();
       //Ayuda a encontrar cedulas en  O(1) y pasamos cedula por parametro a funcion buscar Empleado Por cedula
       private static HashSet<int>_cedulas=new HashSet<int>();
          
        //El model Binding de MVC me pide tener un constructor vacio

        public Empleado() { }

        public Empleado(int ced, decimal salario_dia,string nom)
        {
            cedula = ced;
            salarioDia = salario_dia;
            nombre = nom;
        }

        public static int AgregarEmpleado(Empleado empleado)
        {


            if (_cedulas.Contains(empleado.cedula))
            {

                return -1;
            }
            _empleados.Add(empleado.cedula, empleado);
            _cedulas.Add(empleado.cedula);
           

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

        public static object BuscarEmpleadoPorCedula(int cedula)
        {


            var response = new Response<Empleado>
            {

                Status = false,
                Message = "No existe un usuario con esa cedula",
                Content = null

            };

            if(_cedulas.Contains(cedula))
            {

                response.Status = true;
                response.Message = "Success";
                response.Content = _empleados[cedula];

                
            }

            return response;

        }
        



    }
}

// cédula, fecha de nacimiento, fecha de ingreso a la empresa, salario x día, días de vacaciones acumulados, fecha de retiro, monto de liquidación.

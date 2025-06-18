using Proyecto1.Models;

namespace Proyecto1.Repositiories
{
    public class ClienteRepository
    {

        Dictionary <int, Empleado>  _empleados = new Dictionary<int,Empleado>();

        HashSet<int>cedulas=new HashSet<int>();
        



        public int AgregarEmpelado(Empleado e)
        {
            if (cedulas.Contains(e.Cedula))
            {

                _empleados.Add(e.Cedula, e);

               return _empleados.Count();  
            }

            return -1;

      
        }




            



    }
}


namespace Proyecto1.Models
{
    public class Modelos
    {


        public List <Vehiculo> Vehiculos { get; private set; } 
        public List<Cliente> Clientes { get; private set; }
        public List<Lavado> Lavados { get; private set; } 
        public List<Empleado> Empleados { get; private set; }

       public Modelos()
        {

            Vehiculos= Vehiculo.MostrarVehiculos();
            Clientes=Cliente.MostrarClientes();
            Lavados= Lavado.MostrarLavados();
            Empleados=Empleado.MostrarEmpleados();
        }





          
    }
}

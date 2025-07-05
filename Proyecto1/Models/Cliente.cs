using System.ComponentModel.DataAnnotations;

namespace Proyecto1.Models
{
    public class Cliente
    {

        public enum FrecuenciaLavado
        {
            Semanal,
            Quincenal,
            Mensual,
        }

            [Required]
            
            public int Identificacion { get; set; }
            [Required]
            public string NombreCompleto { get; set; }
            [Required]
            public string Provincia { get; set; }
            [Required]
            public string Canton { get; set; }
            [Required]
            public string Distrito { get; set; }
            [Required]
            public string DireccionExacta { get; set; }
            [Required]
            public string Telefono { get; set; }

            [Required]
            public FrecuenciaLavado? PreferenciaLavado { get; set; }
        


       private static Dictionary<int,Cliente> _clientes= new Dictionary<int,Cliente>();
       //Ayuda a encontrar cedulas en  O(1) y pasamos cedula por parametro a funcion buscar Empleado Por cedula
       private static HashSet<int>_identificaciones=new HashSet<int>();
          

        public Cliente() {}


        public static int AgregarCliente(Cliente cliente)
        {


            if (ExisteCliente(cliente.Identificacion))
            {

                return -1;
            }


            _clientes.Add(cliente.Identificacion, cliente);
            _identificaciones.Add(cliente.Identificacion);


            return -1;




        }

        public static bool ExisteCliente(int identificacion)
        {

            if (_identificaciones.Contains(identificacion)) return true;


            return false;
        }


        public static int EliminarCliente(int identificacion )
        {

            

            if (ExisteCliente(identificacion))
            {
                _clientes.Remove(identificacion);

                return _clientes.Count();
                
            }

            return -1;

            
        }
        public static List<Cliente> MostrarClientes()
        {

            List<Cliente>listaClientes = new List<Cliente>();

            if (_clientes.Count > 0)
            {

                foreach (var emp in _clientes.Values)
                {

                    listaClientes.Add(emp);
                }

            }

            return listaClientes;
            
        }

        public static Cliente BuscarCliente(int identificacion)
        {


            if (ExisteCliente(identificacion))
            {

               return _clientes[identificacion];
            }

            return null;

        }


        public static void ReemplazarCliente(Cliente nuevoCliente)
        {
            //Todo esto tan solo funciona si se respeta la manera en la que se hizo el cliente.
            if (ExisteCliente(nuevoCliente.Identificacion))
            {
                _clientes[nuevoCliente.Identificacion] = nuevoCliente;

            }
            
        }

       


          


    }
}

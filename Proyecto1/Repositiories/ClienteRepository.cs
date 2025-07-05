using System.Diagnostics.Tracing;
using System.Net;
using Proyecto1.Models;

namespace Proyecto1.Repositiories
{
    public class ClienteRepository
    {


       private static Dictionary<int,Cliente> _clientes= new Dictionary<int,Cliente>();
       private static HashSet<int>_identificaciones=new HashSet<int>();
          

        public static int AgregarCliente(Cliente cliente)
        {


            if (ExisteCliente(cliente.Identificacion))
            {

                return -1;
            }


            _clientes.Add(cliente.Identificacion, cliente);
            _identificaciones.Add(cliente.Identificacion);


            return _clientes.Count();




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
            if (_clientes.Count() > 0)
            {
                List<Cliente> lista_clientes = _clientes.Values.ToList();

                return lista_clientes;

            }



            return null;



            
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

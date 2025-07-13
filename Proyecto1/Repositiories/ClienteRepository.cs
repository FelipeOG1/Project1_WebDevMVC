using System.Diagnostics.Tracing;
using System.Net;
using Proyecto1.Models;

namespace Proyecto1.Repositiories
{
    public class ClienteRepository
    {


        private static Dictionary<int, Cliente> _clientes = new Dictionary<int, Cliente>();
        private static HashSet<int> _identificaciones = new HashSet<int>();
       

         public static int AgregarCliente(Cliente cliente)
        {
             
            int id = cliente.Identificacion.Value;

            if (ExisteCliente(id))
            {
                return -1;
            }

            _clientes.Add(id, cliente);
            _identificaciones.Add(id);

            return _clientes.Count;
        }

        public static bool ExisteCliente(int identificacion)
        {
            return _identificaciones.Contains(identificacion);
        }

        public static int EliminarCliente(int identificacion)
        {
            if (ExisteCliente(identificacion))
            {
                _clientes.Remove(identificacion);
                _identificaciones.Remove(identificacion);
                return _clientes.Count;
            }

            return -1;
        }

        public static List<Cliente> MostrarClientes()
        {
            return new List<Cliente>(_clientes.Values);
        }

        public static Cliente BuscarCliente(int identificacion)
        {
            if (ExisteCliente(identificacion))
            {
                return _clientes[identificacion];
            }

            return null;
        }

        public static int ReemplazarCliente(Cliente nuevoCliente)
        {

            
            int id = nuevoCliente.Identificacion.Value;

            if (ExisteCliente(id))
            {
                _clientes[id] = nuevoCliente;


            }
            else
            {

                return -1;
            }

            return 0;
            
        }
    }
}


       




        


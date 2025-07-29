using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Net;
using Proyecto1.Data;
using Proyecto1.Models;

namespace Proyecto1.Repositiories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;
        
        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> AgregarCliente(Cliente cliente)
        {

            int? id = cliente.Identificacion.Value;

            if (await ExisteCliente(id.Value))
            {
                return -1;
            }

            await _context.Clientes.AddAsync(cliente);

            int result = await _context.SaveChangesAsync();

            return result;

        }

        public async Task<bool> ExisteCliente(int identificacion)
        {
            var cliente = await _context.Clientes.FindAsync(identificacion);

            bool response = cliente != null ? true : false;


            return response; 
           
        }

        public  int EliminarCliente(int identificacion)
        {
            if (ExisteCliente(identificacion))
            {
                _clientes.Remove(identificacion);
                _identificaciones.Remove(identificacion);
                return _clientes.Count;
            }

            return -1;
        }

        public  List<Cliente> MostrarClientes()
        {
            return new List<Cliente>(_clientes.Values);
        }

        public  Cliente BuscarCliente(int identificacion)
        {
            if (ExisteCliente(identificacion))
            {
                return _clientes[identificacion];
            }

            return null;
        }

        public  int ReemplazarCliente(Cliente nuevoCliente)
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

        public  void inicializarClientePorDefecto()
        {

            Cliente cliente = new Cliente
            {
                Identificacion = 118540660,
                NombreCompleto = "Martin del campo",
                Provincia = "San José",
                Canton = "San vicente",
                Distrito = "Moravia",
                DireccionExacta = "Del súper 100m norte",
                Telefono = "88888888",
                PreferenciaLavado = 0,

            };

            ClienteRepository.AgregarCliente(cliente);
        }
    }
}


       




        


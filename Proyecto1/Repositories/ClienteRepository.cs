using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Net;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.Query;
using Proyecto1.Data;
using Proyecto1.Models;
using Microsoft.EntityFrameworkCore;

namespace Proyecto1.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
       
        private readonly AppDbContext _Dbcontext;

        public ClienteRepository(AppDbContext context)
        {
            _Dbcontext = context;
        }
        public async Task<int> AgregarCliente(Cliente cliente)
        {
            int? id = cliente.Identificacion.Value;

            if (await ExisteCliente(id.Value) != null)
            {
                return (int)ErroresCliente.clienteYaExiste;
            }

            await _Dbcontext.Clientes.AddAsync(cliente);

            int result = await _Dbcontext.SaveChangesAsync();
            if (result == 0)
            {
                return (int)ErroresCliente.clienteNoFueAgreado;
            }

            return result;

        }

        public async Task<Cliente>? ExisteCliente(int identificacion)
        {
            var cliente = await _Dbcontext.Clientes.FindAsync(identificacion);

            return cliente;


        }
        public async Task<int> EliminarCliente(int identificacion)
        {
            Cliente cliente = await ExisteCliente(identificacion);

            if (cliente != null)
            {
                //Esto solo cambia el estado en memoria a la acciòn que quiere ejecutar db. Por lo que aunque no se encontrase igual el state es deleted.

                _Dbcontext.Remove(cliente);

                //aqui si que podemos saber la cantidad de filas afectadas por lo que le resultado ahora se guarda dentro de un response
                int response = await _Dbcontext.SaveChangesAsync();

            

                if (response > 0) return response;

                return (int)ErroresCliente.clienteNoFueEliminado;
            }

            return (int)ErroresCliente.clienteNoEncontrado;
        }


        public async Task<List<Cliente>> MostrarClientes()
        {
            return await _Dbcontext.Clientes.ToListAsync();
        }

        public async Task<Cliente> ObtenerClientePorId(int identificacion)
        {
            //Verificar que no sea null
            return await ExisteCliente(identificacion);

        }

        public async Task<int> ActualizarCliente (Cliente nuevoCliente)
        {
            
            _Dbcontext.Clientes.Update(nuevoCliente);

            int result = await _Dbcontext.SaveChangesAsync();

            return result;

        }

        public async Task inicializarClientePorDefecto()
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


            _Dbcontext.AddAsync(cliente);

        }
    }
}


       




        


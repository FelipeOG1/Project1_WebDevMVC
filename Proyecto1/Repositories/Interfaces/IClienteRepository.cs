using Proyecto1.Models;

public interface IClienteRepository
{
    Task<List<Cliente>>MostrarClientes();
    Task<Cliente>ObtenerClientePorId(int id);
    Task<int> AgregarCliente(Cliente cliente);
    Task<int> ActualizarCliente(Cliente cliente);
    Task<int> EliminarCliente(int id);




}

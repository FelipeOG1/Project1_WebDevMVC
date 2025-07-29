using Proyecto1.Models;

public interface IClienteRepository
{

    Task<IEnumerable<Cliente>> ObtenerClientes();
    Task<Cliente> ObtenerCLientePorId(int id);
    Task AgregarCliente(Cliente cliente);
    Task ActualziarCliente(Cliente cliente);
    Task EliminarCLiente(int id);


}
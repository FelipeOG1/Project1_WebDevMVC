using Proyecto1.Models;

namespace Proyecto1.Repositiories
{

public interface IEmpleadoRepository
{
    Task<List<Empleado>> MostrarEmpleados();
    Task<Empleado> ObtenerEmpleadoPorId(int id);
    Task<int> AgregarEmpleado(Empleado empleado);
    Task<int> ActualizarEmpleado(Empleado empleado);
    Task<int> EliminarEmpleado(int id);
}

}

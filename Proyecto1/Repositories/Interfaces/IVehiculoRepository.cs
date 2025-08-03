using Proyecto1.Models;
public interface IVehiculoRepository
{
    Task<List<Vehiculo>> MostrarVehiculos();
    Task<Vehiculo> ObtenerVehiculoPorPlaca(string placa);
    Task<int> AgregarVehiculo(Vehiculo vehiculo);
    Task<int> ActualizarVehiculo(Vehiculo vehiculo);
    Task<int> EliminarVehiculo(string placa);

}
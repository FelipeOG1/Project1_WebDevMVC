namespace Proyecto1.Repositories
{
    public enum ErroresCliente
    {
        clienteNoEncontrado = -1,
        clienteNoFueEliminado = -2,
        clienteNoFueAgreado = -3,
        clienteNoFueModificado = -4,
        clienteYaExiste = -5,
    }

    public enum ErroresEmpleado
    {
        EmpleadoNoEncontrado = -1,
        EmpleadoNoFueEliminado = -2,
        EmpleadoNoFueAgreado = -3,
        EmpleadoNoFueModificado = -4,
        EmpleadoYaExiste = -5,
    }

    public enum ErroresVehiculo
    {
    VehiculoNoEncontrado = -1,
    VehiculoNoFueEliminado = -2,
    VehiculoNoFueAgregado = -3,
    VehiculoNoFueModificado = -4,
    VehiculoYaExiste = -5,
    }

}

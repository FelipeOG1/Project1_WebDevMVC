using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proyecto1.Repositiories;
using Proyecto1.Data;
using Proyecto1.Models;
namespace Proyecto1.Repositories
{
    public class EmpleadoRepository : IEmpleadoRepository 
    {
        private readonly AppDbContext _Dbcontext;

        public EmpleadoRepository(AppDbContext context)
        {
            _Dbcontext = context;
        }

        public async Task<int> AgregarEmpleado(Empleado empleado)
        {
            int? cedula = empleado.Cedula.Value;

            if (await ExisteEmpleado(cedula.Value) != null)
            {
                return (int)ErroresEmpleado.EmpleadoYaExiste;
            }

            await _Dbcontext.Empleados.AddAsync(empleado);

            int result = await _Dbcontext.SaveChangesAsync();
            if (result == 0)
            {
                return (int)ErroresEmpleado.EmpleadoNoFueAgreado;
            }

            return result;
        }

        public async Task<Empleado>? ExisteEmpleado(int id)
        {
            var empleado = await _Dbcontext.Empleados.FindAsync(id);
            return empleado;
        }

        public async Task<int> EliminarEmpleado(int id)
        {
            Empleado empleado = await ExisteEmpleado(id);

            if (empleado != null)
            {
                _Dbcontext.Remove(empleado);

                int response = await _Dbcontext.SaveChangesAsync();

                if (response > 0) return response;

                return (int)ErroresEmpleado.EmpleadoNoFueEliminado;
            }

            return (int)ErroresEmpleado.EmpleadoNoEncontrado;
        }

        public async Task<List<Empleado>> MostrarEmpleados()
        {
            return await _Dbcontext.Empleados.ToListAsync();
        }

        public async Task<Empleado> ObtenerEmpleadoPorId(int id)
        {
            return await ExisteEmpleado(id);
        }

        public async Task<int> ActualizarEmpleado(Empleado nuevoEmpleado)
        {
            _Dbcontext.Empleados.Update(nuevoEmpleado);

            int result = await _Dbcontext.SaveChangesAsync();

            return result;
        }

        public async Task InicializarEmpleadoPorDefecto()
        {
            Empleado empleado = new Empleado
            {
                Cedula = 123456789,
                FechaNacimiento = new DateTime(1990, 5, 20),
                FechaIngreso = DateTime.Today.AddYears(-2),
                SalarioPorDia = 30000m,
                DiasVacacionesAcumulados = 15,
                FechaRetiro = null, // Aún no se ha retirado
                MontoLiquidacion = 0m
       
            };
            await _Dbcontext.Empleados.AddAsync(empleado);
            await _Dbcontext.SaveChangesAsync();
        }
    }
}
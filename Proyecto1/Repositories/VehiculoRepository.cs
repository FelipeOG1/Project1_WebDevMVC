
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Net;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.Query;
using Proyecto1.Data;
using Proyecto1.Models;
using Microsoft.EntityFrameworkCore;
using Proyecto1.Repositories;


namespace Proyecto1.Repositiories
{
    public class VehiculoRepository : IVehiculoRepository
    {

        private readonly AppDbContext _Dbcontext;

        public VehiculoRepository(AppDbContext context)
        {
            _Dbcontext = context;
        }

        public async Task<int> AgregarVehiculo(Vehiculo vehiculo)
        {
            string placa = vehiculo.Placa;

            if (await ExisteVehiculo(placa) != null)
            {
                return (int)ErroresVehiculo.VehiculoYaExiste;
            }

            await _Dbcontext.Vehiculos.AddAsync(vehiculo);

            int result = await _Dbcontext.SaveChangesAsync();
            if (result == 0)
            {
                return (int)ErroresVehiculo.VehiculoNoFueAgregado;
            }

            return result;
        }

        public async Task<Vehiculo>? ExisteVehiculo(string placa)
        {
            return await _Dbcontext.Vehiculos.FindAsync(placa);
        }

        public async Task<int> EliminarVehiculo(string placa)
        {
            Vehiculo vehiculo = await ExisteVehiculo(placa);

            if (vehiculo != null)
            {
                _Dbcontext.Remove(vehiculo);

                int response = await _Dbcontext.SaveChangesAsync();

                if (response > 0) return response;

                return (int)ErroresVehiculo.VehiculoNoFueEliminado;
            }

            return (int)ErroresVehiculo.VehiculoNoEncontrado;
        }

        public async Task<List<Vehiculo>> MostrarVehiculos()
        {
            return await _Dbcontext.Vehiculos.ToListAsync();
        }

        public async Task<Vehiculo> ObtenerVehiculoPorPlaca(string placa)
        {
            return await ExisteVehiculo(placa);
        }

        public async Task<int> ActualizarVehiculo(Vehiculo nuevoVehiculo)
        {
            _Dbcontext.Vehiculos.Update(nuevoVehiculo);
            int result = await _Dbcontext.SaveChangesAsync();
            return result;
        }

        public async Task InicializarVehiculoPorDefecto()
        {
            Vehiculo vehiculo = new Vehiculo
            {
                Placa = "ABC123",
                Marca = "Toyota",
                Modelo = "Corolla",
                Traccion = "Delantera",
                Color = "Blanco",
                UltimaFechaAtencion = DateTime.Now.AddMonths(-1),
                TratamientoEspecialNano = true
            };

            await _Dbcontext.Vehiculos.AddAsync(vehiculo);
            await _Dbcontext.SaveChangesAsync();
        }









    }








}


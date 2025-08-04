
using Proyecto1.Models;
using Proyecto1.Data;
using Proyecto1.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace Proyecto1.Repositiories
{

    public class LavadoRepository:ILavadoRepository
    {


        private const decimal Iva = 0.13m;
        private readonly AppDbContext _Dbcontext;
        public LavadoRepository(AppDbContext context)
        {
            _Dbcontext = context;
        }

        public static TipoLavado inicializarTipo(string name)
        {
            TipoLavado tipo = new TipoLavado();

            switch (name.ToLower())
            {
                case "básico":
                    tipo.nombre = "Básico";
                    tipo.prestaciones = new List<string> { "Lavado", "Aspirado", "Encerado" };
                    tipo.precio = 5000m;
                    break;

                case "premium":
                    tipo.nombre = "Premium";
                    tipo.prestaciones = new List<string> { "Lavado", "Aspirado", "Encerado", "Limpieza profunda de asientos" };
                    tipo.precio = 8000m;
                    break;

                case "deluxe":
                    tipo.nombre = "Deluxe";
                    tipo.prestaciones = new List<string> { "Lavado", "Aspirado", "Encerado", "Limpieza profunda de asientos", "Corrección de pintura", "Tratamiento nanocerámico" };
                    tipo.precio = 12000m;
                    break;

                case "la joya":
                    tipo.nombre = "La Joya";
                    tipo.prestaciones = new List<string> { "Todo lo anterior", "Pulidos", "Tratamientos hidrofóbicos", "Extras a convenir" };
                    tipo.precio = 0m;
                    break;

                default:
                    tipo.nombre = "Desconocido";
                    tipo.prestaciones = new List<string>();
                    tipo.precio = 0m;
                    break;
            }

            return tipo;
        }

        public async Task<int> AgregarLavado(Lavado lav, TipoLavado tipo)
        {
            
            int id_cliente = lav.ClienteIdentificacion.Value;
            string placa_vehiculo = lav.VehiculoPlaca;

            Vehiculo vehiculo = await _Dbcontext.Vehiculos.FindAsync(placa_vehiculo);
            Cliente cliente = await _Dbcontext.Clientes.FindAsync(id_cliente);

            if (vehiculo == null || cliente == null)
            {
                return (int)ErroresLavado.ClienteOVehiculoNoExiste;
            }
            lav.Vehiculo = vehiculo ;
            lav.Tipo = tipo;
            if (lav.Tipo.nombre == "La Joya")
            {

                if (!lav.precio.HasValue)
                {
                    return (int)ErroresLavado.LavadoNecesitaPrecio;

                }

                tipo.precio = lav.precio;
            }
            else
            {

                lav.precio = tipo.precio;
            }

            lav.precio_con_iva = obtenerPrecioConIva(lav.precio);
            await _Dbcontext.Lavados.AddAsync(lav);

            int result = await _Dbcontext.SaveChangesAsync();

            if (result == 0)
            {
                return (int)ErroresVehiculo.VehiculoNoFueAgregado;
            }
        
            return result;
        }

         public async Task<Lavado>? ExisteLavado(int id)
         {
            Lavado lavado = await _Dbcontext.Lavados.FindAsync(id);

            return lavado;


         }
       
       public async Task<List<Lavado>> MostrarLavados()
        {

            return await _Dbcontext.Lavados.ToListAsync();
        }


        public async Task<Lavado> ObtenerLavadoPorId(int id)
        {
            return await ExisteLavado(id);
        }

        public async Task<int> ActualizarLavado(Lavado nuevoLavado, TipoLavado tipo)
        {


            int id = nuevoLavado.Id;

            Lavado  lavado= await ExisteLavado(id);

            if (lavado != null)
            {

                Vehiculo vehiculo = await _Dbcontext.Vehiculos.FindAsync(nuevoLavado.VehiculoPlaca);
                Cliente cliente = await _Dbcontext.Clientes.FindAsync(nuevoLavado.ClienteIdentificacion);

                if (vehiculo == null || cliente == null)
                {
                    return (int)ErroresLavado.ClienteOVehiculoNoExiste;
                }
                nuevoLavado.Tipo = tipo;
                if (nuevoLavado.Tipo.nombre == "La Joya")
                {

                    if (!nuevoLavado.precio.HasValue)
                    {
                        return (int)ErroresLavado.LavadoNecesitaPrecio;

                    }

                    tipo.precio = nuevoLavado.precio;
                }
                else
                {

                    nuevoLavado.precio = tipo.precio;

                }

                nuevoLavado.precio_con_iva = obtenerPrecioConIva(nuevoLavado.precio);


                lavado.precio = nuevoLavado.precio;
                lavado.precio_con_iva = nuevoLavado.precio_con_iva;
                lavado.nombreTipo = nuevoLavado.nombreTipo;
                lavado.VehiculoPlaca = nuevoLavado.VehiculoPlaca;
                lavado.ClienteIdentificacion = nuevoLavado.ClienteIdentificacion;
                lavado.Estado = nuevoLavado.Estado;
                int result = await _Dbcontext.SaveChangesAsync();

                if (result == 0)
                {
                    return (int)ErroresLavado.LavadoNoFueModificado;

                }

                return result;          
                


            }

            return (int)ErroresLavado.LavadoNoEncontrado;

        }

         public async Task<int> EliminarLavado(int id)
        {
            Lavado lavado = await ExisteLavado(id);

            if (lavado != null)
            {
                //Esto solo cambia el estado en memoria a la acciòn que quiere ejecutar db. Por lo que aunque no se encontrase igual el state es deleted.

                _Dbcontext.Remove(lavado);

                //aqui si que podemos saber la cantidad de filas afectadas por lo que le resultado ahora se guarda dentro de un response
                int response = await _Dbcontext.SaveChangesAsync();

            

                if (response > 0) return response;

                return (int)ErroresLavado.LavadoNoFueEliminado;
            }

            return (int)ErroresLavado.LavadoNoEncontrado;
        }



        public static decimal obtenerPrecioConIva(decimal? precio)
        {
            {
                if (precio.HasValue == true)
                    return precio.Value * (1 + Iva);
                return 0;
            }
        }


       public async Task InicializarLavadoPorDefecto()
        {
         Lavado lavado = new Lavado
            {
            VehiculoPlaca = "ABC123",
            ClienteIdentificacion = 118540660,
            Estado = EstadoLavado.EnProceso,
            nombreTipo = "Deluxe",
            precio = 5000,
            precio_con_iva = 5650
        };

    await _Dbcontext.AddAsync(lavado);
    await _Dbcontext.SaveChangesAsync();
}
        
 
    }




}
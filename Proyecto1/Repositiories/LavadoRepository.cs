
using Proyecto1.Models;



namespace Proyecto1.Repositiories
{

    public class LavadoRepository
    {


        private static int idCounter = 1;
        private static Dictionary<int, Lavado> _lavados = new Dictionary<int, Lavado>();
        private static HashSet<int> _ids = new HashSet<int>();
        private const decimal Iva = 0.13m;

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



        public static int AgregarLavado(Lavado lav, TipoLavado tipo)
        {

            if (_ids.Contains(lav.Id))
            {

                return -1;
            }

            int id_cliente = lav.IdCLiente;
            string placa_vehiculo = lav.Placa;
            bool respuestaCliente = ClienteRepository.ExisteCliente(id_cliente);
            bool respuestaVehiculo = VehiculoRepository.ExisteVehiculo(placa_vehiculo);

            if (!respuestaCliente || !respuestaVehiculo)
            {

                return -2;
            }

            Cliente clienteLavado = ClienteRepository.BuscarCliente(id_cliente);
            Vehiculo clienteVehiculo = VehiculoRepository.BuscarVehiculoPorPlaca(placa_vehiculo);



            int currentId = idCounter++;
            lav.cliente = clienteLavado;
            lav.vehiculo = clienteVehiculo;
            lav.Tipo = tipo;
            lav.Id = idCounter++;
            if (lav.Tipo.nombre == "La Joya")
            {

                if (!lav.precio.HasValue)
                {
                    return -1;

                }

                tipo.precio = lav.precio;
            }
            else
            {

                lav.precio = tipo.precio;
            }

            lav.precio_con_iva = obtenerPrecioConIva(lav.precio);

            _lavados.Add(lav.Id, lav);


            _ids.Add(lav.Id);

            return _lavados.Count;
        }


        public static bool ExisteLavado(int id)
        {

            if (_ids.Contains(id)) return true;

            return false;


        }

        public static List<Lavado> MostrarLavados()
        {

            return _lavados.Values.ToList();

        }

        public static Lavado BucarLavadoPorId(int id)
        {



            if (ExisteLavado(id))
            {

                return _lavados[id];

            }
            else
            {

                return null;
            }
        }


        public static int ReemplazarLavado(Lavado nuevoLavado, TipoLavado tipo)
        {



            int id = nuevoLavado.Id;
            if (ExisteLavado(id))
            {

                nuevoLavado.Tipo = tipo;
                if (nuevoLavado.Tipo.nombre == "La Joya")
                {

                    if (!nuevoLavado.precio.HasValue)
                    {
                        return -1;

                    }

                    tipo.precio = nuevoLavado.precio;
                }
                else
                {

                    nuevoLavado.precio = tipo.precio;
                }

                nuevoLavado.precio_con_iva = obtenerPrecioConIva(nuevoLavado.precio);


                _lavados[id] = nuevoLavado;

                return _lavados.Count();


            }

            return -1;

        }

        public static int EliminarLavado(int id)
        {

            if (ExisteLavado(id))
            {
                _lavados.Remove(id);

                return _lavados.Count();
            }
            else
            {

                return -1;
            }


        }

        public static decimal obtenerPrecioConIva(decimal? precio)
        {
            {
                if (precio.HasValue == true)
                    return precio.Value * (1 + Iva);
                return 0;
            }
        }


        public static void inicialiarLavadoPorDefecto()
        {

            Lavado lavado = new Lavado
            {
                Placa = "ABC123",
                IdCLiente = 118540660,
                Estado = EstadoLavado.EnProceso,
                nombreTipo = "deluxe",

            };

            TipoLavado tipo = inicializarTipo(lavado.nombreTipo);

            int res = LavadoRepository.AgregarLavado(lavado, tipo);

            Console.WriteLine(res);
            
            
        }

     
        
 
    }




}
using System.Collections.Generic;
using System.Threading.Tasks;
using Proyecto1.Models; // Asegúrate de que este namespace sea correcto

public interface ILavadoRepository
{
    Task<List<Lavado>> MostrarLavados();
    Task<Lavado> ObtenerLavadoPorId(int id);
    Task<int> AgregarLavado(Lavado lavado,TipoLavado tipo);
    Task<int> ActualizarLavado(Lavado lavado,TipoLavado tipo);
    Task<int> EliminarLavado(int id);
}
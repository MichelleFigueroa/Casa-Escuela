using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.EstudiantePreceptoriaDTOs;

namespace CasaEscuela.BL.Interfaces
{
    public interface IEstudiantePreceptoriaBL
    {
        Task<int> CrearAsync(EstudiantePreceptoriaMantDTO pPreceptoria);
        Task<int> ModificarAsync(EstudiantePreceptoriaMantDTO pPreceptoria);
        Task<int> EliminarAsync(EstudiantePreceptoriaMantDTO pPreceptoria);
        Task<EstudiantePreceptoriaMantDTO> ObtenerPorIdAsync(EstudiantePreceptoriaMantDTO pPreceptoria);
        Task<PaginacionOutputDTO<List<EstudiantePreceptoriaMantDTO>>> BuscarAsync(EstudiantePreceptoriaBuscarDTO pPreceptoria);
    }
}
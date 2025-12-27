using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.EstudianteDTOs;

namespace CasaEscuela.BL.Interfaces
{
    public interface IEstudianteBL
    {
        Task<int> CrearAsync(EstudianteMantDTO pEstudiante);
        Task<int> ModificarAsync(EstudianteMantDTO pEstudiante);
        Task<int> EliminarAsync(EstudianteMantDTO pEstudiante);
        Task<EstudianteMantDTO> ObtenerPorIdAsync(EstudianteMantDTO pEstudiante);
        Task<PaginacionOutputDTO<List<EstudianteMantDTO>>> BuscarAsync(EstudianteBuscarDTO pEstudiante);
    }
}
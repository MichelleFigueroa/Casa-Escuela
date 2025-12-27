using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.EntrevistaDTOs;

namespace CasaEscuela.BL.Interfaces
{
    public interface IEntrevistaBL
    {
        Task<int> CrearAsync(EntrevistaMantDTO pEntrevista);
        Task<int> ModificarAsync(EntrevistaMantDTO pEntrevista);
        Task<int> EliminarAsync(EntrevistaMantDTO pEntrevista);
        Task<EntrevistaMantDTO> ObtenerPorIdAsync(EntrevistaMantDTO pEntrevista);
        Task<PaginacionOutputDTO<List<EntrevistaMantDTO>>> BuscarAsync(EntrevistaBuscarDTO pEntrevista);
    }
}
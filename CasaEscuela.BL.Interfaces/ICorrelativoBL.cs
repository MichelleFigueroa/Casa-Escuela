using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.CorrelativoDTOs;

namespace CasaEscuela.BL.Interfaces
{
    public interface ICorrelativoBL
    {
        Task<int> CrearAsync(CorrelativoMantDTO pCorrelativo);
        Task<int> ModificarAsync(CorrelativoMantDTO pCorrelativo);
        Task<int> EliminarAsync(CorrelativoMantDTO pCorrelativo);
        Task<CorrelativoMantDTO> ObtenerPorIdAsync(CorrelativoMantDTO pCorrelativo);
        Task<PaginacionOutputDTO<List<CorrelativoMantDTO>>> BuscarAsync(CorrelativoBuscarDTO pCorrelativo);
    }
}
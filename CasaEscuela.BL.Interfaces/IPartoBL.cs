using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.PartoDTOs;

namespace CasaEscuela.BL.Interfaces
{
    public interface IPartoBL
    {
        Task<int> CrearAsync(PartoMantDTO pParto);
        Task<int> ModificarAsync(PartoMantDTO pParto);
        Task<int> EliminarAsync(PartoMantDTO pParto);
        Task<PartoMantDTO> ObtenerPorIdAsync(PartoMantDTO pParto);
        Task<PaginacionOutputDTO<List<PartoMantDTO>>> BuscarAsync(PartoBuscarDTO pParto);
    }
}
using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.AntePrenatalesDTOs;

namespace CasaEscuela.BL.Interfaces
{
    public interface IAntePrenatalesBL
    {
        Task<int> CrearAsync(AntePrenatalesMantDTO pAntePrenatales);
        Task<int> ModificarAsync(AntePrenatalesMantDTO pAntePrenatales);
        Task<int> EliminarAsync(AntePrenatalesMantDTO pAntePrenatales);
        Task<AntePrenatalesMantDTO> ObtenerPorIdAsync(AntePrenatalesMantDTO pAntePrenatales);
        Task<PaginacionOutputDTO<List<AntePrenatalesMantDTO>>> BuscarAsync(AntePrenatalesBuscarDTO pAntePrenatales);
    }
}
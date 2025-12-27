using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.ComplicacionPartoDTOs;

namespace CasaEscuela.BL.Interfaces
{
    public interface IComplicacionPartoBL
    {
        Task<int> CrearAsync(ComplicacionPartoMantDTO pComplicacion);
        Task<int> ModificarAsync(ComplicacionPartoMantDTO pComplicacion);
        Task<int> EliminarAsync(ComplicacionPartoMantDTO pComplicacion);
        Task<ComplicacionPartoMantDTO> ObtenerPorIdAsync(ComplicacionPartoMantDTO pComplicacion);
        Task<PaginacionOutputDTO<List<ComplicacionPartoMantDTO>>> BuscarAsync(ComplicacionPartoBuscarDTO pComplicacion);
    }
}
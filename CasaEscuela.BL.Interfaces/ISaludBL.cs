using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.SaludDTOs; 

namespace CasaEscuela.BL.Interfaces
{
    public interface ISaludBL
    {
        Task<int> CrearAsync(SaludMantDTO pSalud);

        Task<int> ModificarAsync(SaludMantDTO pSalud);

        Task<int> EliminarAsync(SaludMantDTO pSalud);

        Task<SaludMantDTO> ObtenerPorIdAsync(SaludMantDTO pSalud);

        Task<PaginacionOutputDTO<List<SaludMantDTO>>> BuscarAsync(SaludBuscarDTO pSalud);
    }
}
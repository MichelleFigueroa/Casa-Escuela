using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.FamiliaDTOs;

namespace CasaEscuela.BL.Interfaces
{
    public interface IFamiliaBL
    {
        Task<int> CrearAsync(FamiliaMantDTO pFamilia);
        Task<int> ModificarAsync(FamiliaMantDTO pFamilia);
        Task<int> EliminarAsync(FamiliaMantDTO pFamilia);
        Task<FamiliaMantDTO> ObtenerPorIdAsync(FamiliaMantDTO pFamilia);
        Task<PaginacionOutputDTO<List<FamiliaMantDTO>>> BuscarAsync(FamiliaBuscarDTO pFamilia);
    }
}
using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.EvaluacionDMDTOs;

namespace CasaEscuela.BL.Interfaces
{
    public interface IEvaluacionDMBL
    {
        Task<int> CrearAsync(EvaluacionDMMantDTO pEvaluacion);
        Task<int> ModificarAsync(EvaluacionDMMantDTO pEvaluacion);
        Task<int> EliminarAsync(EvaluacionDMMantDTO pEvaluacion);
        Task<EvaluacionDMMantDTO> ObtenerPorIdAsync(EvaluacionDMMantDTO pEvaluacion);
        Task<PaginacionOutputDTO<List<EvaluacionDMMantDTO>>> BuscarAsync(EvaluacionDMBuscarDTO pEvaluacion);
    }
}
using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.EstudianteDTOs;
using CasaEscuela.BL.DTOs.PreceptoriaDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasaEscuela.BL.Interfaces
{
    public interface IPreceptoriaBL
    {
        Task<int> GuardarPreceptoriaAsync(EstudiantePreceptoriaMantDTO preceptoria);
        Task<List<EstudiantePreceptoriaMantDTO>> ObtenerPreceptoriasPorEstudianteAsync(int idEstudiante);
        Task<EstudiantePreceptoriaMantDTO> ObtenerPreceptoriaPorIdAsync(int id);
    }
}

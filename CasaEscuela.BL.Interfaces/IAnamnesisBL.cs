using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.EstudianteDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasaEscuela.BL.Interfaces
{
    public interface IAnamnesisBL
    {
        public async Task<int> GuardarAnamnesisAsync(
        EstudianteMantDTO estudiante,
        List<EstudianteFamiliarMantDTO> familiares,
        AnamnesisMantDTO anamnesis)
        {
            // Tu lógica aquí
            return 1; // ejemplo temporal
        }


        Task<AnamnesisMantDTO> ObtenerAnamnesisPorIdEstudianteAsync(int idEstudiante);
        Task<List<EstudianteMantDTO>> ObtenerEstudiantesConAnamnesisAsync();
        Task<AnamnesisRegistroDTO> ObtenerAnamnesisRegistroPorIdEstudianteAsync(int idEstudiante);
        Task<AnamnesisAdjuntoDownloadDTO> ObtenerAdjuntoPorIdAsync(int id);
        Task EliminarAdjuntoAsync(int id);
    }
}

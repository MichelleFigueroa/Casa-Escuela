using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.EstudianteDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasaEscuela.BL.Interfaces
{
    public interface IAnamnesisBL
    {
        Task<int> CrearAnamnesisAsync(EstudianteMantDTO estudiante, List<EstudianteFamiliarMantDTO> familiares, AnamnesisMantDTO anamnesis);
        Task<AnamnesisMantDTO> ObtenerAnamnesisPorIdEstudianteAsync(int idEstudiante);
        Task<List<EstudianteMantDTO>> ObtenerEstudiantesConAnamnesisAsync();
        Task<AnamnesisRegistroDTO> ObtenerAnamnesisRegistroPorIdEstudianteAsync(int idEstudiante);
    }
}

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
        // Method to get the full student data for the Anamnesis view if needed, 
        // but typically ObtenerEstudiante is in IEstudianteBL? 
        // Given the requirement "Consulta del expediente completo", maybe:
        // Task<ExpedienteDTO> ObtenerExpedienteAsync(int idEstudiante); 
        // But for now let's stick to Anamnesis specifics and general student creation.
    }
}

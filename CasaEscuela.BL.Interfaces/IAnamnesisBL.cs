using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.EstudianteDTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasaEscuela.BL.Interfaces
{
    public interface IAnamnesisBL
    {
        Task<int> CrearAnamnesisAsync(EstudianteMantDTO pEstudiante, List<EstudianteFamiliarMantDTO> pFamiliares, AnamnesisMantDTO pAnamnesis);
        Task<AnamnesisMantDTO> ObtenerAnamnesisPorIdEstudianteAsync(int idEstudiante);
        Task<List<EstudianteMantDTO>> ObtenerEstudiantesConAnamnesisAsync();
        Task<AnamnesisRegistroDTO> ObtenerAnamnesisRegistroPorIdEstudianteAsync(int idEstudiante);
        
        // Attachments
        Task<AnamnesisAdjuntoDownloadDTO> ObtenerAdjuntoPorIdAsync(int id);
        Task EliminarAdjuntoAsync(int id);

        // Dashboard
        Task<DashboardDTO> ObtenerDatosDashboardAsync();
    }
}

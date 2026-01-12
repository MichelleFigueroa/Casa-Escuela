using CasaEscuela.BL.DTOs.EstudianteDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.BL.DTOs
{
    public class AnamnesisRegistroDTO
    {
        public EstudianteMantDTO Estudiante { get; set; }
        public AnamnesisMantDTO Anamnesis { get; set; }
        public List<EstudianteFamiliarMantDTO> Familiares { get; set; } = new List<EstudianteFamiliarMantDTO>();
        public List<TimelineItemDTO> Timeline { get; set; } = new List<TimelineItemDTO>();
    }
}

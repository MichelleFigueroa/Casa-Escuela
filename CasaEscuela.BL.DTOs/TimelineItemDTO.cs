using System;

namespace CasaEscuela.BL.DTOs
{
    public class TimelineItemDTO
    {
        public DateTime Fecha { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; } // Bootstrap/Tabler icon class
        public string Color { get; set; } // Bootstrap color class (e.g., "primary", "success")
        public string Tipo { get; set; } // "Inicio", "Preceptoria", "Anamnesis", etc.
        public int? EntidadId { get; set; } // ID to link to details if needed
    }
}

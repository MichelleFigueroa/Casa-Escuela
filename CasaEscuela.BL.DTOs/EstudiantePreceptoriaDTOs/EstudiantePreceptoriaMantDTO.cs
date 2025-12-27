using System;

namespace CasaEscuela.BL.DTOs.EstudiantePreceptoriaDTOs
{
    public class EstudiantePreceptoriaMantDTO
    {
        public int Id { get; set; }

        public DateTime fecha { get; set; }

        public string procesos_trabajados { get; set; }

        public string procesos_dificultad { get; set; }

        public string metas_siguientes { get; set; }

        public string recomendaciones { get; set; }

        public DateTime fechaCreacion { get; set; }

        public DateTime fechaActualizacion { get; set; }
    }
}
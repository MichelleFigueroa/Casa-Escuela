using System;

namespace CasaEscuela.BL.DTOs.EntrevistaDTOs
{
    public class EntrevistaBuscarDTO
    {
        public int? Id { get; set; }
        public DateTime? fecha_entrevista { get; set; }
        public int? IdEstudiante { get; set; }
        public int? IdFamilia { get; set; }

        public int Pagina { get; set; }
        public int RegistrosPorPagina { get; set; }
    }
}
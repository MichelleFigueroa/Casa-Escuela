using System;

namespace CasaEscuela.BL.DTOs.EstudiantePreceptoriaDTOs
{
    public class EstudiantePreceptoriaBuscarDTO
    {
        public int? Id { get; set; }
        public DateTime? fecha { get; set; }

        public int Pagina { get; set; }
        public int RegistrosPorPagina { get; set; }
    }
}
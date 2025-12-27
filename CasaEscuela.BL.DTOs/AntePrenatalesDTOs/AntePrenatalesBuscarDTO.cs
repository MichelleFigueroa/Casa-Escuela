using System;

namespace CasaEscuela.BL.DTOs.AntePrenatalesDTOs
{
    public class AntePrenatalesBuscarDTO
    {
        public int? Id { get; set; }
        public string medicamentos { get; set; }
        public string emfermedades { get; set; }

        // Propiedades para la paginación
        public int Pagina { get; set; }
        public int RegistrosPorPagina { get; set; }
    }
}
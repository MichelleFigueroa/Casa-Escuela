using System;

namespace CasaEscuela.BL.DTOs.EstudianteDTOs
{
    public class EstudianteMantDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public int edad { get; set; }
        public string centro_escolar { get; set; }
        public int grado { get; set; }
        public bool estado { get; set; }
    }
}
using CasaEscuela.EN.CatologosEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.EN
{
    public class EstudianteEN
    {
        public int IdEstudiante { get; set; }
        public string? Codigo { get; set; }

        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime? FechaNacimiento { get; set; }

        public SexoEnum Sexo { get; set; }

        public int? NivelEscolarId { get; set; }
        public string? Grado { get; set; }
        public string? Seccion { get; set; }
        public string? Jornada { get; set; }

        public bool Estado { get; set; }
        public DateTime FechaRegistro { get; set; }

        // Navegación
        public CatNivelEscolarEN NivelEscolar { get; set; }
        public AnamnesisEN Anamnesis { get; set; }
        public List<EstudianteFamiliarEN> Familiares { get; set; }
        public List<EstudiantePreceptoriaEN> Preceptorias { get; set; }
    }
}

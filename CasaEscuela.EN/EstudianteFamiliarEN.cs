using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.EN
{
    public class EstudianteFamiliarEN
    {
        public int IdFamiliar { get; set; }
        public int IdEstudiante { get; set; }

        public TipoParentescoEnum TipoParentesco { get; set; }

        public string Nombres { get; set; }
        public string Apellidos { get; set; }

        public int? Edad { get; set; }
        public string Escolaridad { get; set; }
        public string Ocupacion { get; set; }

        public bool ViveConEstudiante { get; set; }
        public string Telefono { get; set; }

        // Navegación
        public EstudianteEN Estudiante { get; set; }
    }
}

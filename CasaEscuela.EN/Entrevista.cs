using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.EN
{
    public class Entrevista
    {
        public int Id { get; set; }

        public string obbservaciones { get; set; }

        public DateTime fecha_entrevista { get; set; }

        public string evaluciones_previa { get; set; }

        public string informacion_adicional { get; set; }
        
        public int IdEstudiante { get; set; }

        public int IdFamilia { get; set; }

        public int IdParto { get; set; }

        public int IdSalud {  get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.BL.DTOs
{
    public class DashboardDTO
    {
        public int TotalEstudiantes { get; set; }
        public int ExpedientesActivos { get; set; }
        public int TotalAnamnesis { get; set; }
        public int TotalPreceptorias { get; set; }
        public int TotalAnalisis { get; set; } // Placeholder if not distinct from Anamnesis
        public int TotalDocumentos { get; set; }
        public int TotalPostClase { get; set; } // Placeholder
        public int UsuariosActivos { get; set; }
    }
}

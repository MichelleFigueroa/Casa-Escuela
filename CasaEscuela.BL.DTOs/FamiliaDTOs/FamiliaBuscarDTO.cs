using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.BL.DTOs.FamiliaDTOs
{
    public class FamiliaBuscarDTO
    {
        public int? Id { get; set; }
        public string Nombre_Madre { get; set; }
        public string Nombre_Padre { get; set; }
        public int? Telefono { get; set; }

        public int Pagina { get; set; }
        public int RegistrosPorPagina { get; set; }
    }
}
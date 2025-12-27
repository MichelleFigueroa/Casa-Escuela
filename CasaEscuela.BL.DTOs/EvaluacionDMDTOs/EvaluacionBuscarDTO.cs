using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.BL.DTOs.EvaluacionDMDTOs
{
    public class EvaluacionDMBuscarDTO
    {
        public int? Id { get; set; }
        public int? IdDesarrolloMotor { get; set; }

        public int Pagina { get; set; }
        public int RegistrosPorPagina { get; set; }
    }
}

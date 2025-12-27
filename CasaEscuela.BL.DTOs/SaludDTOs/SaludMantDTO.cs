using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.BL.DTOs.SaludDTOs
{
    public class SaludMantDTO
    {
        public int Id { get; set; }
        public string descripcion { get; set; }
        public int IdEntrevista { get; set; }
    }
}
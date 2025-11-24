using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.BL.DTOs
{
    public class MensajeErrorDTO
    {
        public string Mensaje { get; set; }
        public byte Estatus { get; set; }
    }
    public enum Estatus_MensajeError
    {
        FALLO=1,
        SINFALLO=0
    }
}

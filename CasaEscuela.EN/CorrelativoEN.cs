using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.EN
{
    public class CorrelativoEN
    {
        public byte Id { get; set; }
        public byte Tipo { get; set; }
        public int Valor { get; set; }
        public string? AliasInicio { get; set; }
        public DateTime? UltFechaActualizacion { get; set; }
        public string? AliasFin { get; set; }
        public int? IdSucursal { get; set; }
    }
}

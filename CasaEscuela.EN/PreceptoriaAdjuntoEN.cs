using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.EN
{
    public class PreceptoriaAdjuntoEN
    {
        public int Id { get; set; }

        public int PreceptoriaId { get; set; }

        [Required]
        [StringLength(255)]
        public string NombreArchivo { get; set; }

        [Required]
        public byte[] Contenido { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }

        public DateTime FechaCreacion { get; set; }

        [ForeignKey("PreceptoriaId")]
        public EstudiantePreceptoriaEN Preceptoria { get; set; }
    }
}

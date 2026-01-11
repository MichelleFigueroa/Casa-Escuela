using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.EN
{
    public class AnamnesisAdjuntoEN
    {
        public int Id { get; set; }

        public int AnamnesisId { get; set; }

        [Required]
        [StringLength(255)]
        public string NombreArchivo { get; set; }

        [Required]
        public byte[] Contenido { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }

        public DateTime FechaCreacion { get; set; }

        [ForeignKey("AnamnesisId")]
        public AnamnesisEN Anamnesis { get; set; }
    }
}

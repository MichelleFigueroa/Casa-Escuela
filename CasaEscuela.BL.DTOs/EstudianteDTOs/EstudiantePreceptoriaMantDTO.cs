using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.BL.DTOs.EstudianteDTOs
{
    public class EstudiantePreceptoriaMantDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El estudiante es obligatorio")]
        public int IdEstudiante { get; set; }

        // ====== TIPO DE PRECEPTORÍA ======

        [Display(Name = "Tipo de preceptoría")]
        public int? TipoPreceptoriaId { get; set; }

        // ====== FECHA ======

        [Required(ErrorMessage = "La fecha es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de la preceptoría")]
        public DateTime Fecha { get; set; }

        // ====== CONTENIDO ======

        [StringLength(2000)]
        [Display(Name = "Procesos trabajados")]
        public string ProcesosTrabajados { get; set; }

        [StringLength(2000)]
        [Display(Name = "Procesos con dificultad")]
        public string ProcesosDificultad { get; set; }

        [StringLength(2000)]
        [Display(Name = "Metas siguientes")]
        public string MetasSiguientes { get; set; }

        [StringLength(2000)]
        [Display(Name = "Recomendaciones")]
        public string Recomendaciones { get; set; }

        // ====== ESTADO ======

        [Required(ErrorMessage = "El estado es obligatorio")]
        [Display(Name = "Estado")]
        public byte EstadoPreceptoria { get; set; }   

        // ====== AUDITORÍA ======

        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        // ====== ADJUNTOS ======
        public List<CasaEscuela.BL.DTOs.PreceptoriaDTOs.PreceptoriaAdjuntoDTO> AdjuntosExistentes { get; set; } = new List<CasaEscuela.BL.DTOs.PreceptoriaDTOs.PreceptoriaAdjuntoDTO>();
        
        
        public List<Microsoft.AspNetCore.Http.IFormFile> ArchivosSubidos { get; set; }

    }
}

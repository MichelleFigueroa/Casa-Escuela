using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.BL.DTOs
{
    public class AnamnesisMantDTO
    {
        public int IdAnamnesis { get; set; }

        [Required(ErrorMessage = "El estudiante es obligatorio")]
        public int IdEstudiante { get; set; }

        // ====== DATOS FAMILIARES ======

        [Display(Name = "Vive con")]
        public int? ViveConId { get; set; }

        [Display(Name = "Tipo de familia")]
        public int? TipoFamiliaId { get; set; }

        // ====== DATOS DE NACIMIENTO ======

        [Display(Name = "Tipo de parto")]
        public int? TipoPartoId { get; set; }

        [Display(Name = "Embarazo controlado")]
        public bool EmbarazoControlado { get; set; }

        [StringLength(500)]
        [Display(Name = "Complicaciones durante el embarazo")]
        public string? ComplicacionesEmbarazo { get; set; }

        // ====== SALUD Y DESARROLLO ======

        [StringLength(1000)]
        [Display(Name = "Condiciones de salud")]
        public string? CondicionesSalud { get; set; }

        [StringLength(1000)]
        [Display(Name = "Desarrollo del lenguaje")]
        public string? DesarrolloLenguaje { get; set; }

        [StringLength(1000)]
        [Display(Name = "Desarrollo motor")]
        public string? DesarrolloMotor { get; set; }

        // ====== CONTEXTO FAMILIAR ======

        [StringLength(1000)]
        [Display(Name = "Situación familiar")]
        public string? SituacionFamiliar { get; set; }

        [StringLength(1000)]
        [Display(Name = "Observaciones")]
        public string? Observaciones { get; set; }

        // ====== DATOS DE LA ENTREVISTA ======

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de la entrevista")]
        public DateTime? FechaEntrevista { get; set; }

        [StringLength(100)]
        [Display(Name = "Entrevistador")]
        public string? Entrevistador { get; set; }

        // ====== ADJUNTOS ======
        public List<AnamnesisAdjuntoDTO> AdjuntosExistentes { get; set; } = new List<AnamnesisAdjuntoDTO>();

        public List<Microsoft.AspNetCore.Http.IFormFile> ArchivosSubidos { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.BL.DTOs.EstudianteDTOs
{
    public class EstudianteFamiliarMantDTO
    {
        public int IdFamiliar { get; set; }

        [Required(ErrorMessage = "El estudiante es obligatorio")]
        public int IdEstudiante { get; set; }

        // ====== PARENTESCO ======

        [Required(ErrorMessage = "El parentesco es obligatorio")]
        [Display(Name = "Parentesco")]
        public byte TipoParentesco { get; set; }   

        // ====== DATOS PERSONALES ======

        [Required(ErrorMessage = "Los nombres son obligatorios")]
        [StringLength(100)]
        [Display(Name = "Nombres")]
        public string Nombres { get; set; }

        [StringLength(100)]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; }

        [Range(0, 120, ErrorMessage = "Edad no válida")]
        [Display(Name = "Edad")]
        public int? Edad { get; set; }

        [StringLength(100)]
        [Display(Name = "Escolaridad")]
        public string Escolaridad { get; set; }

        [StringLength(100)]
        [Display(Name = "Ocupación")]
        public string Ocupacion { get; set; }

        // ====== CONVIVENCIA ======

        [Display(Name = "Vive con el estudiante")]
        public bool ViveConEstudiante { get; set; }

        [Phone]
        [StringLength(20)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
    }
}

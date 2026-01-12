using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.BL.DTOs.EstudianteDTOs
{
    public class EstudianteMantDTO
    {
        public int IdEstudiante { get; set; }

        [StringLength(20)]
        [Display(Name = "Código del estudiante")]
        public string? Codigo { get; set; }

        [Required(ErrorMessage = "Los nombres son obligatorios")]
        [StringLength(100)]
        [Display(Name = "Nombres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios")]
        [StringLength(100)]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El sexo es obligatorio")]
        [Display(Name = "Sexo")]
        public  byte Sexo { get; set; }

        // ====== DATOS ACADÉMICOS ======

        [Display(Name = "Nivel escolar")]
        public int? NivelEscolarId { get; set; }

        [StringLength(50)]
        [Display(Name = "Grado")]
        public string? Grado { get; set; }

        [StringLength(10)]
        [Display(Name = "Sección")]
        public string? Seccion { get; set; }

        [StringLength(50)]
        [Display(Name = "Jornada")]
        public string? Jornada { get; set; }

        // ====== ESTADO ======

        [Display(Name = "Activo")]
        public bool Estado { get; set; } = true;

        [Display(Name = "Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }
    }
}

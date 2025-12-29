using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.BL.DTOs.CatalogoDTO
{
    public class CatViveConMantDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [Display(Name = "Vive con")]
        public string Descripcion { get; set; }

        [Display(Name = "Activo")]
        public bool Estado { get; set; } = true;
    }
}

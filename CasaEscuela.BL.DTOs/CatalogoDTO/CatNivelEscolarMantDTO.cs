using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.BL.DTOs.CatalogoDTO
{
    public class CatNivelEscolarMantDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nivel escolar es obligatorio")]
        [StringLength(100)]
        [Display(Name = "Nivel escolar")]
        public string Descripcion { get; set; }
    }
}

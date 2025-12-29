using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.BL.DTOs.CatalogoDTO
{
    public class CatTipoFamiliaMantDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El tipo de familia es obligatorio")]
        [StringLength(100)]
        [Display(Name = "Tipo de familia")]
        public string Descripcion { get; set; }
    }
}

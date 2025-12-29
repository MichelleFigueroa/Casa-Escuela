using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.BL.DTOs.PreceptoriaDTOs
{
    public class CatTipoPreceptoriaMantDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El tipo de preceptoría es obligatorio")]
        [StringLength(100)]
        [Display(Name = "Tipo de preceptoría")]
        public string Descripcion { get; set; }
    }
}

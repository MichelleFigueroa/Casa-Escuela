using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.BL.DTOs.CatalogoDTO
{
    public class CatTipoPartoMantDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El tipo de parto es obligatorio")]
        [StringLength(50)]
        [Display(Name = "Tipo de parto")]
        public string Descripcion { get; set; }
    }
}

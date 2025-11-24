using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.BL.DTOs.PermisosDTOs
{
    public class PermisoBuscarDTO : PaginacionInputDTO
    {
        public int? IdUsuario { get; set; }       
        public string Vista_like { get; set; }   
    }

}

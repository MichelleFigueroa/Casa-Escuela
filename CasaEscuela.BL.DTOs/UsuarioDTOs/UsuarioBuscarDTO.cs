using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CasaEscuela.BL.DTOs.UsuarioDTOs
{
    public class UsuarioBuscarDTO : PaginacionInputDTO
    {
        public string DUI_like { get; set; }
        public string Nombre_like { get; set; }       
        public string Apellido_like { get; set; }       
        [Display(Name = "Rol")]
        public int IdRol_equal { get; set; }       
        public string Email_equal { get; set; }
        public byte Estado_equal { get; set; }
    }
}

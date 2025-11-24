using CasaEscuela.BL.DTOs.PermisoDTOs;

 
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace CasaEscuela.BL.DTOs.UsuarioDTOs
{
    public class UsuarioMantDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "DUI es obligatorio")]
        [Display(Name = "DUI")]
        [StringLength(10, MinimumLength = 9, ErrorMessage = "El DUI esta incompleto")]
        public string DUI { get; set; }
        [Required(ErrorMessage = "Nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Apellido es obligatorio")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "Rol es obligatorio")]
        [Display(Name = "Rol")]
        [DefaultValue("0")]
        public int IdRol { get; set; }
        [Required(ErrorMessage = "Email es obligatorio")]
        [EmailAddress(ErrorMessage ="Formato de correo invalido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password es obligatorio")]
        [DataType(DataType.Password)]
        [StringLength(80, ErrorMessage = "Password debe estar entre 5 a 32 caracteres", MinimumLength = 5)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirmar el password")]
        [StringLength(80, ErrorMessage = "Password debe estar entre 5 a 32 caracteres", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password y confirmar password deben de ser iguales")]
        [Display(Name = "Confirmar password")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }
        [Display(Name = "Fecha de Validez")]
        [Required(ErrorMessage = "Fecha de Validez es obligatorio")]
        public DateTime FechaValidez { get; set; }
        [Display(Name = "Cargo Directiva")]
        public string? CargoDirectiva { get; set; }
        public String Token { get; set; }

        public int? IdSucursal { get; set; }
        public byte Estado { get; set; }
        public List<string> Permisos { get; set; } = new();
        public List<int> SucursalesSeleccionadas { get; set; } = new();
        public List<PermisoDTO> Permisoss { get; set; } = new List<PermisoDTO>();


    }
}

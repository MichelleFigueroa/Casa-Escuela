using System.ComponentModel.DataAnnotations;
namespace CasaEscuela.BL.DTOs.UsuarioDTOs
{
    public class UsuarioLoginDTO
    {
        [Required(ErrorMessage = "Email es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo invalido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password es obligatorio")]
        [DataType(DataType.Password)]
        [StringLength(80, ErrorMessage = "Password debe estar entre 5 a 32 caracteres", MinimumLength = 5)]
        public string Password { get; set; }

        public byte Estado { get; set; }
    }
}

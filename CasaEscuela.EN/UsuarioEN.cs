using CasaEscuela.EN.PermisosENs;
using CasaEscuela.EN.Seguridad;
using System.ComponentModel.DataAnnotations.Schema;

namespace CasaEscuela.EN
{
    public class UsuarioEN
    {
        public int Id { get; set; }
        public string DUI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime FechRegistro { get; set; }
        public DateTime FechaValidez { get; set; }
        public byte Estado { get; set; }

        public List<UsuarioPermiso> UsuarioPermisos { get; set; }

    }
}

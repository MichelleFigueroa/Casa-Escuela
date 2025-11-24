using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.EN.Seguridad
{
    public class Permiso
    {
        public int Id { get; set; }
        public string Nombre { get; set; } 
        public string Modulo { get; set; }   
        public ICollection<UsuarioPermiso> UsuarioPermisos { get; set; }
    }
}

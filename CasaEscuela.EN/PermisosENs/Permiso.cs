using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.EN.PermisosENs
{
    public class Permiso
    {

        public int Id { get; set; }

        [Column("IdUsuario")]
        public int IdUsuario { get; set; }
        [Column("NombreVista")]
        public string Vista { get; set; }
        public bool PuedeVer { get; set; }
        public bool PuedeCrear { get; set; }
        public bool PuedeEditar { get; set; }
        public bool PuedeEliminar { get; set; }

        public Usuario Usuario { get; set; }
    }
}

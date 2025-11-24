using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.BL.DTOs.PermisoDTOs
{
    public class PermisoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Modulo { get; set; }
        public string NombreAmigable { get; set; }
        public bool Asignado { get; set; } 
    }
}

 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.BL.DTOs.UsuarioDTOs
{
    public class UsuarioSucursalDTO
    {
        public long Id { get; set; }

        public long IdUsuario { get; set; }
        public long IdSucursal { get; set; }

        public UsuarioMantDTO Usuario { get; set; }
    }
}

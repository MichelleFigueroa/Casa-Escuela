using CasaEscuela.BL.DTOs.PermisosDTOs;
using CasaEscuela.BL.DTOs.UsuarioDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasaEscuela.BL.Interfaces
{
    public interface IPermisoBL
    {
        Task<List<PermisoDTO>> ObtenerPorUsuarioAsync(int idUsuario);
        Task<List<PermisoDTO>> ObtenerTodosAsync();
        Task<List<UsuarioMantDTO>> ObtenerUsuariosPorRolAsync(string rol);
        Task<int> CrearAsync(List<PermisoDTO> permisos, List<int> empleadosSeleccionados);
        Task<int> ModificarAsync(PermisoDTO permiso);
        Task<int> EliminarAsync(PermisoDTO permiso);
    }
}

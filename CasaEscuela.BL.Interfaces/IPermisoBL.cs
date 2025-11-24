using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.PermisosDTOs;
using CasaEscuela.BL.DTOs.UsuarioDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.BL.Interfaces
{
    public interface IPermisoBL
    {
        Task<int> CrearAsync(List<PermisoDTO> permisos, List<int> empleadosIds);
        Task<int> ModificarAsync(PermisoDTO pPermiso);
        Task<int> EliminarAsync(PermisoDTO pPermiso);
        Task<PermisoDTO> ObtenerPorIdAsync(PermisoDTO pPermiso);
        Task<PaginacionOutputDTO<List<PermisoDTO>>> BuscarAsync(PermisoDTO pPermiso);
        Task<List<PermisoDTO>> ObtenerPorRolAsync(int idRol);
        Task<List<PermisoDTO>> ObtenerTodosAsync();
        Task<List<PermisoDTO>> ObtenerPorUsuarioAsync(int idUsuario);
        Task<List<UsuarioMantDTO>> ObtenerUsuariosPorRolAsync(string nombreRol);
        Task<bool> TienePermisoAsync(int idUsuario, string nombreVista, string accion);

    }
}

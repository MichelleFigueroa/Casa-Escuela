using CasaEscuela.BL.DTOs.PermisosDTOs;
using CasaEscuela.BL.DTOs.UsuarioDTOs;
using CasaEscuela.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasaEscuela.DAL
{
    public class PermisoDAL : IPermisoBL
    {
        public Task<List<PermisoDTO>> ObtenerPorUsuarioAsync(int idUsuario) => Task.FromResult(new List<PermisoDTO>());
        public Task<List<PermisoDTO>> ObtenerTodosAsync() => Task.FromResult(new List<PermisoDTO>());
        public Task<List<UsuarioMantDTO>> ObtenerUsuariosPorRolAsync(string rol) => Task.FromResult(new List<UsuarioMantDTO>());
        public Task<int> CrearAsync(List<PermisoDTO> permisos, List<int> empleadosSeleccionados) => Task.FromResult(0);
        public Task<int> ModificarAsync(PermisoDTO permiso) => Task.FromResult(0);
        public Task<int> EliminarAsync(PermisoDTO permiso) => Task.FromResult(0);
    }
}

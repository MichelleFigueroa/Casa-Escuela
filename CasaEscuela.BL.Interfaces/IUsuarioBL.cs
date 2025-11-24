using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.PermisoDTOs;
using CasaEscuela.BL.DTOs.UsuarioDTOs;

namespace CasaEscuela.BL.Interfaces
{
    public interface IUsuarioBL
    {
        public Task<int> CrearAsync(UsuarioMantDTO pUsuario);
        public Task<int> ModificarAsync(UsuarioMantDTO pUsuario);
        public Task<int> EliminarAsync(UsuarioMantDTO pUsuario);
        public Task<UsuarioMantDTO> ObtenerPorIdAsync(UsuarioMantDTO pUsuario);
        public Task<PaginacionOutputDTO<List<UsuarioMantDTO>>> BuscarAsync(UsuarioBuscarDTO pUsuario);

        public Task<UsuarioMantDTO> LoginAsync(UsuarioLoginDTO pUsuario);
        public Task<int> CambiarPasswordAsync(UsuarioCambiarPasswordDTO pUsuario);
        public Task<UsuarioMantDTO> ObtenerPorDUIAsync(UsuarioMantDTO pUsuario);
        public Task<List<PermisoDTO>> ObtenerTodosPermisosAsync();
        public Task<List<int>> ObtenerPermisosUsuarioAsync(int idUsuario);



    }
}

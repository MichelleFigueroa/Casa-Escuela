using CasaEscuela.BL.Interfaces;
using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.UsuarioDTOs;
using CasaEscuela.EN;
using CasaEscuela.BL.DTOs.PermisoDTOs;

namespace CasaEscuela.BL
{
    public class UsuarioBL : IUsuarioBL
    {
        public async Task<int> CrearAsync(UsuarioMantDTO pUsuario)
        {
            return 1;
        }

        public async Task<int> ModificarAsync(UsuarioMantDTO pUsuario)
        {
            return 1;
        }

        public async Task<int> EliminarAsync(UsuarioMantDTO pUsuario)
        {
            return 1;
        }

        public async Task<UsuarioMantDTO> ObtenerPorIdAsync(UsuarioMantDTO pUsuario)
        {
            return pUsuario;
        }

        public async Task<PaginacionOutputDTO<List<UsuarioMantDTO>>> BuscarAsync(UsuarioBuscarDTO pUsuario)
        {
            return new PaginacionOutputDTO<List<UsuarioMantDTO>>();
        }

        // --- MÉTODOS FALTANTES PARA QUITAR LOS ERRORES ---

        public async Task<UsuarioMantDTO> LoginAsync(UsuarioLoginDTO pUsuario)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CambiarPasswordAsync(UsuarioCambiarPasswordDTO pUsuario)
        {
            throw new NotImplementedException();
        }

        public async Task<UsuarioMantDTO> ObtenerPorDUIAsync(UsuarioMantDTO pUsuario)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> ObtenerTodosPermisosAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> ObtenerPermisosUsuarioAsync(int pIdUsuario)
        {
            throw new NotImplementedException();
        }

        Task<List<PermisoDTO>> IUsuarioBL.ObtenerTodosPermisosAsync()
        {
            throw new NotImplementedException();
        }

        Task<List<int>> IUsuarioBL.ObtenerPermisosUsuarioAsync(int idUsuario)
        {
            throw new NotImplementedException();
        }
    }
}
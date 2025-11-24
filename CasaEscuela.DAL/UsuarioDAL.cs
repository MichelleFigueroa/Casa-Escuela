using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.PermisoDTOs;
using CasaEscuela.BL.DTOs.UsuarioDTOs;
using CasaEscuela.BL.Interfaces;
using CasaEscuela.EN;
using CasaEscuela.EN.Seguridad;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CasaEscuela.DAL
{
    public class UsuarioDAL : IUsuarioBL
    {
        readonly CasaEscuelaDBContext dbContext;
        public UsuarioDAL(CasaEscuelaDBContext context) => dbContext = context;
		internal IQueryable<Usuario> QuerySelect(IQueryable<Usuario> pQuery, UsuarioBuscarDTO pUsuario)
		{
			if (!string.IsNullOrWhiteSpace(pUsuario.Nombre_like))
				pQuery = pQuery.Where(s => s.Nombre.Contains(pUsuario.Nombre_like));
			if (!string.IsNullOrWhiteSpace(pUsuario.Apellido_like))
				pQuery = pQuery.Where(s => s.Apellido.Contains(pUsuario.Apellido_like));
			if (!string.IsNullOrWhiteSpace(pUsuario.DUI_like))
				pQuery = pQuery.Where(s => s.DUI.Contains(pUsuario.DUI_like));
			if (!string.IsNullOrWhiteSpace(pUsuario.Email_equal))
				pQuery = pQuery.Where(s => s.Email.Contains(pUsuario.Email_equal));
			pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
			if (pUsuario.Take > 0)
				pQuery = pQuery.Skip(pUsuario.Skip).Take(pUsuario.Take).AsQueryable();
			return pQuery;
		}

        public async Task<PaginacionOutputDTO<List<UsuarioMantDTO>>> BuscarAsync(UsuarioBuscarDTO pUsuario)
        {
            var result = new PaginacionOutputDTO<List<UsuarioMantDTO>>();
            result.Data = new List<UsuarioMantDTO>();

            var select = dbContext.Usuarios
                .AsQueryable();

            select = QuerySelect(select, pUsuario);

            var usuarios = await select.ToListAsync();

            if (usuarios.Count > 0)
            {
                if (pUsuario.IsCount)
                {
                    pUsuario.Take = 0;
                    var selectCount = dbContext.Usuarios.AsQueryable();
                    result.Count = await QuerySelect(selectCount, pUsuario).CountAsync();
                }

                usuarios.ForEach(s => result.Data.Add(new UsuarioMantDTO
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Apellido = s.Apellido,
                    DUI = s.DUI,
                    Email = s.Email,
                    FechaRegistro = s.FechRegistro,
                    FechaValidez = s.FechaValidez
                }));
            }

            return result;
        }


        public async Task<int> CambiarPasswordAsync(UsuarioCambiarPasswordDTO pUsuario)
        {
			var usuario = await dbContext.Usuarios.Where(s => s.Id == pUsuario.Id && s.Password== pUsuario.PasswordActual).FirstOrDefaultAsync();
			if (usuario != null && usuario.Id != 0)
			{
				usuario.Password = pUsuario.Password;				
				dbContext.Update(usuario);
				return await dbContext.SaveChangesAsync();
			}
			else
				return 0;
		}

        public async Task<int> CrearAsync(UsuarioMantDTO pUsuario)
        {
            Usuario usuario = new Usuario()
            {
                Nombre = pUsuario.Nombre,
                Apellido = pUsuario.Apellido,
                Estado = 1,
                DUI = pUsuario.DUI,
                Email = pUsuario.Email,
                FechaValidez = DateTime.Now.AddYears(5),
                FechRegistro = DateTime.Now,
                Password = pUsuario.Password,
            };
            dbContext.Usuarios.Add(usuario);
            await dbContext.SaveChangesAsync();

            await CrudPermisoUsuarioAsync(usuario.Id, pUsuario.Permisos);
            return usuario.Id;

        }

        public async Task<int> EliminarAsync(UsuarioMantDTO pUsuario)
        {
            var usuario = await dbContext.Usuarios.
            Where(s => s.Id == pUsuario.Id).FirstOrDefaultAsync();
            if (usuario != null && usuario.Id != 0)
			{
				dbContext.Usuarios.Remove(usuario);
				return await dbContext.SaveChangesAsync();
			}
			else
				return 0;
		}

        public async Task<UsuarioMantDTO> LoginAsync(UsuarioLoginDTO pUsuario)
        {
            var find = await dbContext.Usuarios
                .FirstOrDefaultAsync(s => s.Email == pUsuario.Email && s.Password == pUsuario.Password);

            if (find != null && find.Estado == 1)
            {
                var permisos = await dbContext.UsuarioPermisos
                    .Where(up => up.IdUsuario == find.Id)
                    .Select(up => up.Permiso.Nombre) 
                    .ToListAsync();

                return new UsuarioMantDTO
                {
                    Id = find.Id,
                    Nombre = find.Nombre,
                    Apellido = find.Apellido,
                    Email = find.Email,
                    DUI = find.DUI,
                    FechaRegistro = find.FechRegistro,
                    FechaValidez = find.FechaValidez,
                    Estado = find.Estado,
                    Permisos = permisos   
                };
            }
            else
            {
                return new UsuarioMantDTO();
            }
        }

   
        public async Task<int> ModificarAsync(UsuarioMantDTO pUsuario)
        {
			var usuario = await dbContext.Usuarios.
                Where(s => s.Id == pUsuario.Id).FirstOrDefaultAsync();
			if (usuario != null && usuario.Id != 0)
			{				
                usuario.Nombre = pUsuario.Nombre;
                usuario.Apellido = pUsuario.Apellido;
                usuario.Estado = pUsuario.Estado;
                usuario.DUI = pUsuario.DUI;
                usuario.Email = pUsuario.Email;                        
				dbContext.Update(usuario);
                dbContext.Update(usuario);
                await dbContext.SaveChangesAsync();

                await CrudPermisoUsuarioAsync(usuario.Id, pUsuario.Permisos);

                return usuario.Id;
            }
			else
				return 0;
		}

        public async Task<UsuarioMantDTO> ObtenerPorIdAsync(UsuarioMantDTO pUsuario)
        {
            var find = await dbContext.Usuarios
                .FirstOrDefaultAsync(s => s.Id == pUsuario.Id);

            if (find != null && find.Id != 0)
            {
                return new UsuarioMantDTO
                {
                    Apellido = find.Apellido,
                    Email = find.Email,
                    DUI = find.DUI,
                    Estado = find.Estado,
                    FechaRegistro = find.FechRegistro,
                    FechaValidez = find.FechaValidez,
                    Id = find.Id,
                    Nombre = find.Nombre,
                    
                };
            }
            else
            {
                return new UsuarioMantDTO();
            }
        }



        public async Task<UsuarioMantDTO> ObtenerPorDUIAsync(UsuarioMantDTO pUsuario)
        {
            var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(s => s.DUI == pUsuario.DUI);

            if (usuario != null && usuario.DUI != null)
            {
                return new UsuarioMantDTO
                {
                    Id = usuario.Id,
                    DUI = usuario.DUI,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                };
            }
            else
                return new UsuarioMantDTO();
        }
     
        
        public async Task<List<int>> ObtenerPermisosUsuarioAsync(int idUsuario)
        {
            return await dbContext.UsuarioPermisos
                .Where(up => up.IdUsuario == idUsuario)
                .Select(up => up.IdPermiso)
                .ToListAsync();
        }

        public async Task<List<PermisoDTO>> ObtenerPermisosUsuarioConAsignacionAsync(int idUsuario)
        {
            var todosPermisos = await dbContext.Permisos
                .Select(p => new PermisoDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Modulo = p.Modulo,
                    Asignado = false
                })
                .ToListAsync();

            var permisosUsuario = await ObtenerPermisosUsuarioAsync(idUsuario);

            foreach (var permiso in todosPermisos)
            {
                if (permisosUsuario.Contains(permiso.Id))
                    permiso.Asignado = true;
            }

            return todosPermisos;
        }
        public async Task<List<PermisoDTO>> ObtenerTodosPermisosAsync()
        {
            return await dbContext.Permisos
                .Select(p => new PermisoDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    NombreAmigable = $"{p.Nombre} ({p.Modulo})",
                    Asignado = false,
                    Modulo = p.Modulo
                })
                .ToListAsync();
        }

        public async Task<int> CrudPermisoUsuarioAsync(int idUsuario, List<string> permisosSeleccionados)
        {
            var permisosActuales = dbContext.UsuarioPermisos.Where(up => up.IdUsuario == idUsuario);
            dbContext.UsuarioPermisos.RemoveRange(permisosActuales);

            if (permisosSeleccionados != null && permisosSeleccionados.Any())
            {
                foreach (var idPermiso in permisosSeleccionados)
                {
                    dbContext.UsuarioPermisos.Add(new UsuarioPermiso
                    {
                        IdUsuario = idUsuario,
                        IdPermiso = int.Parse(idPermiso)
                    });
                }
            }

            return await dbContext.SaveChangesAsync();
        }


      
    }
}

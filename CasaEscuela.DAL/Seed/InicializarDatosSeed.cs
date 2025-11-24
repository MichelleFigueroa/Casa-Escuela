using CasaEscuela.EN;
using CasaEscuela.EN.Seguridad;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CasaEscuela.DAL.Seed
{
    public static class InicializarDatosSeed
    {
        public static void Inicializar(CasaEscuelaDBContext dbContext)
        {
            #region Permisos
            var permisos = new[]
 {
    // === ESTUDIANTES ===
    new Permiso { Nombre = "MENU_ESTUDIANTES_GESTION", Modulo = "ESTUDIANTES" },
    new Permiso { Nombre = "ESTUDIANTES_CREAR", Modulo = "ESTUDIANTES" },
    new Permiso { Nombre = "ESTUDIANTES_EDITAR", Modulo = "ESTUDIANTES" },
    new Permiso { Nombre = "ESTUDIANTES_VER", Modulo = "ESTUDIANTES" },
    new Permiso { Nombre = "ESTUDIANTES_ELIMINAR", Modulo = "ESTUDIANTES" },

    // === EXPEDIENTES ===
    new Permiso { Nombre = "MENU_EXPEDIENTE_GENERAL", Modulo = "EXPEDIENTES" },
    new Permiso { Nombre = "MENU_EXPEDIENTE_DOCUMENTOS", Modulo = "EXPEDIENTES" },
    new Permiso { Nombre = "MENU_EXPEDIENTE_CRONOLOGIA", Modulo = "EXPEDIENTES" },
    new Permiso { Nombre = "EXPEDIENTE_VER", Modulo = "EXPEDIENTES" },
    new Permiso { Nombre = "EXPEDIENTE_EDITAR", Modulo = "EXPEDIENTES" },

    // === DOCUMENTOS ===
    new Permiso { Nombre = "DOCUMENTO_SUBIR", Modulo = "DOCUMENTOS" },
    new Permiso { Nombre = "DOCUMENTO_VER", Modulo = "DOCUMENTOS" },
    new Permiso { Nombre = "DOCUMENTO_DESCARGAR", Modulo = "DOCUMENTOS" },
    new Permiso { Nombre = "DOCUMENTO_ELIMINAR", Modulo = "DOCUMENTOS" },

    // === ANAMNESIS ===
    new Permiso { Nombre = "MENU_ANAMNESIS_REGISTRAR", Modulo = "ANAMNESIS" },
    new Permiso { Nombre = "MENU_ANAMNESIS_HISTORIAL", Modulo = "ANAMNESIS" },
    new Permiso { Nombre = "ANAMNESIS_CREAR", Modulo = "ANAMNESIS" },
    new Permiso { Nombre = "ANAMNESIS_EDITAR", Modulo = "ANAMNESIS" },
    new Permiso { Nombre = "ANAMNESIS_VER", Modulo = "ANAMNESIS" },
    new Permiso { Nombre = "ANAMNESIS_ELIMINAR", Modulo = "ANAMNESIS" },

    // === PRECEPTORÍAS ===
    new Permiso { Nombre = "MENU_PRECEPTORIA_REGISTRAR", Modulo = "PRECEPTORIA" },
    new Permiso { Nombre = "MENU_PRECEPTORIA_HISTORIAL", Modulo = "PRECEPTORIA" },
    new Permiso { Nombre = "PRECEPTORIA_CREAR", Modulo = "PRECEPTORIA" },
    new Permiso { Nombre = "PRECEPTORIA_EDITAR", Modulo = "PRECEPTORIA" },
    new Permiso { Nombre = "PRECEPTORIA_VER", Modulo = "PRECEPTORIA" },
    new Permiso { Nombre = "PRECEPTORIA_ELIMINAR", Modulo = "PRECEPTORIA" },

    // === ANÁLISIS PSICOLÓGICO ===
    new Permiso { Nombre = "MENU_PSICOLOGICO_NUEVO", Modulo = "PSICOLOGICO" },
    new Permiso { Nombre = "MENU_PSICOLOGICO_HISTORIAL", Modulo = "PSICOLOGICO" },
    new Permiso { Nombre = "PSICOLOGICO_CREAR", Modulo = "PSICOLOGICO" },
    new Permiso { Nombre = "PSICOLOGICO_EDITAR", Modulo = "PSICOLOGICO" },
    new Permiso { Nombre = "PSICOLOGICO_VER", Modulo = "PSICOLOGICO" },
    new Permiso { Nombre = "PSICOLOGICO_ELIMINAR", Modulo = "PSICOLOGICO" },

    // === POST CLASE ===
    new Permiso { Nombre = "MENU_POSTCLASE_NUEVO", Modulo = "POSTCLASE" },
    new Permiso { Nombre = "MENU_POSTCLASE_HISTORIAL", Modulo = "POSTCLASE" },
    new Permiso { Nombre = "POSTCLASE_CREAR", Modulo = "POSTCLASE" },
    new Permiso { Nombre = "POSTCLASE_EDITAR", Modulo = "POSTCLASE" },
    new Permiso { Nombre = "POSTCLASE_VER", Modulo = "POSTCLASE" },
    new Permiso { Nombre = "POSTCLASE_ELIMINAR", Modulo = "POSTCLASE" },

    // === USUARIOS / SEGURIDAD ===
    new Permiso { Nombre = "MENU_USUARIOS", Modulo = "SEGURIDAD" },
    new Permiso { Nombre = "USUARIOS_CREAR", Modulo = "SEGURIDAD" },
    new Permiso { Nombre = "USUARIOS_EDITAR", Modulo = "SEGURIDAD" },
    new Permiso { Nombre = "USUARIOS_VER", Modulo = "SEGURIDAD" },
    new Permiso { Nombre = "USUARIOS_ELIMINAR", Modulo = "SEGURIDAD" }
};


            foreach (var permiso in permisos)
            {
                if (!dbContext.Permisos.Any(p => p.Nombre == permiso.Nombre))
                    dbContext.Permisos.Add(permiso);
            }

            dbContext.SaveChanges();
            #endregion

            #region Crear usuario ROOT
            if (!dbContext.Usuarios.Any(u => u.Email == "root@admin.com"))
            {
                var root = new Usuario
                {
                    DUI = "000000000",
                    Nombre = "Super",
                    Apellido = "Administrador",
                    Email = "root@admin.com",
                    Password = "root123",   // <<< 
                    FechRegistro = DateTime.Now,
                    FechaValidez = DateTime.Now.AddYears(10),
                    Estado = 1
                };

                dbContext.Usuarios.Add(root);
                dbContext.SaveChanges();
            }
            #endregion

            #region Asignar todos los permisos al usuario ROOT
            var usuarioRoot = dbContext.Usuarios.FirstOrDefault(u => u.Email == "root@admin.com");
            var listaPermisos = dbContext.Permisos.ToList();

            foreach (var per in listaPermisos)
            {
                if (!dbContext.UsuarioPermisos.Any(up => up.IdUsuario == usuarioRoot.Id && up.IdPermiso == per.Id))
                {
                    dbContext.UsuarioPermisos.Add(new UsuarioPermiso
                    {
                        IdUsuario = usuarioRoot.Id,
                        IdPermiso = per.Id
                    });
                }
            }

            dbContext.SaveChanges();
            #endregion
        }

       
    }
}

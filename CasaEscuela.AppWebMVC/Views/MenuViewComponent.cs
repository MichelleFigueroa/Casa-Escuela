using CasaEscuela.AppWebMVC.Models.Menu;
using Microsoft.AspNetCore.Mvc;

namespace CasaEscuela.AppWebMVC.Views
{
    public class MenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var menu = InicializarMenu();
            var userClaims = HttpContext.User.Claims
                .Where(c => c.Type == "Permiso")
                .Select(c => c.Value)
                .ToList();

            foreach (var m in menu)
            {
                m.SubMenu = m.SubMenu
                    .Where(s => userClaims.Contains(s.Permiso))
                    .ToList();
            }

            menu = menu.Where(m => m.SubMenu.Any()).ToList();

            return View(menu);
        }

        private List<MenuDTO> InicializarMenu()
        {
            return new List<MenuDTO>
            {
                // ======================================================
                // === ESTUDIANTES
                // ======================================================
                new MenuDTO
                {
                    Nombre = "Estudiantes",
                    Icono = "fas fa-user-graduate",
                    SubMenu = new List<SubMenuDTO>
                    {
                        new SubMenuDTO
                        {
                            Nombre = "Gestión de Estudiantes",
                            Url = Url.Action("Index","Estudiante"),
                            Permiso = "MENU_ESTUDIANTES_GESTION",
                            Icono = "fas fa-users-line me-2"
                        }
                    }
                },

                // ======================================================
                // === EXPEDIENTES
                // ======================================================
                new MenuDTO
                {
                    Nombre = "Expedientes",
                    Icono = "fas fa-folder-open",
                    SubMenu = new List<SubMenuDTO>
                    {
                        new SubMenuDTO
                        {
                            Nombre = "Gestión de Expedientes",
                            Url = Url.Action("Index","Anamnesis"),
                            Permiso = "MENU_EXPEDIENTE_GENERAL",
                            Icono = "fas fa-file-medical me-2"
                        },
                        new SubMenuDTO
                        {
                            Nombre = "Documentos del Expediente",
                            Url = Url.Action("Index","Documento"),
                            Permiso = "MENU_EXPEDIENTE_DOCUMENTOS",
                            Icono = "fas fa-file-archive me-2"
                        },
                        new SubMenuDTO
                        {
                            Nombre = "Línea Cronológica",
                            Url = Url.Action("Cronologia","Expediente"),
                            Permiso = "MENU_EXPEDIENTE_CRONOLOGIA",
                            Icono = "fas fa-stream me-2"
                        }
                    }
                },

                // ======================================================
                // === ANAMNESIS
                // ======================================================
                new MenuDTO
                {
                    Nombre = "Anamnesis",
                    Icono = "fas fa-notes-medical",
                    SubMenu = new List<SubMenuDTO>
                    {
                        new SubMenuDTO
                        {
                            Nombre = "Registrar Anamnesis",
                            Url = Url.Action("Create","Anamnesis"),
                            Permiso = "MENU_ANAMNESIS_REGISTRAR",
                            Icono = "fas fa-plus me-2"
                        },
                        new SubMenuDTO
                        {
                            Nombre = "Historial",
                            Url = Url.Action("Index","Anamnesis"),
                            Permiso = "MENU_ANAMNESIS_HISTORIAL",
                            Icono = "fas fa-history me-2"
                        }
                    }
                },

                // ======================================================
                // === PRECEPTORÍAS
                // ======================================================
                new MenuDTO
                {
                    Nombre = "Preceptorías",
                    Icono = "fas fa-book-open",
                    SubMenu = new List<SubMenuDTO>
                    {
                        new SubMenuDTO
                        {
                            Nombre = "Registrar Preceptoría",
                            Url = Url.Action("Create","Preceptoria"),
                            Permiso = "MENU_PRECEPTORIA_REGISTRAR",
                            Icono = "fas fa-plus-circle me-2"
                        },
                        new SubMenuDTO
                        {
                            Nombre = "Historial de Preceptorías",
                            Url = Url.Action("Index","Preceptoria"),
                            Permiso = "MENU_PRECEPTORIA_HISTORIAL",
                            Icono = "fas fa-list me-2"
                        }
                    }
                },

                // ======================================================
                // === ANÁLISIS PSICOLÓGICO
                // ======================================================
                new MenuDTO
                {
                    Nombre = "Análisis Psicológico",
                    Icono = "fas fa-brain",
                    SubMenu = new List<SubMenuDTO>
                    {
                        new SubMenuDTO
                        {
                            Nombre = "Nuevo Análisis",
                            Url = Url.Action("Create","AnalisisPsicologico"),
                            Permiso = "MENU_PSICOLOGICO_NUEVO",
                            Icono = "fas fa-plus me-2"
                        },
                        new SubMenuDTO
                        {
                            Nombre = "Historial de Análisis",
                            Url = Url.Action("Index","AnalisisPsicologico"),
                            Permiso = "MENU_PSICOLOGICO_HISTORIAL",
                            Icono = "fas fa-file-alt me-2"
                        }
                    }
                },

                // ======================================================
                // === REGISTRO POST-CLASE
                // ======================================================
                new MenuDTO
                {
                    Nombre = "Registro Post-Clase",
                    Icono = "fas fa-chalkboard-teacher",
                    SubMenu = new List<SubMenuDTO>
                    {
                        new SubMenuDTO
                        {
                            Nombre = "Nuevo Registro",
                            Url = Url.Action("Create","PostClase"),
                            Permiso = "MENU_POSTCLASE_NUEVO",
                            Icono = "fas fa-plus me-2"
                        },
                        new SubMenuDTO
                        {
                            Nombre = "Historial",
                            Url = Url.Action("Index","PostClase"),
                            Permiso = "MENU_POSTCLASE_HISTORIAL",
                            Icono = "fas fa-list me-2"
                        }
                    }
                },

                // ======================================================
                // === SEGURIDAD
                // ======================================================
                new MenuDTO
                {
                    Nombre = "Seguridad",
                    Icono = "fas fa-shield-alt",
                    SubMenu = new List<SubMenuDTO>
                    {
                        new SubMenuDTO
                        {
                            Nombre = "Usuarios",
                            Url = Url.Action("Index","Usuario"),
                            Permiso = "MENU_USUARIOS",
                            Icono = "fas fa-user-lock me-2"
                        }
                    }
                }
            };
        }
    }
}

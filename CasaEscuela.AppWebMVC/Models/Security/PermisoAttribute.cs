using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CasaEscuela.AppWebMVC.Models.Security
{
    public class PermisoAttribute : ActionFilterAttribute
    {
        private readonly string _permisoBase;
        private readonly string _permisoFijo;

        public PermisoAttribute(string permisoBase, string permisoFijo = null)
        {
            _permisoBase = permisoBase;
            _permisoFijo = permisoFijo;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User;
            string permisoNecesario = _permisoFijo;

            if (_permisoFijo == null)
            {
                if (context.ActionArguments.TryGetValue("pAccion", out var accionObj) && accionObj != null)
                {
                    int accionValue = 0;

                    // Si pAccion viene como ActionsUI
                    if (accionObj is CasaEscuela.AppWebMVC.Models.ActionsUI actionUI)
                    {
                        accionValue = actionUI.Accion;
                    }
                    // Si pAccion viene solo como int
                    else if (accionObj is int intVal)
                    {
                        accionValue = intVal;
                    }

                    permisoNecesario = accionValue switch
                    {
                        (int)CasaEscuela.AppWebMVC.Models.ActionsUI_Enums.NUEVO => $"{_permisoBase}_CREAR", 
                        (int)CasaEscuela.AppWebMVC.Models.ActionsUI_Enums.IMPRIMIR => $"{_permisoBase}_IMPRIMIR",
                        (int)CasaEscuela.AppWebMVC.Models.ActionsUI_Enums.MODIFICAR => $"{_permisoBase}_EDITAR",
                        (int)CasaEscuela.AppWebMVC.Models.ActionsUI_Enums.ELIMINAR => $"{_permisoBase}_ELIMINAR",
                        (int)CasaEscuela.AppWebMVC.Models.ActionsUI_Enums.VER => $"{_permisoBase}_VER",
                        (int)CasaEscuela.AppWebMVC.Models.ActionsUI_Enums.CERRAR_TURNO => $"{_permisoBase}_CERRAR",
                        (int)CasaEscuela.AppWebMVC.Models.ActionsUI_Enums.CONSULTAR => $"{_permisoBase}_CONSULTAR",
                        _ => ""
                    };
                }
            }
            var allClaims = user.Claims.Select(c => c.Value).ToList();


            if (!string.IsNullOrEmpty(permisoNecesario) && !user.HasClaim("Permiso", permisoNecesario))
            {
                context.Result = new ForbidResult(); // 403 Forbidden
            }

            base.OnActionExecuting(context);
        }

    }

}

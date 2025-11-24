using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CasaEscuela.AppWebMVC.Models.Security
{
    public static class HtmlHelperPermisosExtensions
    {
        public static IHtmlContent ActionLinkIfPermiso(this IHtmlHelper html,
            string linkText,
            string actionName,
            object routeValues,
            object htmlAttributes,
            string permiso)
        {
            var user = html.ViewContext.HttpContext.User;

            var allClaims = user.Claims.Select(c => c.Value).ToList();

            if (user != null)
            {
                bool tienePermiso = user.Claims
                    .Where(c => c.Type == "Permiso")
                    .Any(c => string.Equals(c.Value, permiso, StringComparison.OrdinalIgnoreCase));

                if (tienePermiso)
                {
                    return html.ActionLink(linkText, actionName, routeValues, htmlAttributes);
                }
            }

            return HtmlString.Empty; 
        }
    }
}

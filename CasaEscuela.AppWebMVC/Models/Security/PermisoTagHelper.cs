using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CasaEscuela.AppWebMVC.Models.Security
{
    [HtmlTargetElement("*", Attributes = "asp-permiso")]
    public class PermisoTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermisoTagHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HtmlAttributeName("asp-permiso")]
        public string Permiso { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null || !user.HasClaim("Permiso", Permiso))
            {
                output.SuppressOutput(); // Elimina el HTML si no tiene el permiso
            }
        }
    }
}

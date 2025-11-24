using System.Security.Claims;

namespace CasaEscuela.AppWebMVC.Models.Utils
{
    public static class ClaimsPrincipalExtensions
    {
        public static int ObtenerSucursalId(this ClaimsPrincipal user)
        {
            var sucursalClaim = user.Claims.FirstOrDefault(c => c.Type == "IdSucursal")?.Value;
            if (string.IsNullOrEmpty(sucursalClaim) || !int.TryParse(sucursalClaim, out var id))
                throw new InvalidOperationException("El usuario no tiene un claim de sucursal válido.");
            return id;
        }
        public static int ObtenerIdUser(this ClaimsPrincipal user)
        {
            var idClaim = user.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

            if (string.IsNullOrEmpty(idClaim) || !int.TryParse(idClaim, out var id))
                throw new InvalidOperationException("El usuario no tiene un claim de Id válido.");
            return id;
        }


    }
}

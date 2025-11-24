using CasaEscuela.BL.Interfaces;
using System.Security.Claims;

namespace CasaEscuela.AppWebMVC.Models
{
    public class Credencial
    {
        //readonly HttpClient httpClient;
        //public Credencial(HttpClient pHttpClient)
        //{
        //    httpClient = pHttpClient;
        //}
        public Credencial( )
        {
        }
        public void Refrescar(ClaimsPrincipal principal)
        {
            var claimExpired = principal.FindFirst(ClaimTypes.GroupSid);
            if (claimExpired != null)
            {
                // httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", claimExpired.Value);
            }
        }
    }
}

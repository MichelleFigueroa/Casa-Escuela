using Microsoft.AspNetCore.SignalR;

namespace CasaEscuela.AppWebMVC.Hubs
{
    public class NombreUsuarioIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst("Id")?.Value; 
        }
    }
}

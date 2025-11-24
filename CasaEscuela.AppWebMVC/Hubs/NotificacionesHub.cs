using Microsoft.AspNetCore.SignalR;

namespace CasaEscuela.AppWebMVC.Hubs
{
    public class NotificacionesHub : Hub
    {
        public async Task NotificarCambioPermisos(string userId)
        {
            await Clients.User(userId).SendAsync("PermisosActualizados");
        }
    }
}

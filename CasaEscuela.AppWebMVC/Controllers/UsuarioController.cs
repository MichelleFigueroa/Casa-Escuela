using CasaEscuela.AppWebMVC.Hubs;
using CasaEscuela.AppWebMVC.Models;
using CasaEscuela.AppWebMVC.Models.Security;
using CasaEscuela.BL.DTOs.PermisoDTOs;

using CasaEscuela.BL.DTOs.UsuarioDTOs;
using CasaEscuela.BL.Interfaces;
using CasaEscuela.DAL;
using CasaEscuela.EN;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace CasaEscuela.AppWebMVC.Controllers
{
    [Permiso("USUARIO")]
    public class UsuarioController : Controller
    {
        readonly IUsuarioBL usuarioBL;
        readonly Credencial credencial;
        public UsuarioController(IUsuarioBL pUsuarioBL, Credencial pCredencial)
        {
            credencial = pCredencial;
            usuarioBL = pUsuarioBL;
        }


        [Permiso("USUARIO_INDEX")]
        public async Task<IActionResult> Index(UsuarioBuscarDTO pUsuario = null)
        {
            credencial.Refrescar(User);
            if (!User.HasClaim("Permiso", "USUARIO_INDEX"))
                return Forbid();
            if (pUsuario == null)
                pUsuario = new UsuarioBuscarDTO();

            if (pUsuario.Take == 0)
                pUsuario.Take = 10;

            var paginacion = await usuarioBL.BuscarAsync(pUsuario);



            return View(paginacion.Data);
        }

        public async Task<IActionResult> Mant(int id, ActionsUI pAccion)
        {
            if (pAccion.EsValidAction())
            {

                ViewBag.ActionsUI = pAccion;
                ViewBag.Error = "";
                UsuarioMantDTO usuarioMantDTO = new UsuarioMantDTO();
                if (pAccion.SiTraerDatos())
                {
                    try
                    {
                        usuarioMantDTO = await usuarioBL.ObtenerPorIdAsync(new UsuarioMantDTO { Id = id });
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }

                }
                var todosPermisos = await usuarioBL.ObtenerTodosPermisosAsync(); // método que devuelve List<PermisoDTO>
                if (usuarioMantDTO.Id > 0)
                {
                    var permisosAsignados = await usuarioBL.ObtenerPermisosUsuarioAsync(usuarioMantDTO.Id); // devuelve lista de IdPermiso
                    usuarioMantDTO.Permisoss = todosPermisos.Select(p => new PermisoDTO
                    {
                        Id = p.Id,
                        Nombre = p.Nombre,
                        Modulo = p.Modulo,
                        NombreAmigable =p.Nombre,
                        Asignado = permisosAsignados.Contains(p.Id)
                    }).ToList();
                }
                else
                {
                    usuarioMantDTO.Permisoss = todosPermisos;
                }
                return View(usuarioMantDTO);

            }
            else
                return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(string? pReturnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewBag.Url = pReturnUrl;
            ViewBag.Error = "";
            return View();
        }
        private string ObtenerNombreAmigable(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return string.Empty;

            // Dividimos por "_"
            var partes = nombre.Split('_');

            if (partes.Length == 0)
                return nombre;

            var accion = partes[0]; // CREAR, EDITAR, INDEX, etc.

            return accion switch
            {
                "CREAR" => "Crear",
                "EDITAR" => "Editar",
                "ELIMINAR" => "Eliminar",
                "VER" => "Ver",
                "INDEX" => "Listado",
                "ANULAR" => "Anular",
                "IMPRIMIR" => "Imprimir",
                "REPORTE" => "Reporte",
                "SEGURIDAD" => "Seguridad",
                "GENERAR" => "Generar",
                _ => accion // fallback si no está mapeado
            };
        }

        [HttpPost]
       // [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UsuarioLoginDTO pUsuario, string? pReturnUrl = null)
        {
            try
            {
                credencial.Refrescar(User);
                UsuarioMantDTO usuarioAut = await usuarioBL.LoginAsync(pUsuario);
                if (usuarioAut != null && usuarioAut.Email == pUsuario.Email)
                {

                    usuarioAut.Token = usuarioAut.Token == null ? "" : usuarioAut.Token;
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuarioAut.Email),
                        new Claim("Id", usuarioAut.Id.ToString()),
                        new Claim(ClaimTypes.GroupSid, usuarioAut.Token),
                        new Claim("IdSucursal", usuarioAut.IdSucursal?.ToString() ?? ""),
                    };
                    if (usuarioAut.Permisos != null)
                    {
                        foreach (var permiso in usuarioAut.Permisos)
                        {
                            claims.Add(new Claim("Permiso", permiso));
                        }
                    }

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                    var result = User.Identity.IsAuthenticated;
                    if (!string.IsNullOrWhiteSpace(pReturnUrl))
                        return Redirect(pReturnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                    throw new Exception("Credenciales incorrectas o usuario inactivo");
            }
            catch (Exception ex)
            {

                ViewBag.Url = pReturnUrl;
                ViewBag.Error = ex.Message;
                return View(new UsuarioLoginDTO { Email = pUsuario.Email });
            }           
        }
        public async Task<IActionResult> CambiarPassword()
        {
            credencial.Refrescar(User);
            var usuarioFind = await usuarioBL.BuscarAsync(new UsuarioBuscarDTO { Email_equal = User.Identity.Name, Take = 1 });
            var usuarioActual = new UsuarioCambiarPasswordDTO();
            if (usuarioFind.Data != null)
            {
                var usuario = usuarioFind.Data.FirstOrDefault();
                usuarioActual.Id = usuario.Id;
            }
            ViewBag.Error = "";
            return View(usuarioActual);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarPassword(UsuarioCambiarPasswordDTO pUsuario)
        {
            credencial.Refrescar(User);
            try
            {
                int result = await usuarioBL.CambiarPasswordAsync(pUsuario);
                if (result > 0)
                {
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    return RedirectToAction("Login", "Usuario");
                }
                else
                {
                    var usuarioFind = await usuarioBL.BuscarAsync(new UsuarioBuscarDTO { Email_equal = User.Identity.Name, Take = 1 });
                    var usuarioActual = new UsuarioCambiarPasswordDTO();
                    if (usuarioFind.Data != null)
                    {
                        var usuario = usuarioFind.Data.FirstOrDefault();
                        usuarioActual.Id = usuario.Id;
                    }
                    ViewBag.Error = "La contraseña actual no es correcta";
                    return View(usuarioActual);
                }
            }
            catch (Exception ex)
            {
                var usuarioFind = await usuarioBL.BuscarAsync(new UsuarioBuscarDTO { Email_equal = User.Identity.Name, Take = 1 });
                var usuarioActual = new UsuarioCambiarPasswordDTO();
                if (usuarioFind.Data != null)
                {
                    var usuario = usuarioFind.Data.FirstOrDefault();
                    usuarioActual.Id = usuario.Id;
                }

               
                ViewBag.Error = ex.Message;
                return View(usuarioActual);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioMantDTO pUsuario)
        {
            credencial.Refrescar(User);
            int result = await usuarioBL.CrearAsync(pUsuario);
            if (result > 0)
                TempData["Mensaje"] = "Usuario creado exitosamente.";
            else
                TempData["MensajeError"] = "No se pudo crear el usuario.";
            return RedirectToAction(nameof(Index));
        }


        [ValidateAntiForgeryToken]
        [Permiso("USUARIO_EDITAR")]
        [HttpPost]
        public async Task<IActionResult> Edit(UsuarioMantDTO pUsuario, [FromServices] IHubContext<NotificacionesHub> hubContext)
        {
            credencial.Refrescar(User);

            int result = await usuarioBL.ModificarAsync(pUsuario);

            if (result > 0)
            {

              

                await hubContext.Clients.User(pUsuario.Id.ToString()).SendAsync("PermisosActualizados");

                TempData["Mensaje"] = "Usuario modificado correctamente.";
            }
            else
            {
                TempData["MensajeError"] = "No se pudo modificar el usuario.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(UsuarioMantDTO pUsuario)
        {
            credencial.Refrescar(User);
            int result = await usuarioBL.EliminarAsync(pUsuario);
            if (result > 0)
                TempData["Mensaje"] = "Usuario eliminado correctamente.";
            else
                TempData["MensajeError"] = "No se pudo eliminar el usuario.";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Detail(UsuarioMantDTO pUsuario)
        {
            return RedirectToAction(nameof(Mant), new { id = pUsuario.Id, Accion = (int)ActionsUI_Enums.MODIFICAR });
        }

        [HttpPost]
        public async Task<ActionResult> ObtenerUsuarioPorDUI(UsuarioMantDTO pUsuario)
        {
            var usuario = await usuarioBL.ObtenerPorDUIAsync(new UsuarioMantDTO { DUI = pUsuario.DUI });

            return Json(usuario);
        }
        //public async Task<IActionResult> AutoCompleteUsuario(string query)
        //{
        //    var list = await usuarioBL.AutoCompleteUsuario(query);
        //    return Json(list);
        //}

       

       




        [HttpPost]
        public async Task<IActionResult> RefrescarPermisos()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (!int.TryParse(userIdClaim, out int idUsuario))
                return BadRequest("Usuario no válido.");

            await ActualizarPermisosUsuarioAsync(HttpContext, idUsuario);

            return Ok(new { success = true, message = "Permisos actualizados correctamente." });
        }

        private async Task ActualizarPermisosUsuarioAsync(HttpContext httpContext, int idUsuario)
        {
            var user = httpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
                throw new InvalidOperationException("El usuario no está autenticado.");

            var permisosIds = await usuarioBL.ObtenerPermisosUsuarioAsync(idUsuario);
            var todosPermisos = await usuarioBL.ObtenerTodosPermisosAsync(); 

            var claims = user.Claims.Where(c => c.Type != "Permiso").ToList();

            foreach (var permisoId in permisosIds)
            {
                var permiso = todosPermisos.FirstOrDefault(p => p.Id == permisoId);
                if (permiso != null)
                    claims.Add(new Claim("Permiso", permiso.Nombre));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }


    }
}

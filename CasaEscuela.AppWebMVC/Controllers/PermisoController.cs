using CasaEscuela.BL.DTOs.PermisosDTOs;
using CasaEscuela.BL.DTOs.UsuarioDTOs;
using CasaEscuela.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using CasaEscuela.AppWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasaEscuela.AppWebMVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "ADMINISTRADOR")]
    public class PermisoController : Controller
    {
        private readonly IPermisoBL _permisoBL;
        private readonly Credencial _credencial;

        public PermisoController(IPermisoBL permisoBL, Credencial credencial)
        {
            _permisoBL = permisoBL;
            _credencial = credencial;
        }

        // LISTADO DE PERMISOS
        public async Task<IActionResult> Index(int? idUsuario = null)
        {
            _credencial.Refrescar(User);
            var permisos = idUsuario.HasValue
                ? await _permisoBL.ObtenerPorUsuarioAsync(idUsuario.Value)
                : await _permisoBL.ObtenerTodosAsync();

            ViewBag.IdUsuario = idUsuario ?? 0;
            return View(permisos);
        }

        // MANTENIMIENTO: CREAR / MODIFICAR / VER
        public async Task<IActionResult> Mant(ActionsUI pAccion, int? idUsuario = null)
        {
            if (!pAccion.EsValidAction())
                return RedirectToAction(nameof(Index));

            _credencial.Refrescar(User);

            ViewBag.ActionsUI = pAccion;
            ViewBag.Error = "";

            // Vistas disponibles
            var vistasDisponibles = new List<string>
            {
                "Ventas","Clientes","Compra","Inventario","Usuarios",
                "Roles","Permisos","TrasladoBodega","AjusteManual",
                "ReporteVentas","ReporteInventario"
            };
            ViewBag.VistasDisponibles = vistasDisponibles;

            var permisos = new List<PermisoDTO>();
            try
            {
                var empleados = await _permisoBL.ObtenerUsuariosPorRolAsync("EMPLEADO");
                ViewBag.Empleados = empleados;

                int usuarioId = idUsuario ?? (empleados.Count > 0 ? empleados[0].Id : 0);

                if (pAccion.SiTraerDatos() && usuarioId > 0)
                {
                    permisos = await _permisoBL.ObtenerPorUsuarioAsync(usuarioId);
                    if (permisos.Count == 0)
                    {
                        foreach (var vista in vistasDisponibles)
                        {
                            permisos.Add(new PermisoDTO
                            {
                                Id = 0,
                                IdUsuario = usuarioId,
                                Vista = vista,
                                PuedeCrear = false,
                                PuedeEditar = false,
                                PuedeEliminar = false,
                                PuedeVer = false
                            });
                        }
                    }
                }
                else
                {
                    foreach (var vista in vistasDisponibles)
                    {
                        permisos.Add(new PermisoDTO
                        {
                            Id = 0,
                            IdUsuario = 0,
                            Vista = vista,
                            PuedeCrear = false,
                            PuedeEditar = false,
                            PuedeEliminar = false,
                            PuedeVer = false
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View(permisos);
        }

        // CREAR PERMISOS
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(List<PermisoDTO> permisos, List<int> empleadosSeleccionados)
        {
            _credencial.Refrescar(User);
            ViewBag.ActionsUI = new ActionsUI { Accion = (int)ActionsUI_Enums.NUEVO };

            if (permisos == null || permisos.Count == 0)
            {
                ViewBag.Error = "No hay permisos para asignar.";
                ViewBag.Empleados = await _permisoBL.ObtenerUsuariosPorRolAsync("EMPLEADO");
                return View("Mant", permisos ?? new List<PermisoDTO>());
            }
            if (empleadosSeleccionados == null || empleadosSeleccionados.Count == 0)
            {
                ViewBag.Error = "Debe seleccionar al menos un empleado.";
                ViewBag.Empleados = await _permisoBL.ObtenerUsuariosPorRolAsync("EMPLEADO");
                return View("Mant", permisos);
            }

            int result = await _permisoBL.CrearAsync(permisos, empleadosSeleccionados);
            TempData[result > 0 ? "Mensaje" : "MensajeError"] =
                result > 0 ? "Permisos asignados correctamente." : "No se pudieron asignar los permisos.";

            return RedirectToAction(nameof(Index), new { idUsuario = empleadosSeleccionados[0] });
        }

        // EDITAR PERMISOS
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(List<PermisoDTO> permisos)
        {
            _credencial.Refrescar(User);
            ViewBag.ActionsUI = new ActionsUI { Accion = (int)ActionsUI_Enums.MODIFICAR };

            if (permisos == null || permisos.Count == 0)
            {
                ViewBag.Error = "No hay permisos para modificar.";
                ViewBag.Empleados = await _permisoBL.ObtenerUsuariosPorRolAsync("EMPLEADO");
                return View("Mant", permisos ?? new List<PermisoDTO>());
            }

            int total = 0;
            foreach (var permiso in permisos)
            {
                total += await _permisoBL.ModificarAsync(permiso);
            }

            TempData[total > 0 ? "Mensaje" : "MensajeError"] =
                total > 0 ? "Permisos modificados correctamente." : "No se pudieron modificar los permisos.";

            return RedirectToAction(nameof(Index), new { idUsuario = permisos[0].IdUsuario });
        }

        // ELIMINAR PERMISO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(PermisoDTO pPermiso)
        {
            _credencial.Refrescar(User);
            int result = await _permisoBL.EliminarAsync(pPermiso);
            TempData[result > 0 ? "Mensaje" : "MensajeError"] =
                result > 0 ? "Permiso eliminado correctamente." : "No se pudo eliminar el permiso.";

            return RedirectToAction(nameof(Index), new { idUsuario = pPermiso.IdUsuario });
        }

        // AJAX: OBTENER PERMISOS POR USUARIO
        [HttpPost]
        public async Task<JsonResult> ObtenerPermisosPorUsuario(int idUsuario)
        {
            var permisos = await _permisoBL.ObtenerPorUsuarioAsync(idUsuario);
            return Json(permisos);
        }
    }
}

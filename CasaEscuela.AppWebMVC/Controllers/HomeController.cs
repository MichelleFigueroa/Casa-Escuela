using CasaEscuela.AppWebMVC.Models;
using CasaEscuela.AppWebMVC.Models.Menu;
using CasaEscuela.AppWebMVC.Models.Utils;
using CasaEscuela.BL.Interfaces;
using CasaEscuela.EN;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace CasaEscuela.AppWebMVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class HomeController : Controller
    {
     

        private readonly IAnamnesisBL _anamnesisBL;

        public HomeController(IAnamnesisBL anamnesisBL)
        {
            _anamnesisBL = anamnesisBL;
        }

        public async Task<IActionResult> Index()
        {
            var dashboardData = await _anamnesisBL.ObtenerDatosDashboardAsync();

            ViewBag.TotalEstudiantes = dashboardData.TotalEstudiantes;
            ViewBag.ExpedientesActivos = dashboardData.ExpedientesActivos;
            ViewBag.TotalAnamnesis = dashboardData.TotalAnamnesis;
            ViewBag.TotalPreceptorias = dashboardData.TotalPreceptorias;
            ViewBag.TotalAnalisis = dashboardData.TotalAnalisis;
            ViewBag.TotalDocumentos = dashboardData.TotalDocumentos;
            ViewBag.TotalPostClase = dashboardData.TotalPostClase;
            ViewBag.UsuariosActivos = dashboardData.UsuariosActivos;

            return View();
        }



    }
}

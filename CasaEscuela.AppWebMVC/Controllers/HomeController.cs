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
     

        public HomeController(
            )
        {
            
        }

        public IActionResult Index()
        {
            // ==== Primera fila ====
            ViewBag.TotalEstudiantes = 320;
            ViewBag.ExpedientesActivos = 280;
            ViewBag.TotalAnamnesis = 150;
            ViewBag.TotalPreceptorias = 210;

            // ==== Segunda fila ====
            ViewBag.TotalAnalisis = 95;
            ViewBag.TotalDocumentos = 430;
            ViewBag.TotalPostClase = 180;
            ViewBag.UsuariosActivos = 12;

            return View();
        }



    }
}

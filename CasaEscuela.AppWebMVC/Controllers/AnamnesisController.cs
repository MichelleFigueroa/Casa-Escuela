using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.EstudianteDTOs;
using CasaEscuela.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasaEscuela.AppWebMVC.Controllers
{
    public class AnamnesisController : Controller
    {
        private readonly IAnamnesisBL _anamnesisBL;
        private readonly ICatalogoBL _catalogoBL;
        private readonly IPreceptoriaBL _preceptoriaBL;

        public AnamnesisController(IAnamnesisBL anamnesisBL, ICatalogoBL catalogoBL, IPreceptoriaBL preceptoriaBL)
        {
            _anamnesisBL = anamnesisBL;
            _catalogoBL = catalogoBL;
            _preceptoriaBL = preceptoriaBL;
        }

        public async Task<IActionResult> Create(int? idEstudiante)
        {
            await CargarCatalogos();
            var model = new AnamnesisRegistroDTO();
            model.Estudiante = new EstudianteMantDTO();
            model.Anamnesis = new AnamnesisMantDTO();
            model.Familiares = new List<EstudianteFamiliarMantDTO>();

            if (idEstudiante.HasValue)
            {
                // Si ya existe el estudiante, podr├¡amos cargar sus datos.
                // Sin embargo, el requerimiento se enfoca en el registro inicial.
                model.Estudiante.IdEstudiante = idEstudiante.Value;
                model.Anamnesis.IdEstudiante = idEstudiante.Value;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnamnesisRegistroDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int idEstudiante = await _anamnesisBL.CrearAnamnesisAsync(model.Estudiante, model.Familiares, model.Anamnesis);
                    return RedirectToAction("Expediente", new { idEstudiante = idEstudiante });
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError("", "Error al guardar la anamnesis: " + ex.Message);
                }
            }

            await CargarCatalogos();
            return View(model);
        }

        public async Task<IActionResult> Expediente(int idEstudiante)
        {
            // Aqu├¡ deber├¡amos cargar todos los datos del estudiante para mostrar el expediente.
            // Dado que no tenemos un IEstudianteBL completo con ObtenerPorId detallado que incluya todo,
            // podr├¡amos necesitar extender las interfaces o usar lo que tenemos.
            
            // Para fines de este ejercicio, pasaremos el id y la vista se encargar├í de mostrar lo necesario
            // o asumiremos que tenemos un m├odotodo en IAnamnesisBL para obtener el expediente.
            
            ViewBag.IdEstudiante = idEstudiante;
            var preceptorias = await _preceptoriaBL.ObtenerPreceptoriasPorEstudianteAsync(idEstudiante);
            ViewBag.Preceptorias = preceptorias;
            
            // Tambi├on necesitaremos los datos de la anamnesis
            var anamnesis = await _anamnesisBL.ObtenerAnamnesisPorIdEstudianteAsync(idEstudiante);
            ViewBag.Anamnesis = anamnesis;

            return View();
        }

        private async Task CargarCatalogos()
        {
            ViewBag.NivelesEscolares = new SelectList(await _catalogoBL.ObtenerNivelesEscolaresAsync(), "Id", "Descripcion");
            ViewBag.TiposFamilia = new SelectList(await _catalogoBL.ObtenerTiposFamiliaAsync(), "Id", "Descripcion");
            ViewBag.ViveCon = new SelectList(await _catalogoBL.ObtenerViveConAsync(), "Id", "Descripcion");
            ViewBag.TiposParto = new SelectList(await _catalogoBL.ObtenerTiposPartoAsync(), "Id", "Descripcion");
        }
    }
}

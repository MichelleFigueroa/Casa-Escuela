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

        public async Task<IActionResult> Index()
        {
            var estudiantes = await _anamnesisBL.ObtenerEstudiantesConAnamnesisAsync();
            return View(estudiantes);
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
                model.Estudiante.IdEstudiante = idEstudiante.Value;
                model.Anamnesis.IdEstudiante = idEstudiante.Value;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnamnesisRegistroDTO model)
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
            

            await CargarCatalogos();
            return View(model);
        }

        public async Task<IActionResult> Expediente(int idEstudiante)
        {
            var registro = await _anamnesisBL.ObtenerAnamnesisRegistroPorIdEstudianteAsync(idEstudiante);
            if (registro == null) return NotFound();

            var preceptorias = await _preceptoriaBL.ObtenerPreceptoriasPorEstudianteAsync(idEstudiante);
            ViewBag.Preceptorias = preceptorias;
            
            return View(registro);
        }

        private async Task CargarCatalogos()
        {
            ViewBag.NivelesEscolares = new SelectList(await _catalogoBL.ObtenerNivelesEscolaresAsync(), "Id", "Descripcion");
            ViewBag.TiposFamilia = new SelectList(await _catalogoBL.ObtenerTiposFamiliaAsync(), "Id", "Descripcion");
            ViewBag.ViveCon = new SelectList(await _catalogoBL.ObtenerViveConAsync(), "Id", "Descripcion");
            ViewBag.TiposParto = new SelectList(await _catalogoBL.ObtenerTiposPartoAsync(), "Id", "Descripcion");
            ViewBag.TiposParto = new SelectList(await _catalogoBL.ObtenerTiposPartoAsync(), "Id", "Descripcion");
        }

        public async Task<IActionResult> DescargarAdjunto(int id)
        {
            var adjunto = await _anamnesisBL.ObtenerAdjuntoPorIdAsync(id);
            if (adjunto == null) return NotFound();

            return File(adjunto.Contenido, adjunto.ContentType, adjunto.NombreArchivo);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarAdjunto(int id)
        {
            try
            {
                var adjunto = await _anamnesisBL.ObtenerAdjuntoPorIdAsync(id);
                if (adjunto != null)
                {
                  
                    var anamnesisDTO = await _anamnesisBL.ObtenerAnamnesisRegistroPorIdEstudianteAsync(0); // This method requires IdEstudiante...
                    
                    int anamnesisId = adjunto.AnamnesisId;

                    await _anamnesisBL.EliminarAdjuntoAsync(id);

                    
                    return Redirect(Request.Headers["Referer"].ToString());
                }
                return NotFound();
            }
            catch (System.Exception ex)
            {
                return BadRequest("Error al eliminar adjunto: " + ex.Message);
            }
        }
    }
}

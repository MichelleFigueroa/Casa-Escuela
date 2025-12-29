using CasaEscuela.BL.DTOs.EstudianteDTOs;
using CasaEscuela.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace CasaEscuela.AppWebMVC.Controllers
{
    public class PreceptoriaController : Controller
    {
        private readonly IPreceptoriaBL _preceptoriaBL;
        private readonly ICatalogoBL _catalogoBL;

        public PreceptoriaController(IPreceptoriaBL preceptoriaBL, ICatalogoBL catalogoBL)
        {
            _preceptoriaBL = preceptoriaBL;
            _catalogoBL = catalogoBL;
        }

        public async Task<IActionResult> Index(int idEstudiante)
        {
            ViewBag.IdEstudiante = idEstudiante;
            var list = await _preceptoriaBL.ObtenerPreceptoriasPorEstudianteAsync(idEstudiante);
            return View(list);
        }

        public async Task<IActionResult> Create(int idEstudiante)
        {
            await CargarCatalogos();
            var model = new EstudiantePreceptoriaMantDTO
            {
                IdEstudiante = idEstudiante,
                Fecha = System.DateTime.Now
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EstudiantePreceptoriaMantDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _preceptoriaBL.GuardarPreceptoriaAsync(model);
                    return RedirectToAction("Index", new { idEstudiante = model.IdEstudiante });
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError("", "Error al guardar la preceptor├¡a: " + ex.Message);
                }
            }

            await CargarCatalogos();
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _preceptoriaBL.ObtenerPreceptoriaPorIdAsync(id);
            if (model == null) return NotFound();

            await CargarCatalogos();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EstudiantePreceptoriaMantDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _preceptoriaBL.GuardarPreceptoriaAsync(model);
                    return RedirectToAction("Index", new { idEstudiante = model.IdEstudiante });
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError("", "Error al actualizar la preceptor├¡a: " + ex.Message);
                }
            }

            await CargarCatalogos();
            return View(model);
        }

        private async Task CargarCatalogos()
        {
            ViewBag.TiposPreceptoria = new SelectList(await _catalogoBL.ObtenerTiposPreceptoriaAsync(), "Id", "Descripcion");
        }
    }
}

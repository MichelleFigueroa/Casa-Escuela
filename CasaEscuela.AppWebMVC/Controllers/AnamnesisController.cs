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
            
            AnamnesisRegistroDTO model;

            if (idEstudiante.HasValue)
            {
                model = await _anamnesisBL.ObtenerAnamnesisRegistroPorIdEstudianteAsync(idEstudiante.Value);
                if (model == null) 
                {
                     model = new AnamnesisRegistroDTO();
                     model.Estudiante = new EstudianteMantDTO { IdEstudiante = idEstudiante.Value };
                     model.Anamnesis = new AnamnesisMantDTO { IdEstudiante = idEstudiante.Value };
                     model.Familiares = new List<EstudianteFamiliarMantDTO>();
                }
            }
            else
            {
                // New
                model = new AnamnesisRegistroDTO();
                model.Estudiante = new EstudianteMantDTO();
                model.Anamnesis = new AnamnesisMantDTO();
                model.Familiares = new List<EstudianteFamiliarMantDTO>();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(AnamnesisRegistroDTO model)
        {
            try
            {
                int idEstudiante = await _anamnesisBL.GuardarAnamnesisAsync(
                    model.Estudiante,
                    model.Familiares,
                    model.Anamnesis
                );

                return RedirectToAction("Expediente", new { idEstudiante });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            await CargarCatalogos();
            return View("Create", model);
        }


        public async Task<IActionResult> Expediente(int idEstudiante)
        {
            var registro = await _anamnesisBL.ObtenerAnamnesisRegistroPorIdEstudianteAsync(idEstudiante);
            if (registro == null) return NotFound();

            var preceptorias = await _preceptoriaBL.ObtenerPreceptoriasPorEstudianteAsync(idEstudiante);
            ViewBag.Preceptorias = preceptorias;
            
            // Construir Timeline
            registro.Timeline = await ConstruirTimeline(registro, preceptorias);

            return View(registro);
        }

        public async Task<IActionResult> Cronologia(int idEstudiante)
        {
            var registro = await _anamnesisBL.ObtenerAnamnesisRegistroPorIdEstudianteAsync(idEstudiante);
            if (registro == null) return NotFound();

            var preceptorias = await _preceptoriaBL.ObtenerPreceptoriasPorEstudianteAsync(idEstudiante);
            registro.Timeline = await ConstruirTimeline(registro, preceptorias);

            return View(registro);
        }

        private async Task<List<TimelineItemDTO>> ConstruirTimeline(AnamnesisRegistroDTO registro, List<EstudiantePreceptoriaMantDTO> preceptorias)
        {
            var timeline = new List<TimelineItemDTO>();

            // 1. Registro
            if (registro.Estudiante.FechaRegistro != DateTime.MinValue)
            {
                timeline.Add(new TimelineItemDTO
                {
                    Fecha = registro.Estudiante.FechaRegistro,
                    Titulo = "Ingreso a Casa-Escuela",
                    Descripcion = "Registro inicial del estudiante en el sistema.",
                    Icono = "ti-user-plus",
                    Color = "success",
                    Tipo = "Inicio"
                });
            }

            // 2. Anamnesis
            if (registro.Anamnesis != null && registro.Anamnesis.IdAnamnesis > 0 && registro.Anamnesis.FechaEntrevista.HasValue)
            {
                timeline.Add(new TimelineItemDTO
                {
                    Fecha = registro.Anamnesis.FechaEntrevista.Value,
                    Titulo = "Entrevista Inicial (Anamnesis)",
                    Descripcion = $"Entrevista realizada por {registro.Anamnesis.Entrevistador}.",
                    Icono = "ti-clipboard-heart",
                    Color = "primary",
                    Tipo = "Anamnesis",
                    EntidadId = registro.Anamnesis.IdAnamnesis
                });

                // Attachments in Anamnesis
                if (registro.Anamnesis.AdjuntosExistentes != null)
                {
                   foreach(var adj in registro.Anamnesis.AdjuntosExistentes)
                   {
                        timeline.Add(new TimelineItemDTO
                        {
                            Fecha = adj.FechaCreacion,
                            Titulo = "Documento Adjunto",
                            Descripcion = $"Se adjuntó: {adj.NombreArchivo}",
                            Icono = "ti-paperclip",
                            Color = "info",
                            Tipo = "Documento"
                        });
                   }
                }
            }

            // 3. Preceptorias
            if (preceptorias != null)
            {
                foreach (var p in preceptorias)
                {
                    timeline.Add(new TimelineItemDTO
                    {
                        Fecha = p.Fecha,
                        Titulo = "Sesión de Preceptoría",
                        Descripcion = $"Tema: {p.ProcesosTrabajados}. " + (p.EstadoPreceptoria == 3 ? "Sesión Cerrada." : "Sesión de Seguimiento."),
                        Icono = "ti-messages",
                        Color = p.EstadoPreceptoria == 3 ? "secondary" : "warning",
                        Tipo = "Preceptoria",
                        EntidadId = p.Id
                    });
                }
            }

            return timeline.OrderByDescending(t => t.Fecha).ToList();
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

        public async Task<IActionResult> Edit(int idEstudiante)
        {
            await CargarCatalogos();

            var model = await _anamnesisBL.ObtenerAnamnesisRegistroPorIdEstudianteAsync(idEstudiante);

            if (model == null) return NotFound();

            return View("Create", model); // reutiliza la vista Create
        }

    }
}

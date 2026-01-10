using CasaEscuela.EN;
using CasaEscuela.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class EstudiantesController : Controller
{
    private readonly CasaEscuelaDBContext _context;

    public EstudiantesController(CasaEscuelaDBContext context)
    {
        _context = context;
    }

    // LISTADO GENERAL
    // El Index SIEMPRE manda una lista
    public async Task<IActionResult> Index()
    {
        var lista = await _context.Estudiantes.ToListAsync();
        return View(lista); // Aquí el modelo es List<EstudianteEN>
    }

    // El Details (y el reporte) SIEMPRE manda UNO solo
    public async Task<IActionResult> Details(int? id)
    {
        // Este "if" evita que si el ID llega vacío desde el menú lateral, el programa explote
        if (id == null)
        {
            return RedirectToAction(nameof(Index));
        }

        var estudiante = await _context.Estudiantes
            .Include(e => e.Anamnesis)
            .Include(e => e.Preceptorias)
            .FirstOrDefaultAsync(m => m.IdEstudiante == id);

        if (estudiante == null) return NotFound();

        return View(estudiante); // Aquí el modelo es EstudianteEN (uno solo)
    }
    public async Task<IActionResult> GenerarExpedientePdf(int? id)
    {
        if (id == null)
        {
            // Si no hay ID, lo mandamos al Index en lugar de mostrar error
            return RedirectToAction(nameof(Index));
        }

        var estudiante = await _context.Estudiantes
            .Include(e => e.Anamnesis)
            .Include(e => e.Preceptorias)
            .FirstOrDefaultAsync(e => e.IdEstudiante == id);

        if (estudiante == null) return NotFound();

        // IMPORTANTE: Aquí retornamos la vista que creamos para el reporte
        return View("VistaReporte", estudiante);
    }
}
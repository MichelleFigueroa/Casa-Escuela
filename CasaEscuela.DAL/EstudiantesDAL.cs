using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CasaEscuela.EN; // Aquí están tus clases Estudiante, Anamnesis, etc.

namespace CasaEscuela.DAL
{
    public class EstudianteDAL
    {
        // 1. Método para obtener todos los estudiantes (para el Index)
        public async Task<List<EstudianteEN>> ObtenerTodosAsync(DbContext context)
        {
            return await context.Set<EstudianteEN>().ToListAsync();
        }

        // 2. Método para el Expediente (Trae al estudiante + Anamnesis + Preceptorías)
        public async Task<EstudianteEN> ObtenerExpedienteCompletoAsync(int id, DbContext context)
        {
            return await context.Set<EstudianteEN>()
                .Include(e => e.Anamnesis)        // Junta la info de salud
                .Include(e => e.Preceptorias)     // Junta todas las visitas
                .FirstOrDefaultAsync(e => e.IdEstudiante == id);
        }
    }
}
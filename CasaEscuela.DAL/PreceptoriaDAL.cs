using CasaEscuela.BL.DTOs.EstudianteDTOs;
using CasaEscuela.BL.Interfaces;
using CasaEscuela.EN;
using CasaEscuela.EN.CatologosEN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaEscuela.DAL
{
    public class PreceptoriaDAL : IPreceptoriaBL
    {
        private readonly CasaEscuelaDBContext dbContext;

        public PreceptoriaDAL(CasaEscuelaDBContext context)
        {
            dbContext = context;
        }

        public async Task<int> GuardarPreceptoriaAsync(EstudiantePreceptoriaMantDTO pPreceptoria)
        {
            EstudiantePreceptoriaEN preceptoriaEN;

            if (pPreceptoria.Id > 0)
            {
                preceptoriaEN = await dbContext.EstudiantePreceptorias.FindAsync(pPreceptoria.Id);
                if (preceptoriaEN == null) throw new Exception("Preceptoría no encontrada");

                preceptoriaEN.TipoPreceptoriaId = pPreceptoria.TipoPreceptoriaId;
                preceptoriaEN.Fecha = pPreceptoria.Fecha;
                preceptoriaEN.ProcesosTrabajados = pPreceptoria.ProcesosTrabajados;
                preceptoriaEN.ProcesosDificultad = pPreceptoria.ProcesosDificultad;
                preceptoriaEN.MetasSiguientes = pPreceptoria.MetasSiguientes;
                preceptoriaEN.Recomendaciones = pPreceptoria.Recomendaciones;
                preceptoriaEN.EstadoPreceptoria = (EstadoPreceptoriaEnum)pPreceptoria.EstadoPreceptoria;
                preceptoriaEN.FechaActualizacion = DateTime.Now;

                dbContext.EstudiantePreceptorias.Update(preceptoriaEN);
            }
            else
            {
                preceptoriaEN = new EstudiantePreceptoriaEN
                {
                    IdEstudiante = pPreceptoria.IdEstudiante,
                    TipoPreceptoriaId = pPreceptoria.TipoPreceptoriaId,
                    Fecha = pPreceptoria.Fecha,
                    ProcesosTrabajados = pPreceptoria.ProcesosTrabajados,
                    ProcesosDificultad = pPreceptoria.ProcesosDificultad,
                    MetasSiguientes = pPreceptoria.MetasSiguientes,
                    Recomendaciones = pPreceptoria.Recomendaciones,
                    EstadoPreceptoria = (EstadoPreceptoriaEnum)pPreceptoria.EstadoPreceptoria,
                    FechaCreacion = DateTime.Now
                };
                dbContext.EstudiantePreceptorias.Add(preceptoriaEN);
            }

            await dbContext.SaveChangesAsync();
            return preceptoriaEN.Id;
        }

        public async Task<List<EstudiantePreceptoriaMantDTO>> ObtenerPreceptoriasPorEstudianteAsync(int idEstudiante)
        {
            var preceptorias = await dbContext.EstudiantePreceptorias
                .Where(p => p.IdEstudiante == idEstudiante)
                .OrderByDescending(p => p.Fecha)
                .ToListAsync();

            return preceptorias.Select(p => new EstudiantePreceptoriaMantDTO
            {
                Id = p.Id,
                IdEstudiante = p.IdEstudiante,
                TipoPreceptoriaId = p.TipoPreceptoriaId,
                Fecha = p.Fecha,
                ProcesosTrabajados = p.ProcesosTrabajados,
                ProcesosDificultad = p.ProcesosDificultad,
                MetasSiguientes = p.MetasSiguientes,
                Recomendaciones = p.Recomendaciones,
                EstadoPreceptoria = (byte)p.EstadoPreceptoria,
                FechaCreacion = p.FechaCreacion,
                FechaActualizacion = p.FechaActualizacion
            }).ToList();
        }

        public async Task<EstudiantePreceptoriaMantDTO> ObtenerPreceptoriaPorIdAsync(int id)
        {
            var p = await dbContext.EstudiantePreceptorias
                .FirstOrDefaultAsync(x => x.Id == id);

            if (p == null) return null;

            return new EstudiantePreceptoriaMantDTO
            {
                Id = p.Id,
                IdEstudiante = p.IdEstudiante,
                TipoPreceptoriaId = p.TipoPreceptoriaId,
                Fecha = p.Fecha,
                ProcesosTrabajados = p.ProcesosTrabajados,
                ProcesosDificultad = p.ProcesosDificultad,
                MetasSiguientes = p.MetasSiguientes,
                Recomendaciones = p.Recomendaciones,
                EstadoPreceptoria = (byte)p.EstadoPreceptoria,
                FechaCreacion = p.FechaCreacion,
                FechaActualizacion = p.FechaActualizacion
            };
        }

        public async Task<List<EstudiantePreceptoriaMantDTO>> ObtenerTodasAsync()
        {
            var preceptorias = await dbContext.EstudiantePreceptorias
                .Include(p => p.Estudiante)
                .OrderByDescending(p => p.Fecha)
                .ToListAsync();

            return preceptorias.Select(p => new EstudiantePreceptoriaMantDTO
            {
                Id = p.Id,
                IdEstudiante = p.IdEstudiante,
                TipoPreceptoriaId = p.TipoPreceptoriaId,
                Fecha = p.Fecha,
                ProcesosTrabajados = p.ProcesosTrabajados,
                ProcesosDificultad = p.ProcesosDificultad,
                MetasSiguientes = p.MetasSiguientes,
                Recomendaciones = p.Recomendaciones,
                EstadoPreceptoria = (byte)p.EstadoPreceptoria,
                FechaCreacion = p.FechaCreacion,
                FechaActualizacion = p.FechaActualizacion
            }).ToList();
        }
    }
}

using CasaEscuela.BL.DTOs.EstudianteDTOs;
using CasaEscuela.BL.Interfaces;
using CasaEscuela.EN;
using CasaEscuela.EN.CatologosEN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasaEscuela.BL.DTOs.PreceptoriaDTOs;
using System.IO;

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

            // Handle Attachments
            if (pPreceptoria.ArchivosSubidos != null && pPreceptoria.ArchivosSubidos.Count > 0)
            {
                foreach (var file in pPreceptoria.ArchivosSubidos)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            await file.CopyToAsync(ms);
                            var adjunto = new PreceptoriaAdjuntoEN
                            {
                                PreceptoriaId = preceptoriaEN.Id,
                                NombreArchivo = file.FileName,
                                ContentType = file.ContentType,
                                Contenido = ms.ToArray(),
                                FechaCreacion = DateTime.Now
                            };
                            dbContext.PreceptoriaAdjuntos.Add(adjunto);
                        }
                    }
                }
                await dbContext.SaveChangesAsync();
            }

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
                FechaActualizacion = p.FechaActualizacion,
                AdjuntosExistentes = await dbContext.PreceptoriaAdjuntos
                    .Where(a => a.PreceptoriaId == p.Id)
                    .Select(a => new PreceptoriaAdjuntoDTO
                    {
                        Id = a.Id,
                        PreceptoriaId = a.PreceptoriaId,
                        NombreArchivo = a.NombreArchivo,
                        ContentType = a.ContentType,
                        FechaCreacion = a.FechaCreacion
                    }).ToListAsync()
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

        public async Task<PreceptoriaAdjuntoDownloadDTO> ObtenerAdjuntoPorIdAsync(int id)
        {
            var adjunto = await dbContext.PreceptoriaAdjuntos.FindAsync(id);
            if (adjunto == null) return null;

            return new PreceptoriaAdjuntoDownloadDTO
            {
                Id = adjunto.Id,
                PreceptoriaId = adjunto.PreceptoriaId,
                NombreArchivo = adjunto.NombreArchivo,
                ContentType = adjunto.ContentType,
                FechaCreacion = adjunto.FechaCreacion,
                Contenido = adjunto.Contenido
            };
        }

        public async Task EliminarAdjuntoAsync(int id)
        {
            var adjunto = await dbContext.PreceptoriaAdjuntos.FindAsync(id);
            if (adjunto != null)
            {
                dbContext.PreceptoriaAdjuntos.Remove(adjunto);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}

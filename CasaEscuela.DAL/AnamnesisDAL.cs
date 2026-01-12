using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.EstudianteDTOs;
using CasaEscuela.BL.Interfaces;
using CasaEscuela.EN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace CasaEscuela.DAL
{
    public class AnamnesisDAL : IAnamnesisBL
    {
        private readonly CasaEscuelaDBContext dbContext;

        public AnamnesisDAL(CasaEscuelaDBContext context)
        {
            dbContext = context;
        }

        public async Task<int>  GuardarAnamnesisAsync(EstudianteMantDTO pEstudiante, List<EstudianteFamiliarMantDTO> pFamiliares, AnamnesisMantDTO pAnamnesis)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                EstudianteEN estudianteEN;
                
                if (pEstudiante.IdEstudiante > 0)
                {
                    estudianteEN = await dbContext.Estudiantes.FindAsync(pEstudiante.IdEstudiante);
                    if (estudianteEN == null) throw new Exception("Estudiante no encontrado");
                    
                    // Actualizar campos
                    estudianteEN.Codigo = pEstudiante.Codigo;
                    estudianteEN.Nombres = pEstudiante.Nombres;
                    estudianteEN.Apellidos = pEstudiante.Apellidos;
                    estudianteEN.FechaNacimiento = pEstudiante.FechaNacimiento;
                    estudianteEN.Sexo = (SexoEnum)pEstudiante.Sexo; 
                    estudianteEN.NivelEscolarId = pEstudiante.NivelEscolarId;
                    estudianteEN.Grado = pEstudiante.Grado;
                    estudianteEN.Seccion = pEstudiante.Seccion;
                    estudianteEN.Jornada = pEstudiante.Jornada;
                    
                    dbContext.Estudiantes.Update(estudianteEN);
                }
                else
                {
                    estudianteEN = new EstudianteEN
                    {
                        Codigo = pEstudiante.Codigo,
                        Nombres = pEstudiante.Nombres,
                        Apellidos = pEstudiante.Apellidos,
                        FechaNacimiento = pEstudiante.FechaNacimiento,
                        Sexo = (SexoEnum)pEstudiante.Sexo,
                        NivelEscolarId = pEstudiante.NivelEscolarId,
                        Grado = pEstudiante.Grado,
                        Seccion = pEstudiante.Seccion,
                        Jornada = pEstudiante.Jornada,
                        Estado = true,
                        FechaRegistro = DateTime.Now
                    };
                    dbContext.Estudiantes.Add(estudianteEN);
                }
                
                await dbContext.SaveChangesAsync();
                int idEstudiante = estudianteEN.IdEstudiante;

                // 2. Guardar Familiares (Actualizar: Borrar anteriores y crear nuevos simplificado, o actualizar uno a uno)
                // Estrategia: Borrar los existentes y recrear lista para limpiar
                var familiaresExistentes = await dbContext.EstudianteFamiliares.Where(f => f.IdEstudiante == idEstudiante).ToListAsync();
                dbContext.EstudianteFamiliares.RemoveRange(familiaresExistentes);
                await dbContext.SaveChangesAsync(); // Commit delete

                if (pFamiliares != null && pFamiliares.Any())
                {
                    foreach (var fam in pFamiliares)
                    {
                        var familiarEN = new EstudianteFamiliarEN
                        {
                            IdEstudiante = idEstudiante,
                            TipoParentesco = (TipoParentescoEnum)fam.TipoParentesco, 
                            Nombres = fam.Nombres,
                            Apellidos = fam.Apellidos,
                            Edad = fam.Edad,
                            Escolaridad = fam.Escolaridad,
                            Ocupacion = fam.Ocupacion,
                            ViveConEstudiante = fam.ViveConEstudiante,
                            Telefono = fam.Telefono
                        };
                        dbContext.EstudianteFamiliares.Add(familiarEN);
                    }
                    await dbContext.SaveChangesAsync();
                }

                // 3. Guardar Anamnesis
                // Verificar si existe
                var anamnesisExistente = await dbContext.Anamnesis.FirstOrDefaultAsync(a => a.IdEstudiante == idEstudiante);
                
                if (anamnesisExistente != null)
                {
                    // Update
                    anamnesisExistente.ViveConId = pAnamnesis.ViveConId;
                    anamnesisExistente.TipoFamiliaId = pAnamnesis.TipoFamiliaId;
                    anamnesisExistente.TipoPartoId = pAnamnesis.TipoPartoId;
                    anamnesisExistente.EmbarazoControlado = pAnamnesis.EmbarazoControlado;
                    anamnesisExistente.ComplicacionesEmbarazo = pAnamnesis.ComplicacionesEmbarazo;
                    anamnesisExistente.CondicionesSalud = pAnamnesis.CondicionesSalud;
                    anamnesisExistente.DesarrolloLenguaje = pAnamnesis.DesarrolloLenguaje;
                    anamnesisExistente.DesarrolloMotor = pAnamnesis.DesarrolloMotor;
                    anamnesisExistente.SituacionFamiliar = pAnamnesis.SituacionFamiliar;
                    anamnesisExistente.Observaciones = pAnamnesis.Observaciones;
                    anamnesisExistente.FechaEntrevista = pAnamnesis.FechaEntrevista;
                    anamnesisExistente.Entrevistador = pAnamnesis.Entrevistador;
                    
                    dbContext.Anamnesis.Update(anamnesisExistente);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    // Insert
                    var anamnesisEN = new AnamnesisEN
                    {
                        IdEstudiante = idEstudiante,
                        ViveConId = pAnamnesis.ViveConId,
                        TipoFamiliaId = pAnamnesis.TipoFamiliaId,
                        TipoPartoId = pAnamnesis.TipoPartoId,
                        EmbarazoControlado = pAnamnesis.EmbarazoControlado,
                        ComplicacionesEmbarazo = pAnamnesis.ComplicacionesEmbarazo,
                        CondicionesSalud = pAnamnesis.CondicionesSalud,
                        DesarrolloLenguaje = pAnamnesis.DesarrolloLenguaje,
                        DesarrolloMotor = pAnamnesis.DesarrolloMotor,
                        SituacionFamiliar = pAnamnesis.SituacionFamiliar,
                        Observaciones = pAnamnesis.Observaciones,
                        FechaEntrevista = pAnamnesis.FechaEntrevista,
                        Entrevistador = pAnamnesis.Entrevistador
                    };
                
                    dbContext.Anamnesis.Add(anamnesisEN);
                    await dbContext.SaveChangesAsync();
                    anamnesisExistente = anamnesisEN; // Asignar para uso abajo
                }


                // 4. Guardar Adjuntos
                if (pAnamnesis.ArchivosSubidos != null && pAnamnesis.ArchivosSubidos.Count > 0)
                {
                    foreach (var file in pAnamnesis.ArchivosSubidos)
                    {
                        if (file.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                await file.CopyToAsync(ms);
                                var adjunto = new AnamnesisAdjuntoEN
                                {
                                    AnamnesisId = anamnesisExistente.IdAnamnesis,
                                    NombreArchivo = file.FileName,
                                    ContentType = file.ContentType,
                                    Contenido = ms.ToArray(),
                                    FechaCreacion = DateTime.Now
                                };
                                dbContext.AnamnesisAdjuntos.Add(adjunto);
                            }
                        }
                    }
                    await dbContext.SaveChangesAsync();
                }

                await transaction.CommitAsync();
                return idEstudiante;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<AnamnesisMantDTO> ObtenerAnamnesisPorIdEstudianteAsync(int idEstudiante)
        {
             var anamnesis = await dbContext.Anamnesis
                .FirstOrDefaultAsync(a => a.IdEstudiante == idEstudiante);

            if (anamnesis == null) return null;

            return new AnamnesisMantDTO
            {
                IdAnamnesis = anamnesis.IdAnamnesis,
                IdEstudiante = anamnesis.IdEstudiante,
                ViveConId = anamnesis.ViveConId,
                TipoFamiliaId = anamnesis.TipoFamiliaId,
                TipoPartoId = anamnesis.TipoPartoId,
                EmbarazoControlado = anamnesis.EmbarazoControlado,
                ComplicacionesEmbarazo = anamnesis.ComplicacionesEmbarazo,
                CondicionesSalud = anamnesis.CondicionesSalud,
                DesarrolloLenguaje = anamnesis.DesarrolloLenguaje,
                DesarrolloMotor = anamnesis.DesarrolloMotor,
                SituacionFamiliar = anamnesis.SituacionFamiliar,
                Observaciones = anamnesis.Observaciones,
                FechaEntrevista = anamnesis.FechaEntrevista,
                Entrevistador = anamnesis.Entrevistador
            };
        }

        public async Task<List<EstudianteMantDTO>> ObtenerEstudiantesConAnamnesisAsync()
        {
            var list = await dbContext.Anamnesis
                .Include(a => a.Estudiante)
                .Select(a => new EstudianteMantDTO
                {
                    IdEstudiante = a.Estudiante.IdEstudiante,
                    Codigo = a.Estudiante.Codigo,
                    Nombres = a.Estudiante.Nombres,
                    Apellidos = a.Estudiante.Apellidos,
                    FechaNacimiento = a.Estudiante.FechaNacimiento,
                    Grado = a.Estudiante.Grado,
                    Seccion = a.Estudiante.Seccion
                })
                .ToListAsync();

            return list;
        }

        public async Task<AnamnesisRegistroDTO> ObtenerAnamnesisRegistroPorIdEstudianteAsync(int idEstudiante)
        {
            var estudiante = await dbContext.Estudiantes
                .Include(e => e.Familiares)
                .Include(e => e.Anamnesis)
                .FirstOrDefaultAsync(e => e.IdEstudiante == idEstudiante);

            if (estudiante == null) return null;

            return new AnamnesisRegistroDTO
            {
                Estudiante = new EstudianteMantDTO
                {
                    IdEstudiante = estudiante.IdEstudiante,
                    Codigo = estudiante.Codigo,
                    Nombres = estudiante.Nombres,
                    Apellidos = estudiante.Apellidos,
                    FechaNacimiento = estudiante.FechaNacimiento,
                    Sexo = (byte)estudiante.Sexo,
                    NivelEscolarId = estudiante.NivelEscolarId,
                    Grado = estudiante.Grado,
                    Seccion = estudiante.Seccion,
                    Jornada = estudiante.Jornada,
                    FechaRegistro = estudiante.FechaRegistro
                },
                Familiares = estudiante.Familiares.Select(f => new EstudianteFamiliarMantDTO
                {
                    IdFamiliar = f.IdFamiliar,
                    IdEstudiante = f.IdEstudiante,
                    TipoParentesco = (byte)f.TipoParentesco,
                    Nombres = f.Nombres,
                    Apellidos = f.Apellidos,
                    Edad = f.Edad,
                    Escolaridad = f.Escolaridad,
                    Ocupacion = f.Ocupacion,
                    ViveConEstudiante = f.ViveConEstudiante,
                    Telefono = f.Telefono
                }).ToList(),
                Anamnesis = estudiante.Anamnesis != null ? new AnamnesisMantDTO
                {
                    IdAnamnesis = estudiante.Anamnesis.IdAnamnesis,
                    IdEstudiante = estudiante.Anamnesis.IdEstudiante,
                    ViveConId = estudiante.Anamnesis.ViveConId,
                    TipoFamiliaId = estudiante.Anamnesis.TipoFamiliaId,
                    TipoPartoId = estudiante.Anamnesis.TipoPartoId,
                    EmbarazoControlado = estudiante.Anamnesis.EmbarazoControlado,
                    ComplicacionesEmbarazo = estudiante.Anamnesis.ComplicacionesEmbarazo,
                    CondicionesSalud = estudiante.Anamnesis.CondicionesSalud,
                    DesarrolloLenguaje = estudiante.Anamnesis.DesarrolloLenguaje,
                    DesarrolloMotor = estudiante.Anamnesis.DesarrolloMotor,
                    SituacionFamiliar = estudiante.Anamnesis.SituacionFamiliar,
                    Observaciones = estudiante.Anamnesis.Observaciones,
                    FechaEntrevista = estudiante.Anamnesis.FechaEntrevista,
                    Entrevistador = estudiante.Anamnesis.Entrevistador,
                    AdjuntosExistentes = await dbContext.AnamnesisAdjuntos
                        .Where(a => a.AnamnesisId == estudiante.Anamnesis.IdAnamnesis)
                        .Select(a => new AnamnesisAdjuntoDTO
                        {
                            Id = a.Id,
                            AnamnesisId = a.AnamnesisId,
                            NombreArchivo = a.NombreArchivo,
                            ContentType = a.ContentType,
                            FechaCreacion = a.FechaCreacion
                        }).ToListAsync()
                } : new AnamnesisMantDTO()
            };
        }

        public async Task<AnamnesisAdjuntoDownloadDTO> ObtenerAdjuntoPorIdAsync(int id)
        {
            var adjunto = await dbContext.AnamnesisAdjuntos.FindAsync(id);
            if (adjunto == null) return null;

            return new AnamnesisAdjuntoDownloadDTO
            {
                Id = adjunto.Id,
                AnamnesisId = adjunto.AnamnesisId,
                NombreArchivo = adjunto.NombreArchivo,
                ContentType = adjunto.ContentType,
                FechaCreacion = adjunto.FechaCreacion,
                Contenido = adjunto.Contenido
            };
        }

        public async Task EliminarAdjuntoAsync(int id)
        {
            var adjunto = await dbContext.AnamnesisAdjuntos.FindAsync(id);
            if (adjunto != null)
            {
                dbContext.AnamnesisAdjuntos.Remove(adjunto);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}

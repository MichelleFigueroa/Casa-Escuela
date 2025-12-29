using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.EstudianteDTOs;
using CasaEscuela.BL.Interfaces;
using CasaEscuela.EN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaEscuela.DAL
{
    public class AnamnesisDAL : IAnamnesisBL
    {
        private readonly CasaEscuelaDBContext dbContext;

        public AnamnesisDAL(CasaEscuelaDBContext context)
        {
            dbContext = context;
        }

        public async Task<int> CrearAnamnesisAsync(EstudianteMantDTO pEstudiante, List<EstudianteFamiliarMantDTO> pFamiliares, AnamnesisMantDTO pAnamnesis)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                // 1. Guardar o Actualizar Estudiante
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
                    estudianteEN.Sexo = (SexoEnum)pEstudiante.Sexo; // Asumiendo Enum
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

                // 2. Guardar Familiares
                if (pFamiliares != null && pFamiliares.Any())
                {
                    // Si es edición, podríamos borrar los anteriores o actualizar. 
                    // El requerimiento dice "Registro y consulta". Asumiremos adición de nuevos o reemplazo?
                    // "Registro de familiares". Probablemente se añadan.
                    // Pero si es "Crear Anamnesis", suele ser un proceso inicial.
                    // Si ya existen, tal vez no duplicar?
                    // Para simplificar y cumplir con "Una sola pantalla", si hay familiares en la lista, los agregamos/actualizamos.
                    // Si asumimos que vienen todos los actuales, podríamos borrar y reinsertar o hacer merge.
                    // Dado el alcance, vamos a insertar los nuevos.
                    
                    foreach (var fam in pFamiliares)
                    {
                        var familiarEN = new EstudianteFamiliarEN
                        {
                            IdEstudiante = idEstudiante,
                            TipoParentesco = (TipoParentescoEnum)fam.TipoParentesco, // Asumiendo Enum en DTO es compatible o int
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
    }
}

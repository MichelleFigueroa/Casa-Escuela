using CasaEscuela.BL.Interfaces;
using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.EstudianteDTOs;
using CasaEscuela.EN;

namespace CasaEscuela.BL
{
    public class EstudianteBL : IEstudianteBL
    {
        public async Task<int> CrearAsync(EstudianteMantDTO pEstudiante)
        {
            EstudianteEN entidad = new EstudianteEN
            {
                Nombre = pEstudiante.Nombre,
                fecha_nacimiento = pEstudiante.fecha_nacimiento,
                edad = pEstudiante.edad,
                centro_escolar = pEstudiante.centro_escolar,
                grado = pEstudiante.grado,
                estado = pEstudiante.estado
            };

            // return await _estudianteDAL.CrearAsync(entidad);
            return 1;
        }

        public async Task<int> ModificarAsync(EstudianteMantDTO pEstudiante)
        {
            EstudianteEN entidad = new EstudianteEN
            {
                Id = pEstudiante.Id,
                Nombre = pEstudiante.Nombre,
                fecha_nacimiento = pEstudiante.fecha_nacimiento,
                edad = pEstudiante.edad,
                centro_escolar = pEstudiante.centro_escolar,
                grado = pEstudiante.grado,
                estado = pEstudiante.estado
            };

            // return await _estudianteDAL.ModificarAsync(entidad);
            return 1;
        }

        public async Task<int> EliminarAsync(EstudianteMantDTO pEstudiante)
        {
            // return await _estudianteDAL.EliminarAsync(pEstudiante.Id);
            return 1;
        }

        public async Task<EstudianteMantDTO> ObtenerPorIdAsync(EstudianteMantDTO pEstudiante)
        {
            return pEstudiante;
        }

        public async Task<PaginacionOutputDTO<List<EstudianteMantDTO>>> BuscarAsync(EstudianteBuscarDTO pEstudiante)
        {
            return new PaginacionOutputDTO<List<EstudianteMantDTO>>();
        }
    }
}
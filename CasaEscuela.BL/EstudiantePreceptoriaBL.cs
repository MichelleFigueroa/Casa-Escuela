using CasaEscuela.BL.Interfaces;
using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.EstudiantePreceptoriaDTOs;
using CasaEscuela.EN;

namespace CasaEscuela.BL
{
    public class EstudiantePreceptoriaBL : IEstudiantePreceptoriaBL
    {
        public async Task<int> CrearAsync(EstudiantePreceptoriaMantDTO pPreceptoria)
        {
            EstudiantePreceptoriaEN entidad = new EstudiantePreceptoriaEN
            {
                fecha = pPreceptoria.fecha,
                procesos_trabajados = pPreceptoria.procesos_trabajados,
                procesos_dificultad = pPreceptoria.procesos_dificultad,
                metas_siguientes = pPreceptoria.metas_siguientes,
                recomendaciones = pPreceptoria.recomendaciones,
                fechaCreacion = DateTime.Now, // Se asigna la fecha actual al crear
                fechaActualizacion = DateTime.Now
            };

            // return await _preceptoriaDAL.CrearAsync(entidad);
            return 1;
        }

        public async Task<int> ModificarAsync(EstudiantePreceptoriaMantDTO pPreceptoria)
        {
            EstudiantePreceptoriaEN entidad = new EstudiantePreceptoriaEN
            {
                Id = pPreceptoria.Id,
                fecha = pPreceptoria.fecha,
                procesos_trabajados = pPreceptoria.procesos_trabajados,
                procesos_dificultad = pPreceptoria.procesos_dificultad,
                metas_siguientes = pPreceptoria.metas_siguientes,
                recomendaciones = pPreceptoria.recomendaciones,
                fechaActualizacion = DateTime.Now // Se actualiza la fecha de modificación
            };

            // return await _preceptoriaDAL.ModificarAsync(entidad);
            return 1;
        }

        public async Task<int> EliminarAsync(EstudiantePreceptoriaMantDTO pPreceptoria)
        {
            // return await _preceptoriaDAL.EliminarAsync(pPreceptoria.Id);
            return 1;
        }

        public async Task<EstudiantePreceptoriaMantDTO> ObtenerPorIdAsync(EstudiantePreceptoriaMantDTO pPreceptoria)
        {
            return pPreceptoria;
        }

        public async Task<PaginacionOutputDTO<List<EstudiantePreceptoriaMantDTO>>> BuscarAsync(EstudiantePreceptoriaBuscarDTO pPreceptoria)
        {
            return new PaginacionOutputDTO<List<EstudiantePreceptoriaMantDTO>>();
        }
    }
}
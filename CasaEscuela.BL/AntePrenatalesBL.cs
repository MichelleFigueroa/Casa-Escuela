using CasaEscuela.BL.Interfaces;
using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.AntePrenatalesDTOs;
using CasaEscuela.EN;

namespace CasaEscuela.BL
{
    public class AntePrenatalesBL : IAntePrenatalesBL
    {
        public async Task<int> CrearAsync(AntePrenatalesMantDTO pAntePrenatales)
        {
            AntePrenatalesEN entidad = new AntePrenatalesEN
            {
                duracion_Emb = pAntePrenatales.duracion_Emb,
                medicamentos = pAntePrenatales.medicamentos,
                emfermedades = pAntePrenatales.emfermedades,
                caidas = pAntePrenatales.caidas,
                controles_medic = pAntePrenatales.controles_medic,
                estado_emo = pAntePrenatales.estado_emo,
                estado_nutri = pAntePrenatales.estado_nutri,
                info_adicionales = pAntePrenatales.info_adicionales
            };

            // Aquí se llamaría a la DAL: return await _antePrenatalesDAL.CrearAsync(entidad);
            return 1;
        }

        public async Task<int> ModificarAsync(AntePrenatalesMantDTO pAntePrenatales)
        {
            AntePrenatalesEN entidad = new AntePrenatalesEN
            {
                Id = pAntePrenatales.Id,
                duracion_Emb = pAntePrenatales.duracion_Emb,
                medicamentos = pAntePrenatales.medicamentos,
                emfermedades = pAntePrenatales.emfermedades,
                caidas = pAntePrenatales.caidas,
                controles_medic = pAntePrenatales.controles_medic,
                estado_emo = pAntePrenatales.estado_emo,
                estado_nutri = pAntePrenatales.estado_nutri,
                info_adicionales = pAntePrenatales.info_adicionales
            };

            // return await _antePrenatalesDAL.ModificarAsync(entidad);
            return 1;
        }

        public async Task<int> EliminarAsync(AntePrenatalesMantDTO pAntePrenatales)
        {
            // return await _antePrenatalesDAL.EliminarAsync(pAntePrenatales.Id);
            return 1;
        }

        public async Task<AntePrenatalesMantDTO> ObtenerPorIdAsync(AntePrenatalesMantDTO pAntePrenatales)
        {
            // Lógica para retornar el DTO desde la base de datos
            return pAntePrenatales;
        }

        public async Task<PaginacionOutputDTO<List<AntePrenatalesMantDTO>>> BuscarAsync(AntePrenatalesBuscarDTO pAntePrenatales)
        {
            return new PaginacionOutputDTO<List<AntePrenatalesMantDTO>>();
        }
    }
}
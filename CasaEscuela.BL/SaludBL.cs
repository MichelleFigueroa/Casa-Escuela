using CasaEscuela.BL.Interfaces;
using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.SaludDTOs;
using CasaEscuela.EN;

namespace CasaEscuela.BL
{
    public class SaludBL : ISaludBL
    {
        public async Task<int> CrearAsync(SaludMantDTO pSalud)
        {
            SaludEN entidad = new SaludEN
            {
                descripcion = pSalud.descripcion,
                IdEntrevista = pSalud.IdEntrevista
            };

            // return await _saludDAL.CrearAsync(entidad);
            return 1;
        }

        public async Task<int> ModificarAsync(SaludMantDTO pSalud)
        {
            SaludEN entidad = new SaludEN
            {
                Id = pSalud.Id,
                descripcion = pSalud.descripcion,
                IdEntrevista = pSalud.IdEntrevista
            };

            // return await _saludDAL.ModificarAsync(entidad);
            return 1;
        }

        public async Task<int> EliminarAsync(SaludMantDTO pSalud)
        {
            // return await _saludDAL.EliminarAsync(pSalud.Id);
            return 1;
        }

        public async Task<SaludMantDTO> ObtenerPorIdAsync(SaludMantDTO pSalud)
        {
            return pSalud;
        }

        public async Task<PaginacionOutputDTO<List<SaludMantDTO>>> BuscarAsync(SaludBuscarDTO pSalud)
        {
            return new PaginacionOutputDTO<List<SaludMantDTO>>();
        }
    }
}
using CasaEscuela.BL.Interfaces;
using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.CorrelativoDTOs;
using CasaEscuela.EN;

namespace CasaEscuela.BL
{
    public class CorrelativoBL : ICorrelativoBL
    {
        public async Task<int> CrearAsync(CorrelativoMantDTO pCorrelativo)
        {
            CorrelativoEN entidad = new CorrelativoEN
            {
                Tipo = pCorrelativo.Tipo,
                Valor = pCorrelativo.Valor,
                AliasInicio = pCorrelativo.AliasInicio,
                UltFechaActualizacion = pCorrelativo.UltFechaActualizacion,
                AliasFin = pCorrelativo.AliasFin,
                IdSucursal = pCorrelativo.IdSucursal
            };

            // return await _correlativoDAL.CrearAsync(entidad);
            return 1;
        }

        public async Task<int> ModificarAsync(CorrelativoMantDTO pCorrelativo)
        {
            CorrelativoEN entidad = new CorrelativoEN
            {
                Id = pCorrelativo.Id,
                Tipo = pCorrelativo.Tipo,
                Valor = pCorrelativo.Valor,
                AliasInicio = pCorrelativo.AliasInicio,
                UltFechaActualizacion = pCorrelativo.UltFechaActualizacion,
                AliasFin = pCorrelativo.AliasFin,
                IdSucursal = pCorrelativo.IdSucursal
            };

            // return await _correlativoDAL.ModificarAsync(entidad);
            return 1;
        }

        public async Task<int> EliminarAsync(CorrelativoMantDTO pCorrelativo)
        {
            // return await _correlativoDAL.EliminarAsync(pCorrelativo.Id);
            return 1;
        }

        public async Task<CorrelativoMantDTO> ObtenerPorIdAsync(CorrelativoMantDTO pCorrelativo)
        {
            return pCorrelativo;
        }

        public async Task<PaginacionOutputDTO<List<CorrelativoMantDTO>>> BuscarAsync(CorrelativoBuscarDTO pCorrelativo)
        {
            return new PaginacionOutputDTO<List<CorrelativoMantDTO>>();
        }
    }
}
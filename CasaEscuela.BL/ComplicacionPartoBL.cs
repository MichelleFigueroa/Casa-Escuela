using CasaEscuela.BL.Interfaces;
using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.ComplicacionPartoDTOs;
using CasaEscuela.EN;

namespace CasaEscuela.BL
{
    public class ComplicacionPartoBL : IComplicacionPartoBL
    {
        public async Task<int> CrearAsync(ComplicacionPartoMantDTO pComplicacion)
        {
            ComplicacionPartoEN entidad = new ComplicacionPartoEN
            {
                tipo_parto = pComplicacion.tipo_parto,
                descripcion = pComplicacion.descripcion,
                IdParto = pComplicacion.IdParto
            };

            // return await _complicacionPartoDAL.CrearAsync(entidad);
            return 1;
        }

        public async Task<int> ModificarAsync(ComplicacionPartoMantDTO pComplicacion)
        {
            ComplicacionPartoEN entidad = new ComplicacionPartoEN
            {
                Id = pComplicacion.Id,
                tipo_parto = pComplicacion.tipo_parto,
                descripcion = pComplicacion.descripcion,
                IdParto = pComplicacion.IdParto
            };

            // return await _complicacionPartoDAL.ModificarAsync(entidad);
            return 1;
        }

        public async Task<int> EliminarAsync(ComplicacionPartoMantDTO pComplicacion)
        {
            // return await _complicacionPartoDAL.EliminarAsync(pComplicacion.Id);
            return 1;
        }

        public async Task<ComplicacionPartoMantDTO> ObtenerPorIdAsync(ComplicacionPartoMantDTO pComplicacion)
        {
            return pComplicacion;
        }

        public async Task<PaginacionOutputDTO<List<ComplicacionPartoMantDTO>>> BuscarAsync(ComplicacionPartoBuscarDTO pComplicacion)
        {
            return new PaginacionOutputDTO<List<ComplicacionPartoMantDTO>>();
        }
    }
}
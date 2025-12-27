using CasaEscuela.BL.Interfaces;
using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.FamiliaDTOs;
using CasaEscuela.EN;

namespace CasaEscuela.BL
{
    public class FamiliaBL : IFamiliaBL
    {
        public async Task<int> CrearAsync(FamiliaMantDTO pFamilia)
        {
            FamiliaEN entidad = new FamiliaEN
            {
                Nombre_Madre = pFamilia.Nombre_Madre,
                Ocupacion_Madre = pFamilia.Ocupacion_Madre,
                Nombre_Padre = pFamilia.Nombre_Padre,
                Ocupacion_Padre = pFamilia.Ocupacion_Padre,
                Telefono = pFamilia.Telefono,
                Direccion = pFamilia.Direccion
            };

            // return await _familiaDAL.CrearAsync(entidad);
            return 1;
        }

        public async Task<int> ModificarAsync(FamiliaMantDTO pFamilia)
        {
            FamiliaEN entidad = new FamiliaEN
            {
                Id = pFamilia.Id,
                Nombre_Madre = pFamilia.Nombre_Madre,
                Ocupacion_Madre = pFamilia.Ocupacion_Madre,
                Nombre_Padre = pFamilia.Nombre_Padre,
                Ocupacion_Padre = pFamilia.Ocupacion_Padre,
                Telefono = pFamilia.Telefono,
                Direccion = pFamilia.Direccion
            };

            // return await _familiaDAL.ModificarAsync(entidad);
            return 1;
        }

        public async Task<int> EliminarAsync(FamiliaMantDTO pFamilia)
        {
            // return await _familiaDAL.EliminarAsync(pFamilia.Id);
            return 1;
        }

        public async Task<FamiliaMantDTO> ObtenerPorIdAsync(FamiliaMantDTO pFamilia)
        {
            return pFamilia;
        }

        public async Task<PaginacionOutputDTO<List<FamiliaMantDTO>>> BuscarAsync(FamiliaBuscarDTO pFamilia)
        {
            return new PaginacionOutputDTO<List<FamiliaMantDTO>>();
        }
    }
}
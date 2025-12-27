using CasaEscuela.BL.Interfaces;
using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.DesarrolloLenguajeDTOs;
using CasaEscuela.EN;

namespace CasaEscuela.BL
{
    public class DesarrolloLenguajeBL : IDesarrolloLenguajeBL
    {
        public async Task<int> CrearAsync(DesarrolloLenguajeMantDTO pDesarrollo)
        {
            DesarrolloLenguajeEN entidad = new DesarrolloLenguajeEN
            {
                Descripcion = pDesarrollo.Descripcion,
                IdEntrevista = pDesarrollo.IdEntrevista
            };

            // return await _desarrolloLenguajeDAL.CrearAsync(entidad);
            return 1;
        }

        public async Task<int> ModificarAsync(DesarrolloLenguajeMantDTO pDesarrollo)
        {
            DesarrolloLenguajeEN entidad = new DesarrolloLenguajeEN
            {
                Id = pDesarrollo.Id,
                Descripcion = pDesarrollo.Descripcion,
                IdEntrevista = pDesarrollo.IdEntrevista
            };

            // return await _desarrolloLenguajeDAL.ModificarAsync(entidad);
            return 1;
        }

        public async Task<int> EliminarAsync(DesarrolloLenguajeMantDTO pDesarrollo)
        {
            // return await _desarrolloLenguajeDAL.EliminarAsync(pDesarrollo.Id);
            return 1;
        }

        public async Task<DesarrolloLenguajeMantDTO> ObtenerPorIdAsync(DesarrolloLenguajeMantDTO pDesarrollo)
        {
            return pDesarrollo;
        }

        public async Task<PaginacionOutputDTO<List<DesarrolloLenguajeMantDTO>>> BuscarAsync(DesarrolloLenguajeBuscarDTO pDesarrollo)
        {
            return new PaginacionOutputDTO<List<DesarrolloLenguajeMantDTO>>();
        }
    }
}
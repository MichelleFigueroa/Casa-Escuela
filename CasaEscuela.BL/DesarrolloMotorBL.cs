using CasaEscuela.BL.Interfaces;
using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.DesarrolloMotorDTOs;
using CasaEscuela.EN;

namespace CasaEscuela.BL
{
    public class DesarrolloMotorBL : IDesarrolloMotorBL
    {
        public async Task<int> CrearAsync(DesarrolloMotorMantDTO pDesarrollo)
        {
            DesarrolloMotorEN entidad = new DesarrolloMotorEN
            {
                Descripcion = pDesarrollo.Descripcion
            };

            // return await _desarrolloMotorDAL.CrearAsync(entidad);
            return 1;
        }

        public async Task<int> ModificarAsync(DesarrolloMotorMantDTO pDesarrollo)
        {
            DesarrolloMotorEN entidad = new DesarrolloMotorEN
            {
                Id = pDesarrollo.Id,
                Descripcion = pDesarrollo.Descripcion
            };

            // return await _desarrolloMotorDAL.ModificarAsync(entidad);
            return 1;
        }

        public async Task<int> EliminarAsync(DesarrolloMotorMantDTO pDesarrollo)
        {
            // return await _desarrolloMotorDAL.EliminarAsync(pDesarrollo.Id);
            return 1;
        }

        public async Task<DesarrolloMotorMantDTO> ObtenerPorIdAsync(DesarrolloMotorMantDTO pDesarrollo)
        {
            return pDesarrollo;
        }

        public async Task<PaginacionOutputDTO<List<DesarrolloMotorMantDTO>>> BuscarAsync(DesarrolloMotorBuscarDTO pDesarrollo)
        {
            return new PaginacionOutputDTO<List<DesarrolloMotorMantDTO>>();
        }
    }
}
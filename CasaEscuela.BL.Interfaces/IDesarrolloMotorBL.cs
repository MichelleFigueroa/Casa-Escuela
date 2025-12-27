using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.DesarrolloMotorDTOs;

namespace CasaEscuela.BL.Interfaces
{
    public interface IDesarrolloMotorBL
    {
        Task<int> CrearAsync(DesarrolloMotorMantDTO pDesarrollo);
        Task<int> ModificarAsync(DesarrolloMotorMantDTO pDesarrollo);
        Task<int> EliminarAsync(DesarrolloMotorMantDTO pDesarrollo);
        Task<DesarrolloMotorMantDTO> ObtenerPorIdAsync(DesarrolloMotorMantDTO pDesarrollo);
        Task<PaginacionOutputDTO<List<DesarrolloMotorMantDTO>>> BuscarAsync(DesarrolloMotorBuscarDTO pDesarrollo);
    }
}
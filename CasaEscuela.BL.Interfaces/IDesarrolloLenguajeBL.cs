using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.DesarrolloLenguajeDTOs;

namespace CasaEscuela.BL.Interfaces
{
    public interface IDesarrolloLenguajeBL
    {
        Task<int> CrearAsync(DesarrolloLenguajeMantDTO pDesarrollo);
        Task<int> ModificarAsync(DesarrolloLenguajeMantDTO pDesarrollo);
        Task<int> EliminarAsync(DesarrolloLenguajeMantDTO pDesarrollo);
        Task<DesarrolloLenguajeMantDTO> ObtenerPorIdAsync(DesarrolloLenguajeMantDTO pDesarrollo);
        Task<PaginacionOutputDTO<List<DesarrolloLenguajeMantDTO>>> BuscarAsync(DesarrolloLenguajeBuscarDTO pDesarrollo);
    }
}
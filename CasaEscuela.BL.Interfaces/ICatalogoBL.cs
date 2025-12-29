using CasaEscuela.BL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasaEscuela.BL.Interfaces
{
    public interface ICatalogoBL
    {
        Task<List<ItemCatalogoDTO>> ObtenerNivelesEscolaresAsync();
        Task<List<ItemCatalogoDTO>> ObtenerTiposFamiliaAsync();
        Task<List<ItemCatalogoDTO>> ObtenerViveConAsync();
        Task<List<ItemCatalogoDTO>> ObtenerTiposPartoAsync();
        Task<List<ItemCatalogoDTO>> ObtenerTiposPreceptoriaAsync();
    }
}

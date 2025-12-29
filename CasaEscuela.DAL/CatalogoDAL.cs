using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.Interfaces;
using CasaEscuela.EN;
using CasaEscuela.EN.CatologosEN;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaEscuela.DAL
{
    public class CatalogoDAL : ICatalogoBL
    {
        private readonly CasaEscuelaDBContext dbContext;

        public CatalogoDAL(CasaEscuelaDBContext context)
        {
            dbContext = context;
        }

        public async Task<List<ItemCatalogoDTO>> ObtenerNivelesEscolaresAsync()
        {
            return await dbContext.CatNivelEscolar
                .Select(x => new ItemCatalogoDTO { Id = x.Id, Descripcion = x.Descripcion })
                .ToListAsync();
        }

        public async Task<List<ItemCatalogoDTO>> ObtenerTiposFamiliaAsync()
        {
            return await dbContext.CatTipoFamilia
                .Select(x => new ItemCatalogoDTO { Id = x.Id, Descripcion = x.Descripcion })
                .ToListAsync();
        }

        public async Task<List<ItemCatalogoDTO>> ObtenerViveConAsync()
        {
            return await dbContext.CatViveCon
                .Select(x => new ItemCatalogoDTO { Id = x.Id, Descripcion = x.Descripcion })
                .ToListAsync();
        }

        public async Task<List<ItemCatalogoDTO>> ObtenerTiposPartoAsync()
        {
            return await dbContext.CatTipoParto
                .Select(x => new ItemCatalogoDTO { Id = x.Id, Descripcion = x.Descripcion })
                .ToListAsync();
        }

        public async Task<List<ItemCatalogoDTO>> ObtenerTiposPreceptoriaAsync()
        {
            return await dbContext.CatTipoPreceptoria
                .Select(x => new ItemCatalogoDTO { Id = x.Id, Descripcion = x.Descripcion })
                .ToListAsync();
        }
    }
}

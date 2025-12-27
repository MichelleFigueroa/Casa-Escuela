using CasaEscuela.BL.Interfaces;
using CasaEscuela.BL.DTOs;
using CasaEscuela.BL.DTOs.EntrevistaDTOs;
using CasaEscuela.EN;

namespace CasaEscuela.BL
{
    public class EntrevistaBL : IEntrevistaBL
    {
        public async Task<int> CrearAsync(EntrevistaMantDTO pEntrevista)
        {
            EntrevistaEN entidad = new EntrevistaEN
            {
                obbservaciones = pEntrevista.obbservaciones,
                fecha_entrevista = pEntrevista.fecha_entrevista,
                evaluciones_previa = pEntrevista.evaluciones_previa,
                informacion_adicional = pEntrevista.informacion_adicional,
                IdEstudiante = pEntrevista.IdEstudiante,
                IdFamilia = pEntrevista.IdFamilia,
                IdParto = pEntrevista.IdParto,
                IdSalud = pEntrevista.IdSalud
            };

            return 1;
        }

        public async Task<int> ModificarAsync(EntrevistaMantDTO pEntrevista)
        {
            EntrevistaEN entidad = new EntrevistaEN
            {
                Id = pEntrevista.Id,
                obbservaciones = pEntrevista.obbservaciones,
                fecha_entrevista = pEntrevista.fecha_entrevista,
                evaluciones_previa = pEntrevista.evaluciones_previa,
                informacion_adicional = pEntrevista.informacion_adicional,
                IdEstudiante = pEntrevista.IdEstudiante,
                IdFamilia = pEntrevista.IdFamilia,
                IdParto = pEntrevista.IdParto,
                IdSalud = pEntrevista.IdSalud
            };

            return 1;
        }

        public async Task<int> EliminarAsync(EntrevistaMantDTO pEntrevista)
        {
            return 1;
        }

        public async Task<EntrevistaMantDTO> ObtenerPorIdAsync(EntrevistaMantDTO pEntrevista)
        {
            return pEntrevista;
        }

        public async Task<PaginacionOutputDTO<List<EntrevistaMantDTO>>> BuscarAsync(EntrevistaBuscarDTO pEntrevista)
        {
            return new PaginacionOutputDTO<List<EntrevistaMantDTO>>();
        }
    }
}
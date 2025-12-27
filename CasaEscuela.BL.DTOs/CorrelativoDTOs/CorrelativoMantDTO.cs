using System;

namespace CasaEscuela.BL.DTOs.CorrelativoDTOs
{
    public class CorrelativoMantDTO
    {
        public byte Id { get; set; }
        public byte Tipo { get; set; }
        public int Valor { get; set; }
        public string? AliasInicio { get; set; }
        public DateTime? UltFechaActualizacion { get; set; }
        public string? AliasFin { get; set; }
        public int? IdSucursal { get; set; }
    }
}
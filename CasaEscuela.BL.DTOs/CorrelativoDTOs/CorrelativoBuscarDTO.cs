namespace CasaEscuela.BL.DTOs.CorrelativoDTOs
{
    public class CorrelativoBuscarDTO
    {
        public byte? Id { get; set; }
        public byte? Tipo { get; set; }
        public int? IdSucursal { get; set; }

        public int Pagina { get; set; }
        public int RegistrosPorPagina { get; set; }
    }
}
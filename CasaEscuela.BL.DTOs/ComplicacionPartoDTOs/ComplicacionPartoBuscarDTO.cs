namespace CasaEscuela.BL.DTOs.ComplicacionPartoDTOs
{
    public class ComplicacionPartoBuscarDTO
    {
        public int? Id { get; set; }
        public string tipo_parto { get; set; }
        public int? IdParto { get; set; }

        public int Pagina { get; set; }
        public int RegistrosPorPagina { get; set; }
    }
}
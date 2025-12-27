namespace CasaEscuela.BL.DTOs.DesarrolloLenguajeDTOs
{
    public class DesarrolloLenguajeBuscarDTO
    {
        public int? Id { get; set; }
        public string Descripcion { get; set; }
        public int? IdEntrevista { get; set; }

        public int Pagina { get; set; }
        public int RegistrosPorPagina { get; set; }
    }
}
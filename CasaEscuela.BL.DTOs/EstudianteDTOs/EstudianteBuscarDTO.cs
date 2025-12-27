namespace CasaEscuela.BL.DTOs.EstudianteDTOs
{
    public class EstudianteBuscarDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public int? grado { get; set; }
        public bool? estado { get; set; }

        public int Pagina { get; set; }
        public int RegistrosPorPagina { get; set; }
    }
}
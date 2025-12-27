namespace CasaEscuela.BL.DTOs.DesarrolloMotorDTOs
{
    public class DesarrolloMotorBuscarDTO
    {
        public int? Id { get; set; }
        public string Descripcion { get; set; }

        public int Pagina { get; set; }
        public int RegistrosPorPagina { get; set; }
    }
}
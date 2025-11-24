namespace CasaEscuela.AppWebMVC.Models.Menu
{
    public class MenuDTO
    {
        public string Nombre { get; set; }
        public string Icono { get; set; }
        public List<SubMenuDTO> SubMenu { get; set; } = new();
    }
}

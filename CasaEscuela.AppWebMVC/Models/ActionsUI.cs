namespace CasaEscuela.AppWebMVC.Models
{
    public class ActionsUI
    {
        public int Accion { get; set; }
        public bool EsValidAction() 
        {
            if (Accion == (int)ActionsUI_Enums.NUEVO ||
               Accion == (int)ActionsUI_Enums.IMPRIMIR ||
                Accion == (int)ActionsUI_Enums.MODIFICAR ||
                Accion == (int)ActionsUI_Enums.ELIMINAR ||
                Accion == (int)ActionsUI_Enums.VER ||
                Accion == (int)ActionsUI_Enums.ABRIR_TURNO ||
                Accion == (int)ActionsUI_Enums.CERRAR_TURNO ||
                Accion == (int)ActionsUI_Enums.EN_REVISION)
            {
                return true;
            }
            else
                return false;
        }
        public string ObtenerAccion()
        {
            if (Accion == (int)ActionsUI_Enums.NUEVO)
                return "Create";
            if (Accion == (int)ActionsUI_Enums.IMPRIMIR)
                return "Imprimir";
            else if (Accion == (int)ActionsUI_Enums.MODIFICAR)
                return "Edit";
            else if (Accion == (int)ActionsUI_Enums.ELIMINAR)
                return "Delete";
            else if (Accion == (int)ActionsUI_Enums.VER)
                return "Detail";
            else if (Accion == (int)ActionsUI_Enums.ABRIR_TURNO)
                return "AbrirTurno";
            else if (Accion == (int)ActionsUI_Enums.CERRAR_TURNO)
                return "CerrarTurno";
            else if (Accion == (int)ActionsUI_Enums.EN_REVISION)
                return "RevisarTurno";
            return "";

        }
        public string ObtenerTitulo(string pTexto)
        {
            if (Accion == (int)ActionsUI_Enums.NUEVO)
                return "Crear "+ pTexto;
            if (Accion == (int)ActionsUI_Enums.IMPRIMIR)
                return "Imprimir " + pTexto;
            else if (Accion == (int)ActionsUI_Enums.MODIFICAR)
                return "Modificar " + pTexto;
            else if (Accion == (int)ActionsUI_Enums.ELIMINAR)
                return "Eliminar " + pTexto;
            else if (Accion == (int)ActionsUI_Enums.VER)
                return "Ver " + pTexto;
            else if (Accion == (int)ActionsUI_Enums.ABRIR_TURNO)
                return "Abrir " + pTexto;
            else if (Accion == (int)ActionsUI_Enums.CERRAR_TURNO)
                return "Cerrar " + pTexto;
            else if (Accion == (int)ActionsUI_Enums.EN_REVISION)
                return "Revision " + pTexto;
            return "";
        }
        public string ObtenerNombreBoton()
        {
            if (Accion == (int)ActionsUI_Enums.NUEVO)
                return "Crear";
            if (Accion == (int)ActionsUI_Enums.IMPRIMIR)
                return "Imprimir";
            else if (Accion == (int)ActionsUI_Enums.MODIFICAR)
                return "Modificar";
            else if (Accion == (int)ActionsUI_Enums.ELIMINAR)
                return "Eliminar";
            else if (Accion == (int)ActionsUI_Enums.VER)
                return "Ir a modificar";
            else if (Accion == (int)ActionsUI_Enums.ABRIR_TURNO)
                return "Abrir";
            else if (Accion == (int)ActionsUI_Enums.CERRAR_TURNO)
                return "Cerrar";
            else if (Accion == (int)ActionsUI_Enums.EN_REVISION)
                return "Guardar Revisión";
            return "";

        }
        public string ObtenerAccionJs()
        {
            if (Accion == (int)ActionsUI_Enums.NUEVO)
                return "crear";
            if (Accion == (int)ActionsUI_Enums.IMPRIMIR)
                return "imprimir";
            else if (Accion == (int)ActionsUI_Enums.MODIFICAR)
                return "modificar";
            else if (Accion == (int)ActionsUI_Enums.ELIMINAR)
                return "eliminar";
            else if (Accion == (int)ActionsUI_Enums.VER)
                return "ver";
            else if (Accion == (int)ActionsUI_Enums.ABRIR_TURNO)
                return "abrirTurno";
            else if (Accion == (int)ActionsUI_Enums.CERRAR_TURNO)
                return "cerrarTurno";
            else if (Accion == (int)ActionsUI_Enums.EN_REVISION)
                return "enRevision";
            return "";

        }
        public bool SiTraerDatos()
        {
            if (Accion == (int)ActionsUI_Enums.NUEVO)
                return false;
            else
                return true;
        }
    }
    public enum ActionsUI_Enums
    {
        NUEVO=1,
        MODIFICAR=2,
        ELIMINAR=3,
        VER = 4,
        IMPRIMIR = 5,
        ACTIVAR = 6,
        ANULAR = 7,
        ABRIR_TURNO = 8,
        CERRAR_TURNO = 9,
        EN_REVISION = 10,
        CONSULTAR = 11,
        
    }
}

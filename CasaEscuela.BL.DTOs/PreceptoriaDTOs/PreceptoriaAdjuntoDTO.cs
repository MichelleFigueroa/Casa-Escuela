using System;

namespace CasaEscuela.BL.DTOs.PreceptoriaDTOs
{
    public class PreceptoriaAdjuntoDTO
    {
        public int Id { get; set; }
        public int PreceptoriaId { get; set; }
        public string NombreArchivo { get; set; }
        public string ContentType { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    public class PreceptoriaAdjuntoDownloadDTO : PreceptoriaAdjuntoDTO
    {
        public byte[] Contenido { get; set; }
    }
}

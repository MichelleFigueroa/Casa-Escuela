using System;

namespace CasaEscuela.BL.DTOs
{
    public class AnamnesisAdjuntoDTO
    {
        public int Id { get; set; }
        public int AnamnesisId { get; set; }
        public string NombreArchivo { get; set; }
        public string ContentType { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    public class AnamnesisAdjuntoDownloadDTO : AnamnesisAdjuntoDTO
    {
        public byte[] Contenido { get; set; }
    }
}

using CasaEscuela.EN.CatologosEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.EN
{
    public class AnamnesisEN
    {
        public int IdAnamnesis { get; set; }
        public int IdEstudiante { get; set; }

        public int? ViveConId { get; set; }
        public int? TipoFamiliaId { get; set; }
        public int? TipoPartoId { get; set; }

        public bool EmbarazoControlado { get; set; }
        public string? ComplicacionesEmbarazo { get; set; }

        public string? CondicionesSalud { get; set; }
        public string? DesarrolloLenguaje { get; set; }
        public string? DesarrolloMotor { get; set; }

        public string? SituacionFamiliar { get; set; }
        public string? Observaciones { get; set; }

        public DateTime? FechaEntrevista { get; set; }
        public string? Entrevistador { get; set; }

        public EstudianteEN Estudiante { get; set; }
        public CatViveConEN ViveCon { get; set; }
        public CatTipoFamiliaEN TipoFamilia { get; set; }
        public CatTipoPartoEN TipoParto { get; set; }
    }
}

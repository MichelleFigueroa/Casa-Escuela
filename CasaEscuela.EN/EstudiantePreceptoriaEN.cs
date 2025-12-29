using CasaEscuela.EN.CatologosEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.EN
{
    public class EstudiantePreceptoriaEN
    {
        public int Id { get; set; }
        public int IdEstudiante { get; set; }
        public int? TipoPreceptoriaId { get; set; }

        public DateTime Fecha { get; set; }

        public string ProcesosTrabajados { get; set; }
        public string ProcesosDificultad { get; set; }
        public string MetasSiguientes { get; set; }
        public string Recomendaciones { get; set; }

        public EstadoPreceptoriaEnum EstadoPreceptoria { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        public EstudianteEN Estudiante { get; set; }
        public CatTipoPreceptoriaEN TipoPreceptoria { get; set; }
    }
}

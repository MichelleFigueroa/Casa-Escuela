using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.EN
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        
        public DateTime fecha_nacimiento { get; set; }

        public int edad {  get; set; }

        public string centro_escolar { get; set; }

        public int grado { get; set; }

        public Boolean estado { get; set; }
    }
}

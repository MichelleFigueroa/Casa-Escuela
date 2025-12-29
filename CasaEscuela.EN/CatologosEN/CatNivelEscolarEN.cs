using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.EN.CatologosEN
{
    public class CatNivelEscolarEN
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public List<EstudianteEN> Estudiantes { get; set; }
    }
}

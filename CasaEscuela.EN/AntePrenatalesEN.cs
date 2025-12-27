using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.EN
{
    public class AntePrenatalesEN
    {
        public int Id { get; set; }

        public DateTime duracion_Emb {  get; set; }

        public string medicamentos { get; set; }

        public string emfermedades { get; set; }

        public string caidas { get; set; }

        public string controles_medic { get; set; }

        public string estado_emo { get; set; }

        public string estado_nutri { get; set; }

        public string info_adicionales { get; set; }
    }
}

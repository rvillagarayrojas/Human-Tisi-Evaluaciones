using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.A_Seleccion
{
    public class E_RespuestaPreguntaFile
    {
        public decimal NU_ID_RESPUESTA_PREGUNTA_FILE { get; set; }
        public decimal NU_ID_PRUEBA_CANDIDATO { get; set; }
        public decimal NU_ID_PRUEBA_PARTE { get; set; }
        public decimal NU_ID_PREGUNTA { get; set; }
        public decimal NUM_NOTA { get; set; }
        public String VC_COMENTARIOS { get; set; }
        public String VC_NOMFILE_RESPUESTA { get; set; }
        public String TIPO_REVISION { get; set; }
    }
}
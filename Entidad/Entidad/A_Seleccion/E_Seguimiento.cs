using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.A_Seleccion
{
    [Serializable]
    public class E_Seguimiento
    {
        public decimal ID { get; set; }
        public string NU_ID_USUARIO { get; set; }
        public string VC_COD_USUARIO { get; set; }
        public string VC_NOM_PROCESO { get; set; }
        public string VC_NOM_ACCION { get; set; }
        public string VC_DESC_ACCION { get; set; }
        public string VC_URL_REFERRER_PAGINA { get; set; }
        public string VC_URL_PAGINA { get; set; }
        public DateTime? DT_FECHA_ACCION { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Siscom.Entity.Global
{
    public class TipoCuentaBE
    {
       public int?      nu_id_cuenta    { get; set; }
       public string    vc_desc_cuenta  { get; set; }
       public string    ch_status       { get; set; }
       public string    vc_usr_reg      { get; set; }
       public DateTime? dt_fec_reg      { get; set; }

       public string    vc_criterio     { get; set; }
       public DateTime? dt_fecha_inicio { get; set; }
       public DateTime? dt_fecha_fin    { get; set; }
    }
}

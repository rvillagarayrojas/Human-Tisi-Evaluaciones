using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Siscom.Entity.Global
{
    public class TipoSubCuentaBE
    {
        public int?         nu_id_cuenta        { get; set; }
        public int?         nu_id_subcuenta     { get; set; }
        public string       vc_desc_sub_cuenta  { get; set; }
        public string       ch_status           { get; set; }
        public string       vc_usr_reg          { get; set; }
        public DateTime?    dt_fec_reg          { get; set; }

        public string       vc_criterio         { get; set; }
    }
}

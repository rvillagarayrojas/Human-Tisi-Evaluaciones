using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Siscom.Entity.Global
{
     public class TipoPuestoBE
    {
        public int?        nu_id_puesto     { get; set; }
        public int?        nu_id_subcuenta  { get; set; }
        public int?        nu_id_cuenta     { get; set; }
        public string      vc_desc_puesto   { get; set; }
        public string      ch_status        { get; set; }
        public string      ch_flag          { get; set; }
        public string      vc_usr_reg       { get; set; }
        public DateTime?   dt_fec_reg       { get; set; }

    }
}

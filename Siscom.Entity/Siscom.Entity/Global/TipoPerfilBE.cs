using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Siscom.Entity.Global
{
    public class TipoPerfilBE
    {
        public int?         nu_id_perfil    { get; set; }
        public string       vc_desc_perfil  { get; set; }
        public string       ch_status       { get; set; }
        public string       vc_usr_reg      { get; set; }
        public DateTime?    dt_fec_reg      { get; set; }
    }
}

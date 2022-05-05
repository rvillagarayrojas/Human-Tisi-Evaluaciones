using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siscom.Entity.Global
{
    [Serializable]
    public partial class TipoPruebaBE
    {
        public decimal? nu_id_tipo_prueba   { get; set; }
        public decimal? nu_id_nivel_prueba  { get; set; }
        public string   vc_cod_tipo_prueba  { get; set; }
        public string   vc_desc_tipo_prueba { get; set; }
    }
}

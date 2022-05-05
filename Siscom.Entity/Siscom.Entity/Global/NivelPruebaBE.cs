using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siscom.Entity.Global
{
    [Serializable]
    public partial class NivelPruebaBE
    {
        public decimal?     nu_id_nivel_prueba      { get; set; }
        public string       vc_cod_nivel_prueba     { get; set; }
        public string       vc_desc_nivel_prueba    { get; set; }
        public string       vc_criterio             { get; set; }
    }
}

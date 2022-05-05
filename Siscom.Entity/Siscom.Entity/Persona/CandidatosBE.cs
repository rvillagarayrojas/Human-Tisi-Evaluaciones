using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Siscom.Entity.Global;

namespace Siscom.Entity.Persona
{
    [Serializable]
    public partial class CandidatoBE
    {
        public string   mes_reg { get; set; }

        public decimal? cant_candidatos { get; set; }
        public decimal? anno_reg        { get; set; }
        public string   vc_desc_prueba  { get; set; }
        public decimal? cant_pruebas    { get; set; }

        public decimal? nu_id_cuenta    { get; set; }
        public decimal? nu_id_subcuenta { get; set; }
        public int      opcion          { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siscom.Entity.Global
{
    [Serializable]
    public partial class SubCuentaBE
    {
        public decimal? nu_id_cuenta        { get; set; }
        public decimal? nu_id_subcuenta     { get; set; }
        public string   vc_desc_sub_cuenta  { get; set; }
        public string   vc_desc_cuenta      { get; set; }

        //PERSONA
        public decimal? nu_id_persona               { get; set; }
        public string   vc_nombres                  { get; set; }
        public string   vc_apellidos                { get; set; }
        public string   ch_sexo                     { get; set; }
        public string   vc_direccion_email_usuario  { get; set; }
        public string   vc_doc_identi               { get; set; }
        public string   vc_cargo                    { get; set; }
        public string   vc_telefono_usuario         { get; set; }

        //USUARIO

        public decimal? nu_id_usuario   { get; set; }
        public string   vc_cod_usuario  { get; set; }
        public string   vc_password     { get; set; }
        public decimal? nu_id_perfil    { get; set; }
        public decimal? nu_id_puesto    { get; set; }

		public string       vc_usr_reg          { get; set; }
		public DateTime?    dt_fec_reg          { get; set; }
		public string       vc_usr_mod          { get; set; }
		public DateTime?    dt_fec_mod          { get; set; }
        public string       ch_status           { get; set; }

        public string       vc_criterio         { get; set; }

        public decimal? opcion { get; set; }

        public decimal? nu_id_contacto { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Siscom.Entity.Global
{
    [Serializable]
    public partial class UsuarioBE
    {
        public string       vc_cod_usuario          { get; set; }
        public string       vc_password             { get; set; }
        public int?         nu_id_cuenta            { get; set; }
        public int?         nu_id_perfil            { get; set; }
        public int?         nu_id_persona           { get; set; }
        public string       vc_nombres              { get; set; }
        public string       ch_sexo                 { get; set; }
        public string       vc_direccion_email      { get; set; }
        public DateTime?    dt_fecha_inicio         { get; set; }
        public DateTime?    dt_fecha_fin            { get; set; }
        public string       ch_status               { get; set; }
        public string       vc_desc_cuenta          { get; set; }
        public int?         nu_id_subcuenta         { get; set; }
        public string       vc_desc_sub_cuenta      { get; set; }
        public int?         nu_id_puesto            { get; set; }
        public string       vc_desc_puesto          { get; set; }
        public string       ch_contrato             { get; set; }
        public string       vc_usr_mod              { get; set; }
        public DateTime?    dt_fec_mod              { get; set; }
        public string       vc_imagen_personalizar  { get; set; }

        public decimal? opcion                      { get; set; }
        public decimal? cant_puesto                 { get; set; }
        public decimal? cant_candidatos             { get; set; }
    }
}

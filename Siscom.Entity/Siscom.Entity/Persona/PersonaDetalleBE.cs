using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Siscom.Entity.Global;

namespace Siscom.Entity.Persona
{
    [Serializable]
    public partial class PersonaDetalleBE
    {
        public string       vc_nombres          { get; set; }
        public string       vc_apellidos        { get; set; }
        public string       ch_sexo             { get; set; }
        public string       vc_direccion_email  { get; set; }
        public string       vc_doc_identi       { get; set; }
        public string       vc_cargo            { get; set; }
        public string       vc_telefono         { get; set; }
        public string       vc_usr_reg          { get; set; }
        public DateTime?    dt_fec_reg          { get; set; }
        public string       vc_usr_mod          { get; set; }
        public DateTime?    dt_fec_mod          { get; set; }
        public string       ch_status           { get; set; }

        public string       vc_criterio         { get; set; }
        //campos para la tabla usuario

        public string vc_cod_usuario            { get; set; }
        public string vc_password               { get; set; }
        
        public decimal? nu_id_perfil            { get; set; }
        public decimal? nu_id_cuenta            { get; set; }
        public decimal? nu_id_subcuenta         { get; set; }
        public decimal? nu_id_puesto            { get; set; }


        public int opcion { get; set; }

        public int opcion1 { get; set; }

        public DateTime? dt_fec_inicio { get; set; }
        public DateTime? dt_fec_fin { get; set; }

        public string vc_direccion_email_salida { get; set; }
        public string vc_datos_usuario_salida { get; set; }
        public string vc_cod_usuario_salida { get; set; }
        public string vc_password_salida { get; set; }
        public string vc_dato_consul_salida { get; set; }
        public string vc_correo_consul_salida { get; set; }


        

    }
}

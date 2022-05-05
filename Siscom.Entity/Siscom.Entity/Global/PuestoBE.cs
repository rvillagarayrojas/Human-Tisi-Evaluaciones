using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siscom.Entity.Global
{
    [Serializable]
    public partial class PuestoBE
    {
        public decimal?     nu_id_puesto    { get; set; }
        public decimal?     nu_id_subcuenta { get; set; }
        public decimal?     nu_id_cuenta    { get; set; }
        public string       vc_desc_puesto  { get; set; }
        public string       vc_usr_reg      { get; set; }
        public DateTime?    dt_fec_reg      { get; set; }
        public string       vc_usr_mod      { get; set; }
        public DateTime?    dt_fec_mod      { get; set; }
        public string       ch_status       { get; set; }
        public string       ch_flag         { get; set; }
        public string       ch_estado       { get; set; }
        public string       ch_brecha       { get; set; }
        public string       ch_ficha        { get; set; }
        public decimal?     nu_pruebas      { get; set; }
        public decimal?     opcion          { get; set; }
        public decimal?      nu_dias_vigencia   { get; set; }

        public string       vc_criterio         { get; set; }
        public string       vc_desc_cuenta      { get; set; }
        public string       vc_desc_sub_cuenta  { get; set; }
        public string       vc_desc_observacion { get; set; }

        public string       archivo { get; set; }



        //campos para la lista
        public decimal? nu_id_nivel_prueba      { get; set; }
        public string   vc_cod_nivel_prueba     { get; set; }
        public string   vc_desc_nivel_prueba    { get; set; }
        public decimal? nu_id_tipo_prueba       { get; set; }
        public string   vc_cod_tipo_prueba      { get; set; }
        public string   vc_desc_tipo_prueba     { get; set; }
        public string   vc_desc_clasificacion   { get; set; }
        public decimal? nu_id_clasificacion     { get; set; }

        //pruebas
        public decimal? nu_id_prueba    { get; set; }
        public string   vc_cod_prueba   { get; set; }
        public string   vc_desc_prueba  { get; set; }
        public decimal? nu_min_tiempo   { get; set; }
        public decimal? nu_preguntas    { get; set; }
        public string   PUBLICA         { get; set; }
        public decimal? nu_id_prueba_parte { get; set; }

        //PERSONA
        public string vc_nombres { get; set; }
        public string vc_apellidos { get; set; }
        public string ch_sexo { get; set; }
        public string vc_direccion_email_usuario { get; set; }
        public string vc_doc_identi { get; set; }
        public string vc_cargo { get; set; }
        public string vc_telefono_usuario { get; set; }

        //USUARIO

        public decimal? nu_id_usuario { get; set; }
        public string vc_cod_usuario { get; set; }
        public string vc_password { get; set; }
        public decimal? nu_id_perfil { get; set; }

        //RETURN ERROR
        public decimal nu_cod_error { get; set; }
        public string vc_desc_error { get; set; }
        
        public List<PuestoDetalleBE> PruebaDetalle     { get; set; }
        public List<PuestoDetalleBE> PersonaDetalle { get; set; }
        public List<PuestoDetalleBE> UsuarioDetalle { get; set; }


    }
}

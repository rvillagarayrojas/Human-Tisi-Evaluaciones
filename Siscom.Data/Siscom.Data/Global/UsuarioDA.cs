
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.Common;
using Siscom.Entity.Global;
using Siscom.Library.Common;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Siscom.Data.Interface;

namespace Siscom.Data.Global
{
    public class UsuarioDA : BaseAbstractDA, IBaseBA<UsuarioBE>
    {
        public UsuarioDA()
            : base()
        { 
        }
        public IList<UsuarioBE> List(UsuarioBE oItem)
        {
            try
            {
                DbCommand cmd = null;
                {
                    cmd = db.GetStoredProcCommand("SP_PRC_LOGEO");
                    db.AddInParameter(cmd, "@VC_COD_USUARIO", DbType.String, oItem.vc_cod_usuario);
                    db.AddInParameter(cmd, "@VC_PASSWORD", DbType.String, oItem.vc_password);
                   
                }
                using (IDataReader oR = db.ExecuteReader(cmd))
                {
                    return MapDataReaderToList(oR);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("UsuarioDA.List()", ex);
            }
        }

        private IList<UsuarioBE> MapDataReaderToList(IDataReader oR, int? nOpcion = null)
        {
            var lista = new List<UsuarioBE>();

            while (oR.Read())
            {
                lista.Add(MapDataReaderToEntity(oR, nOpcion));
            }

            return lista;
        }

        private UsuarioBE MapDataReaderToEntity(IDataReader oR, int? nOpcion = null)
        {
            var item = new UsuarioBE();

            item.nu_id_cuenta               = oR["NU_ID_CUENTA"].ToInt();
            item.vc_cod_usuario             = oR["VC_COD_USUARIO"].ToText();
            item.vc_password                = oR["VC_PASSWORD"].ToText();
            item.nu_id_perfil               = oR["NU_ID_PERFIL"].ToInt();
            item.nu_id_persona              = oR["NU_ID_PERSONA"].ToInt();
            item.vc_nombres                 = oR["VC_NOMBRES"].ToText();
            item.ch_sexo                    = oR["CH_SEXO"].ToText();
            item.vc_direccion_email         = oR["VC_DIRECCION_EMAIL"].ToText();
            item.dt_fecha_inicio            = oR["DT_FECHA_INICIO"].ToDateTime();
            item.dt_fecha_fin               = oR["DT_FECHA_FIN"].ToDateTime();
            item.ch_status                  = oR["CH_STATUS"].ToText();
            item.vc_desc_cuenta             = oR["VC_DESC_CUENTA"].ToText();
            item.nu_id_subcuenta            = oR["NU_ID_SUBCUENTA"].ToInt();
            item.vc_desc_sub_cuenta         = oR["VC_DESC_SUB_CUENTA"].ToText();
            item.ch_contrato                = oR["CH_CONTRATO"].ToText();
            item.vc_imagen_personalizar     = oR["VC_IMAGEN_PERSONALIZAR"].ToText();
            item.cant_candidatos            = oR["CANT_CANDIDATOS"].ToDecimal();
            item.cant_puesto                = oR["CANT_PUESTO"].ToDecimal();

            return item;
        }

        public int Update(UsuarioBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_UPD_ACUERDO_CONTRATO");

                db.AddInParameter(cmd, "@NU_ID_CUENTA", DbType.Decimal, oItem.nu_id_cuenta);
                db.AddInParameter(cmd, "@CH_CONTRATO", DbType.String, oItem.ch_contrato);
                db.AddInParameter(cmd, "@VC_USR_MOD", DbType.String, oItem.vc_usr_mod);
                db.AddInParameter(cmd, "@DT_FEC_MOD", DbType.DateTime, oItem.dt_fec_mod);

                return db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception("UsuarioDA.Update()", ex);
            }
        }
    }
}

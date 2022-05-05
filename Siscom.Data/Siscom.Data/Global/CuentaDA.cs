using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.Common;
using Siscom.Entity.Persona;
using Siscom.Library.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Siscom.Data.Interface;
using Siscom.Entity.Global;

namespace Siscom.Data.Global
{
    public class CuentaDA : BaseAbstractDA
    {
        public int Insert(CuentaBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_INS_CUENTA");

                db.AddInParameter(cmd, "@VC_DESC_CUENTA", DbType.String, oItem.vc_desc_cuenta);
                db.AddInParameter(cmd, "@VC_RUC", DbType.String, oItem.vc_ruc);
                db.AddInParameter(cmd, "@VC_DIRECCION", DbType.String, oItem.vc_direccion);
                db.AddInParameter(cmd, "@VC_DIRECCION_EMAIL", DbType.String, oItem.vc_direccion_email);
                db.AddInParameter(cmd, "@VC_TELEFONO", DbType.String, oItem.vc_telefono);
                db.AddInParameter(cmd, "@NU_TIEMPO_DURACION", DbType.Decimal, oItem.nu_tiempo_duracion);
                db.AddInParameter(cmd, "@DT_FECHA_INICIO", DbType.DateTime, oItem.dt_fecha_inicio.Value.ToShortDateString());
                db.AddInParameter(cmd, "@DT_FECHA_FIN", DbType.DateTime, oItem.dt_fecha_fin.Value.ToShortDateString());
                //Persona
                db.AddInParameter(cmd, "@VC_NOMBRES", DbType.String, oItem.vc_nombres);
                db.AddInParameter(cmd, "@VC_APELLIDOS", DbType.String, oItem.vc_apellidos);
                db.AddInParameter(cmd, "@CH_SEXO", DbType.String, oItem.ch_sexo);
                db.AddInParameter(cmd, "@VC_DIRECCION_EMAIL_USUARIO", DbType.String, oItem.vc_direccion_email_usuario);
                db.AddInParameter(cmd, "@VC_DOC_IDENTI", DbType.String, oItem.vc_doc_identi);
                db.AddInParameter(cmd, "@VC_CARGO", DbType.String, oItem.vc_cargo);
                db.AddInParameter(cmd, "@VC_TELEFONO_USUARIO", DbType.String, oItem.vc_telefono_usuario);
                //Usuario
                db.AddInParameter(cmd, "@VC_COD_USUARIO", DbType.String, oItem.vc_cod_usuario);
                db.AddInParameter(cmd, "@VC_PASSWORD", DbType.String, oItem.vc_password);
                db.AddInParameter(cmd, "@NU_ID_PERFIL", DbType.Decimal, oItem.nu_id_perfil);
                db.AddInParameter(cmd, "@NU_ID_PUESTO", DbType.Decimal, oItem.nu_id_puesto);
                db.AddInParameter(cmd, "@VC_CHATBOT_KEY", DbType.String, (oItem.vc_chatbot_key != null)?oItem.vc_chatbot_key.Trim():"");

                db.AddInParameter(cmd, "@VC_USR_REG", DbType.String, oItem.vc_usr_reg);
                db.AddInParameter(cmd, "@DT_FEC_REG", DbType.DateTime, oItem.dt_fec_reg.Value.ToShortDateString());
                db.AddInParameter(cmd, "@CH_STATUS", DbType.String, oItem.ch_status);

                return db.ExecuteNonQuery(cmd);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public int Update(CuentaBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_UPD_CUENTA");

                db.AddInParameter(cmd, "@NU_ID_CUENTA", DbType.Decimal, oItem.nu_id_cuenta);
                db.AddInParameter(cmd, "@NU_ID_PERSONA", DbType.Decimal, oItem.nu_id_persona);

                db.AddInParameter(cmd, "@VC_DESC_CUENTA", DbType.String, oItem.vc_desc_cuenta);
                db.AddInParameter(cmd, "@VC_RUC", DbType.String, oItem.vc_ruc);
                db.AddInParameter(cmd, "@VC_DIRECCION", DbType.String, oItem.vc_direccion);
                db.AddInParameter(cmd, "@VC_DIRECCION_EMAIL", DbType.String, oItem.vc_direccion_email);
                db.AddInParameter(cmd, "@VC_TELEFONO", DbType.String, oItem.vc_telefono);
                db.AddInParameter(cmd, "@NU_TIEMPO_DURACION", DbType.Decimal, oItem.nu_tiempo_duracion);
                db.AddInParameter(cmd, "@DT_FECHA_INICIO", DbType.DateTime, oItem.dt_fecha_inicio.Value.ToShortDateString());
                db.AddInParameter(cmd, "@DT_FECHA_FIN", DbType.DateTime, oItem.dt_fecha_fin.Value.ToShortDateString());
                //Persona
                db.AddInParameter(cmd, "@VC_NOMBRES", DbType.String, oItem.vc_nombres);
                db.AddInParameter(cmd, "@VC_APELLIDOS", DbType.String, oItem.vc_apellidos);
                db.AddInParameter(cmd, "@CH_SEXO", DbType.String, oItem.ch_sexo);
                db.AddInParameter(cmd, "@VC_DIRECCION_EMAIL_USUARIO", DbType.String, oItem.vc_direccion_email_usuario);
                db.AddInParameter(cmd, "@VC_DOC_IDENTI", DbType.String, oItem.vc_doc_identi);
                db.AddInParameter(cmd, "@VC_CARGO", DbType.String, oItem.vc_cargo);
                db.AddInParameter(cmd, "@VC_TELEFONO_USUARIO", DbType.String, oItem.vc_telefono_usuario);
                db.AddInParameter(cmd, "@VC_CHATBOT_KEY", DbType.String, (oItem.vc_chatbot_key != null)?oItem.vc_chatbot_key.Trim():"");

                db.AddInParameter(cmd, "@VC_USR_MOD", DbType.String, oItem.vc_usr_mod);
                db.AddInParameter(cmd, "@DT_FEC_MOD", DbType.DateTime, oItem.dt_fec_mod.Value.ToShortDateString());

                return db.ExecuteNonQuery(cmd);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public CuentaBE ChangePassword(CuentaBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_UPD_CUENTA_PASSWORD");

                db.AddInParameter(cmd, "@VC_COD_USUARIO", DbType.String, oItem.vc_cod_usuario);
                db.AddInParameter(cmd, "@vc_password_old", DbType.String, oItem.vc_password_old);
                db.AddInParameter(cmd, "@vc_password_new", DbType.String, oItem.vc_password_new);
                db.AddInParameter(cmd, "@vc_password_con", DbType.String, oItem.vc_password_con);
                db.AddOutParameter(cmd, "@vc_Mensaje_Out", DbType.String,500);

                db.ExecuteNonQuery(cmd);

                String vc_Mensaje_Out = db.GetParameterValue(cmd, "@vc_Mensaje_Out").ToString();

                oItem.vc_Mensaje_Out = vc_Mensaje_Out;
                return oItem;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public IList<CuentaBE> List(CuentaBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_SEL_CUENTAS");
                db.AddInParameter(cmd, "@NU_ID_CUENTA", DbType.String, oItem.nu_id_cuenta);
                db.AddInParameter(cmd, "@DT_FECHA_INICIO", DbType.DateTime, oItem.dt_fecha_inicio);
                db.AddInParameter(cmd, "@DT_FECHA_FIN", DbType.DateTime, oItem.dt_fecha_fin);
                db.AddInParameter(cmd, "@CH_STATUS", DbType.String, oItem.ch_status);

                using (IDataReader oR = db.ExecuteReader(cmd))
                {
                    return MapDataReaderToList(oR,1);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("CuentaDA.List()", ex);
            }
        }

        private IList<CuentaBE> MapDataReaderToList(IDataReader oR, decimal nOpcion)
        {
            var lista = new List<CuentaBE>();

            while (oR.Read())
            {
                lista.Add(MapDataReaderToEntity(oR, nOpcion));
            }

            return lista;
        }

        private CuentaBE MapDataReaderToEntity(IDataReader oR, decimal nOpcion)
        {
            var item = new CuentaBE();
            if (nOpcion == 1)
            {

                item.nu_id_cuenta       = oR["NU_ID_CUENTA"].ToDecimal();
               item.vc_desc_cuenta      = oR["VC_DESC_CUENTA"].ToText();
                item.vc_ruc             = oR["VC_RUC"].ToText();
                item.vc_direccion       = oR["VC_DIRECCION"].ToText();
                item.vc_direccion_email = oR["VC_DIRECCION_EMAIL"].ToText();
                item.vc_telefono        = oR["VC_TELEFONO"].ToText();
                item.nu_tiempo_duracion = oR["NU_TIEMPO_DURACION"].ToDecimal();
                item.dt_fecha_inicio    = oR["DT_FECHA_INICIO"].ToDateTime();
                item.dt_fecha_fin       = oR["DT_FECHA_FIN"].ToDateTime();
                item.vc_doc_identi      = oR["VC_DOC_IDENTI"].ToText();
                item.vc_nombres         = oR["VC_NOMBRES"].ToText();
                item.vc_apellidos       = oR["VC_APELLIDOS"].ToText();
                item.ch_status          = oR["CH_STATUS"].ToText();
            }
            if (nOpcion == 2)
            {
                item.nu_id_cuenta               = oR["NU_ID_CUENTA"].ToDecimal();
                item.nu_id_persona              = oR["NU_ID_PERSONA"].ToDecimal();
                item.nu_id_usuario              = oR["NU_ID_USUARIO"].ToDecimal();
                item.vc_desc_cuenta             = oR["VC_DESC_CUENTA"].ToText();
                item.vc_ruc                     = oR["VC_RUC"].ToText();
                item.vc_direccion               = oR["VC_DIRECCION"].ToText();
                item.vc_direccion_email         = oR["VC_DIRECCION_EMAIL"].ToText();
                item.vc_telefono                = oR["VC_TELEFONO"].ToText();
                item.nu_tiempo_duracion         = oR["NU_TIEMPO_DURACION"].ToDecimal();
                item.dt_fecha_inicio            = oR["DT_FECHA_INICIO"].ToDateTime();
                item.dt_fecha_fin               = oR["DT_FECHA_FIN"].ToDateTime();
                item.vc_doc_identi              = oR["VC_DOC_IDENTI"].ToText();
                item.vc_nombres                 = oR["VC_NOMBRES"].ToText();
                item.vc_apellidos               = oR["VC_APELLIDOS"].ToText();
                item.vc_cargo                   = oR["VC_CARGO"].ToText();
                item.vc_direccion_email_usuario = oR["VC_DIRECCION_EMAIL_USUARIO"].ToText();
                item.vc_telefono_usuario        = oR["VC_TELEFONO_USUARIO"].ToText();
                item.ch_sexo                    = oR["CH_SEXO"].ToText();
                item.vc_chatbot_key             = oR["VC_CHATBOT_KEY"].ToText();
            }
            return item;
        }

        public CuentaBE Get(CuentaBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_GET_CUENTA");
                db.AddInParameter(cmd, "@NU_ID_CUENTA", DbType.Decimal, oItem.nu_id_cuenta);

                using (IDataReader oR = db.ExecuteReader(cmd))
                {
                    while (oR.Read())
                    {
                        return MapDataReaderToEntity(oR,2);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("CuentaDA.Get()", ex);
            }

            return null;
        }
    }
}

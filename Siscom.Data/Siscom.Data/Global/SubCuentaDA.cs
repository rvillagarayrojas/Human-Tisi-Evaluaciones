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
    public class SubCuentaDA : BaseAbstractDA
    {
        public int Insert(SubCuentaBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_INS_SUBCUENTA");

                db.AddInParameter(cmd, "@NU_ID_CUENTA", DbType.Decimal, oItem.nu_id_cuenta);
                db.AddInParameter(cmd, "@VC_DESC_SUB_CUENTA", DbType.String, oItem.vc_desc_sub_cuenta);
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

                db.AddInParameter(cmd, "@VC_USR_REG", DbType.String, oItem.vc_usr_reg);
                db.AddInParameter(cmd, "@DT_FEC_REG", DbType.DateTime, oItem.dt_fec_reg);
                db.AddInParameter(cmd, "@CH_STATUS", DbType.String, oItem.ch_status);

                return db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public int Update(SubCuentaBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_UPD_SUCBCUENTA");

                db.AddInParameter(cmd, "@NU_ID_SUB_CUENTA", DbType.Decimal, oItem.nu_id_subcuenta);
                db.AddInParameter(cmd, "@NU_ID_CUENTA", DbType.Decimal, oItem.nu_id_cuenta);
                db.AddInParameter(cmd, "@NU_ID_PERSONA", DbType.Decimal, oItem.nu_id_persona);

                db.AddInParameter(cmd, "@VC_DESC_SUB_CUENTA", DbType.String, oItem.vc_desc_sub_cuenta);
                //Persona
                db.AddInParameter(cmd, "@VC_NOMBRES", DbType.String, oItem.vc_nombres);
                db.AddInParameter(cmd, "@VC_APELLIDOS", DbType.String, oItem.vc_apellidos);
                db.AddInParameter(cmd, "@CH_SEXO", DbType.String, oItem.ch_sexo);
                db.AddInParameter(cmd, "@VC_DIRECCION_EMAIL_USUARIO", DbType.String, oItem.vc_direccion_email_usuario);
                db.AddInParameter(cmd, "@VC_DOC_IDENTI", DbType.String, oItem.vc_doc_identi);
                db.AddInParameter(cmd, "@VC_CARGO", DbType.String, oItem.vc_cargo);
                db.AddInParameter(cmd, "@VC_TELEFONO_USUARIO", DbType.String, oItem.vc_telefono_usuario);

                db.AddInParameter(cmd, "@VC_USR_MOD", DbType.String, oItem.vc_usr_mod);
                db.AddInParameter(cmd, "@DT_FEC_MOD", DbType.DateTime, oItem.dt_fec_mod.Value.ToShortDateString());

                return db.ExecuteNonQuery(cmd);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public int UpdateUsuario(SubCuentaBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_UPD_ESTADO_SUBCUENTA");

                db.AddInParameter(cmd, "@NU_ID_USUARIO", DbType.Decimal, oItem.nu_id_usuario);
                db.AddInParameter(cmd, "@VC_USR_MOD", DbType.String, oItem.vc_usr_mod);
                db.AddInParameter(cmd, "@DT_FEC_MOD", DbType.DateTime, oItem.dt_fec_mod);

                return db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception("SubCuentaDA.UpdateUsuario()", ex);
            }
        }

        public IList<SubCuentaBE> List(SubCuentaBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_SEL_SUBCUENTAS");
                db.AddInParameter(cmd, "@VC_CRITERIO", DbType.String, oItem.vc_criterio);
                db.AddInParameter(cmd, "@NU_ID_CUENTA", DbType.Decimal, oItem.nu_id_cuenta);
                db.AddInParameter(cmd, "@CH_STATUS", DbType.String, oItem.ch_status);
                using (IDataReader oR = db.ExecuteReader(cmd))
                {
                    return MapDataReaderToList(oR,1);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SubCuentaDA.List()", ex);
            }
        }

        private IList<SubCuentaBE> MapDataReaderToList(IDataReader oR, decimal nOpcion)
        {
            var lista = new List<SubCuentaBE>();

            while (oR.Read())
            {
                lista.Add(MapDataReaderToEntity(oR, nOpcion));
            }

            return lista;
        }

        private SubCuentaBE MapDataReaderToEntity(IDataReader oR, decimal nOpcion)
        {
            var item = new SubCuentaBE();
            if (nOpcion == 1)
            {
                item.nu_id_subcuenta        = oR["NU_ID_SUBCUENTA"].ToDecimal();
                item.nu_id_cuenta           = oR["NU_ID_CUENTA"].ToDecimal();
                item.nu_id_contacto         = oR["NU_ID_CONTACTO"].ToDecimal();
                item.vc_desc_cuenta         = oR["VC_DESC_CUENTA"].ToText();
                item.vc_desc_sub_cuenta     = oR["VC_DESC_SUB_CUENTA"].ToText();
                item.vc_doc_identi          = oR["VC_DOC_IDENTI"].ToText();
                item.vc_nombres             = oR["VC_NOMBRES"].ToText();
                item.vc_apellidos           = oR["VC_APELLIDOS"].ToText();
                item.ch_status              = oR["CH_STATUS"].ToText();
            }
            if (nOpcion == 2) 
            {
                item.nu_id_subcuenta                = oR["NU_ID_SUBCUENTA"].ToDecimal();
                item.nu_id_cuenta                   = oR["NU_ID_CUENTA"].ToDecimal();
                item.nu_id_persona                  = oR["NU_ID_PERSONA"].ToDecimal();
                item.nu_id_usuario                  = oR["NU_ID_USUARIO"].ToDecimal();
                item.vc_desc_cuenta                 = oR["VC_DESC_CUENTA"].ToText();
                item.vc_desc_sub_cuenta             = oR["VC_DESC_SUB_CUENTA"].ToText();
                item.vc_doc_identi                  = oR["VC_DOC_IDENTI"].ToText();
                item.vc_nombres                     = oR["VC_NOMBRES"].ToText();
                item.vc_apellidos                   = oR["VC_APELLIDOS"].ToText();
                item.vc_cargo                       = oR["VC_CARGO"].ToText();
                item.vc_direccion_email_usuario     = oR["VC_DIRECCION_EMAIL_USUARIO"].ToText();
                item.vc_telefono_usuario            = oR["VC_TELEFONO_USUARIO"].ToText();
                item.ch_sexo                        = oR["CH_SEXO"].ToText();
            }
            return item;
        }

        public SubCuentaBE Get(SubCuentaBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_GET_SUBCUENTA");
                db.AddInParameter(cmd, "@NU_ID_SUBCUENTA", DbType.Decimal, oItem.nu_id_subcuenta);

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
                throw new Exception("SubCuentaDA.Get()", ex);
            }

            return null;
        }


    }
}

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
    public class PuestoDA : BaseAbstractDA
    {
        public int Insert(PuestoBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_INS_PUESTO");

                db.AddOutParameter(cmd, "@NU_ID_PUESTO_SALIDA", DbType.Decimal, 10);

                db.AddInParameter(cmd, "@NU_ID_SUBCUENTA", DbType.Decimal, oItem.nu_id_subcuenta);
                db.AddInParameter(cmd, "@NU_ID_CUENTA", DbType.Decimal, oItem.nu_id_cuenta);
                db.AddInParameter(cmd, "@VC_DESC_PUESTO", DbType.String, oItem.vc_desc_puesto);

                db.AddInParameter(cmd, "@NU_ID_PUESTO", DbType.Decimal, null);
                db.AddInParameter(cmd, "@NU_ID_PRUEBA", DbType.Decimal, null);

                db.AddInParameter(cmd, "@NU_ID_CLASIFICACION", DbType.Decimal, null);
                db.AddInParameter(cmd, "@VC_PRUEBA_PUBLICA", DbType.Int32, null);

                db.AddInParameter(cmd, "@VC_USR_REG", DbType.String, oItem.vc_usr_reg);
                db.AddInParameter(cmd, "@DT_FEC_REG", DbType.DateTime, oItem.dt_fec_reg.Value.ToShortDateString());
                db.AddInParameter(cmd, "@CH_STATUS", DbType.String, oItem.ch_status);
                db.AddInParameter(cmd, "@CH_FLAG", DbType.String, oItem.ch_flag);
                db.AddInParameter(cmd, "@CH_ESTADO", DbType.String, oItem.ch_estado);
                db.AddInParameter(cmd, "@CH_FICHA", DbType.String, oItem.ch_ficha);
                db.AddInParameter(cmd, "@VC_DESC_OBSV_PUESTO", DbType.String, oItem.vc_desc_observacion);

                db.AddInParameter(cmd, "@RUTA", DbType.String, 1);
                db.AddInParameter(cmd, "@NU_DIAS_VIGENCIA", DbType.Decimal, oItem.nu_dias_vigencia);

                db.AddInParameter(cmd, "@VC_NOMFILE", DbType.String,null);
                db.AddInParameter(cmd, "@NUM_TIEMPOFILE", DbType.Int32, null);
                
                db.ExecuteNonQuery(cmd);

                int nu_id_puesto_salida = Int32.Parse(db.GetParameterValue(cmd, "@NU_ID_PUESTO_SALIDA") + "");
                oItem.nu_id_puesto = Decimal.Parse(db.GetParameterValue(cmd, "@NU_ID_PUESTO_SALIDA") + "");
                db.SetParameterValue(cmd, "@RUTA", 2);
                
                    foreach (var item in oItem.PruebaDetalle)
                    {
                        db.SetParameterValue(cmd, "@NU_ID_PUESTO", nu_id_puesto_salida);
                        db.SetParameterValue(cmd, "@NU_ID_PRUEBA", item.nu_id_prueba);
                        db.SetParameterValue(cmd, "@NU_ID_CLASIFICACION", item.nu_id_clasificacion);
                        db.SetParameterValue(cmd, "@VC_USR_REG", oItem.vc_usr_reg);
                        db.SetParameterValue(cmd, "@DT_FEC_REG", oItem.dt_fec_reg);
                        db.SetParameterValue(cmd, "@CH_STATUS", oItem.ch_status);
                        db.SetParameterValue(cmd, "@VC_PRUEBA_PUBLICA", item.vc_prueba_publica);
                        db.SetParameterValue(cmd, "@VC_NOMFILE", "Prueba_"+ nu_id_puesto_salida.ToString()+ item.nu_id_prueba.ToString()+"."+item.vc_nomfile);
                        db.SetParameterValue(cmd, "@NUM_TIEMPOFILE", item.num_tiempofile);
                    db.ExecuteNonQuery(cmd);
                    }

                    return nu_id_puesto_salida;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        
        public IList<PuestoBE> List(PuestoBE oItem) //opcion = 0
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_SEL_PUESTOS_ASPIRANTES");
                db.AddInParameter(cmd, "@NU_ID_CUENTA", DbType.Decimal, oItem.nu_id_cuenta);
                db.AddInParameter(cmd, "@NU_ID_SUBCUENTA", DbType.Decimal, oItem.nu_id_subcuenta);
                using (IDataReader oR = db.ExecuteReader(cmd))
                {
                    return MapDataReaderToList(oR, oItem.opcion.Value);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("PuestoDA.List()", ex);
            }
        }

        public IList<PuestoBE> ListPruebas(PuestoBE oItem) //opcion =1
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_SEL_PRUEBA");
                db.AddInParameter(cmd, "@NU_ID_NIVEL_PRUEBA", DbType.Decimal, oItem.nu_id_nivel_prueba);
                using (IDataReader oR = db.ExecuteReader(cmd))
                {
                    return MapDataReaderToList(oR, oItem.opcion.Value);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("PuestoDA.ListPruebas()", ex);
            }
        }


        public IList<PuestoBE> ListaPruebaPuesto(PuestoBE oItem) //opcion = 2
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_SEL_PRUEBA_PUESTO");
                db.AddInParameter(cmd, "@NU_ID_PUESTO", DbType.Decimal, oItem.nu_id_puesto);
                using (IDataReader oR = db.ExecuteReader(cmd))
                {
                    return MapDataReaderToList(oR, oItem.opcion.Value);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("PuestoDA.ListaPruebaPuesto()", ex);
            }
        }

        public IList<PuestoBE> ListaCandidatosPuesto(PuestoBE oItem) //opcion = 4
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_SEL_CANDIDATOS");
                db.AddInParameter(cmd, "@NU_ID_PUESTO", DbType.Decimal, oItem.nu_id_puesto);
                db.AddInParameter(cmd, "@NU_RUTA", DbType.Decimal, oItem.opcion);
                using (IDataReader oR = db.ExecuteReader(cmd))
                {
                    return MapDataReaderToList(oR, oItem.opcion.Value);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("PuestoDA.ListaCandidatosPuesto()", ex);
            }
        }

        public IList<PuestoBE> ListaReporte(PuestoBE oItem) //opcion = 6
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_REPORTE_PRUEBAS");
                db.AddInParameter(cmd, "@NU_ID_CUENTA", DbType.Decimal, oItem.nu_id_cuenta);
                db.AddInParameter(cmd, "@NU_ID_SUBCUENTA", DbType.Decimal, oItem.nu_id_subcuenta);
                using (IDataReader oR = db.ExecuteReader(cmd))
                {
                    return MapDataReaderToList(oR, oItem.opcion.Value);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("PuestoDA.ListaReporte()", ex);
            }
        }

        private IList<PuestoBE> MapDataReaderToList(IDataReader oR, decimal nOpcion)
        {
            var lista = new List<PuestoBE>();

            while (oR.Read())
            {
                lista.Add(MapDataReaderToEntity(oR, nOpcion));
            }

            return lista;
        }

        private PuestoBE MapDataReaderToEntity(IDataReader oR, decimal nOpcion)
        {
            var item = new PuestoBE();
            if (nOpcion == 0)
            {
                item.nu_id_subcuenta        = oR["NU_ID_SUBCUENTA"].ToDecimal();
                item.nu_id_cuenta           = oR["NU_ID_CUENTA"].ToDecimal();
                item.nu_id_puesto           = oR["NU_ID_PUESTO"].ToDecimal();
                item.vc_desc_cuenta         = oR["VC_DESC_CUENTA"].ToText();
                item.vc_desc_sub_cuenta     = oR["VC_DESC_SUB_CUENTA"].ToText();
                item.vc_desc_puesto         = oR["VC_DESC_PUESTO"].ToText();
                item.ch_status              = oR["CH_STATUS"].ToText();
                item.ch_estado              = oR["CH_ESTADO"].ToText();
                item.ch_brecha              = oR["CH_BRECHA"].ToText();
                item.nu_pruebas             = oR["NU_PRUEBAS"].ToDecimal();
                item.nu_dias_vigencia       = oR["NU_DIAS_VIGENCIA"].ToDecimal();

            }
            else if (nOpcion == 1)
            {
                item.nu_id_prueba   = oR["NU_ID_PRUEBA"].ToDecimal();
                item.nu_id_tipo_prueba = oR["NU_ID_TIPO_PRUEBA"].ToDecimal();
                item.vc_desc_prueba = oR["VC_DESC_PRUEBA"].ToText();
                item.vc_desc_observacion = oR["VC_DESC_OBSERVACION"].ToText();
                item.nu_min_tiempo = oR["NU_TIEMPO_LIMITE_MIN"].ToDecimal();
                item.nu_preguntas   = oR["NU_PREGUNTAS"].ToDecimal();
                item.ch_status      = oR["CH_STATUS"].ToText();
                item.nu_id_clasificacion = oR["NU_ID_CLASIFICACION"].ToDecimal();
                item.vc_desc_clasificacion = oR["VC_DESC_CLASIFICACION"].ToText();
                item.archivo = oR["ARCHIVO"].ToText(); 
            }
            else if (nOpcion == 2)
            {
                item.nu_id_prueba           = oR["NU_ID_PRUEBA"].ToDecimal();
                item.nu_id_puesto           = oR["NU_ID_PUESTO"].ToDecimal();
                item.vc_desc_prueba         = oR["VC_DESC_PRUEBA"].ToText();
                item.vc_desc_observacion    = oR["VC_DESC_OBSERVACION"].ToText();
                item.nu_min_tiempo          = oR["NU_MIN_TIEMPO"].ToDecimal();
                item.nu_preguntas           = oR["NU_PREGUNTAS"].ToDecimal();
                item.nu_id_tipo_prueba      = oR["NU_ID_TIPO_PRUEBA"].ToDecimal();
                item.vc_desc_tipo_prueba    = oR["VC_DESC_TIPO_PRUEBA"].ToText();
                item.nu_id_nivel_prueba     = oR["NU_ID_NIVEL_PRUEBA"].ToDecimal();
                item.vc_desc_nivel_prueba   = oR["VC_DESC_NIVEL_PRUEBA"].ToText();
                item.ch_status              = oR["CH_STATUS"].ToText();
                item.PUBLICA                = oR["PUBLICA"].ToText();
            }
            if (nOpcion == 3)
            {
                item.nu_id_puesto       = oR["NU_ID_PUESTO"].ToDecimal();
                item.nu_id_subcuenta    = oR["NU_ID_SUBCUENTA"].ToDecimal();
                item.nu_id_cuenta       = oR["NU_ID_CUENTA"].ToDecimal();
                item.vc_desc_puesto     = oR["VC_DESC_PUESTO"].ToText();
                item.vc_desc_sub_cuenta = oR["VC_DESC_SUB_CUENTA"].ToText();
                item.vc_desc_cuenta     = oR["VC_DESC_CUENTA"].ToText();
                item.ch_status          = oR["CH_STATUS"].ToText();
            }
            if (nOpcion == 4)
            {
                item.nu_id_puesto = oR["NU_ID_PUESTO"].ToDecimal();
                item.nu_id_usuario = oR["NU_ID_USUARIO"].ToDecimal();
                item.vc_nombres = oR["VC_NOMBRES"].ToText();
                item.vc_apellidos = oR["VC_APELLIDOS"].ToText();
                item.vc_direccion_email_usuario = oR["VC_DIRECCION_EMAIL"].ToText();
                item.vc_telefono_usuario = oR["VC_TELEFONO"].ToText();
                item.vc_doc_identi = oR["VC_DOC_IDENTI"].ToText();
                item.vc_cod_usuario = oR["VC_COD_USUARIO"].ToText();
                item.vc_password = oR["VC_PASSWORD"].ToText();
                item.ch_status = oR["CH_STATUS"].ToText();
            }
            if (nOpcion == 6)
            {
                item.vc_desc_cuenta = oR["VC_DESC_CUENTA"].ToText();
                item.vc_desc_sub_cuenta = oR["VC_DESC_SUB_CUENTA"].ToText();
                item.vc_desc_puesto = oR["VC_DESC_PUESTO"].ToText();
                item.nu_pruebas = oR["NU_PRUEBAS"].ToDecimal();
            }
            if (nOpcion == 7)
            {
                item.nu_id_puesto = oR["NU_ID_PUESTO"].ToDecimal();
                item.nu_id_usuario = oR["NU_ID_USUARIO"].ToDecimal();
                item.vc_nombres = oR["VC_NOMBRES"].ToText();
                item.vc_apellidos = oR["VC_APELLIDOS"].ToText();
                item.vc_direccion_email_usuario = oR["VC_DIRECCION_EMAIL"].ToText();
                item.vc_telefono_usuario = oR["VC_TELEFONO"].ToText();
                item.vc_doc_identi = oR["VC_DOC_IDENTI"].ToText();
                item.vc_cod_usuario = oR["VC_COD_USUARIO"].ToText();
                item.vc_password = oR["VC_PASSWORD"].ToText();
                item.ch_status = oR["CH_STATUS"].ToText();
            }
                    return item;
            
        }

        public PuestoBE Get(PuestoBE oItem) //opcion = 3
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_GET_PUESTO");
                db.AddInParameter(cmd, "@NU_ID_PUESTO", DbType.Decimal, oItem.nu_id_puesto);

                using (IDataReader oR = db.ExecuteReader(cmd))
                {
                    while (oR.Read())
                    {
                        return MapDataReaderToEntity(oR,3);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("PuestoDA.Get()", ex);
            }

            return null;
        }

        public int Update(PuestoBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_UPD_PRUEBAS_PUESTO");

                db.AddInParameter(cmd, "@NU_ID_PUESTO", DbType.Decimal, oItem.nu_id_puesto);
                db.AddInParameter(cmd, "@NU_ID_PRUEBA", DbType.Decimal, null);
                db.AddInParameter(cmd, "@NU_ID_CLASIFICACION", DbType.Decimal, null);
                db.AddInParameter(cmd, "@VC_USR_MOD", DbType.String, oItem.vc_usr_mod);
                db.AddInParameter(cmd, "@DT_FEC_MOD", DbType.DateTime, oItem.dt_fec_mod);
                db.AddInParameter(cmd, "@VC_DESC_PUESTO", DbType.String, oItem.vc_desc_puesto);
                db.AddInParameter(cmd, "@RUTA", DbType.String, 1);
                db.AddInParameter(cmd, "@VC_PRUEBA_PUBLICA", DbType.Int32, null);
                db.AddOutParameter(cmd,"@NU_ID_PUESTO_SALIDA", DbType.Decimal, 10);

                db.AddInParameter(cmd, "@VC_NOMFILE", DbType.String, null);
                db.AddInParameter(cmd, "@NUM_TIEMPOFILE", DbType.Int32, null);


                db.ExecuteNonQuery(cmd);    
                int nu_id_puesto_salida = Int32.Parse(db.GetParameterValue(cmd, "@NU_ID_PUESTO_SALIDA") + "");

                db.SetParameterValue(cmd, "@RUTA", 2);
                if (oItem.PruebaDetalle != null)
                {
                    foreach (var item1 in oItem.PruebaDetalle)
                    {
                        db.SetParameterValue(cmd, "@NU_ID_PRUEBA", item1.nu_id_prueba);
                        db.SetParameterValue(cmd, "@NU_ID_CLASIFICACION", item1.nu_id_clasificacion);
                        db.SetParameterValue(cmd, "@VC_PRUEBA_PUBLICA", item1.vc_prueba_publica);
                        db.SetParameterValue(cmd, "@VC_NOMFILE", "Prueba_" + nu_id_puesto_salida.ToString() + item1.nu_id_prueba.ToString() + "." + item1.vc_nomfile);
                        db.SetParameterValue(cmd, "@NUM_TIEMPOFILE", item1.num_tiempofile);
                        db.ExecuteNonQuery(cmd);
                    }
                }
                return nu_id_puesto_salida;
            }
            catch (Exception ex)
            {
                throw new Exception("PuestoDA.Update()", ex);
            }
        }

         public int UpdateUsuarios(PuestoBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_UPD_CANDIDATO");
                
                db.AddInParameter(cmd, "@NU_ID_PUESTO", DbType.Decimal, oItem.nu_id_puesto);
                db.AddInParameter(cmd, "@VC_DIRECCION_EMAIL_USUARIO", DbType.String, null);
                db.AddInParameter(cmd, "@VC_DOC_IDENTI", DbType.String, null);
                db.AddInParameter(cmd, "@VC_TELEFONO_USUARIO", DbType.String, null);
                db.AddInParameter(cmd, "@VC_COD_USUARIO", DbType.String, null);
                db.AddInParameter(cmd, "@VC_PASSWORD", DbType.String, null);
                db.AddInParameter(cmd, "@VC_USR_REG", DbType.String, oItem.vc_usr_reg);
                db.AddInParameter(cmd, "@DT_FEC_REG", DbType.DateTime, oItem.dt_fec_reg);

                foreach (var item in oItem.PersonaDetalle)
                {
                    db.SetParameterValue(cmd, "@VC_DIRECCION_EMAIL_USUARIO", item.vc_direccion_email_usuario);
                    db.SetParameterValue(cmd, "@VC_DOC_IDENTI", item.vc_doc_identi);
                    db.SetParameterValue(cmd, "@VC_TELEFONO_USUARIO", item.vc_telefono_usuario);
                    db.SetParameterValue(cmd, "@VC_COD_USUARIO", item.vc_cod_usuario);
                    db.SetParameterValue(cmd, "@VC_PASSWORD", item.vc_password);
                    db.ExecuteNonQuery(cmd);
                }

                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception("PuestoDA.UpdateUsuarios()", ex);
            }
        }
         public int UpdatePuesto(PuestoBE oItem)
         {
             try
             {
                 var cmd = db.GetStoredProcCommand("SP_UPD_ESTADO_PUESTO");

                 db.AddInParameter(cmd, "@NU_ID_PUESTO", DbType.Decimal, oItem.nu_id_puesto);
                 db.AddInParameter(cmd, "@VC_USR_MOD", DbType.String, oItem.vc_usr_mod);
                 db.AddInParameter(cmd, "@DT_FEC_MOD", DbType.DateTime, oItem.dt_fec_mod);

                 return db.ExecuteNonQuery(cmd);
             }
             catch (Exception ex)
             {
                 throw new Exception("PuestoDA.UpdatePuesto()", ex);
             }
         }
         public int UpdatePuestoTipo(PuestoBE oItem)
         {
             try
             {
                 var cmd = db.GetStoredProcCommand("SP_UPD_ESTADO_PUESTO_TIPO");

                 db.AddInParameter(cmd, "@NU_ID_PUESTO", DbType.Decimal, oItem.nu_id_puesto);
                 db.AddInParameter(cmd, "@VC_USR_MOD", DbType.String, oItem.vc_usr_mod);
                 db.AddInParameter(cmd, "@DT_FEC_MOD", DbType.DateTime, oItem.dt_fec_mod);

                 return db.ExecuteNonQuery(cmd);
             }
             catch (Exception ex)
             {
                 throw new Exception("PuestoDA.UpdatePuesto()", ex);
             }
         }

         public Tuple<Int32,String> DeletePuesto(PuestoBE oItem)
         { 
             try
             {
                 

                 var cmd = db.GetStoredProcCommand("SP_DEL_PUESTO");

                 db.AddInParameter(cmd, "@NU_ID_PUESTO", DbType.Decimal, oItem.nu_id_puesto);
                 db.AddInParameter(cmd, "@VC_USR_DEL", DbType.String, oItem.vc_usr_mod);
                 db.AddInParameter(cmd, "@DT_FEC_DEL", DbType.DateTime, oItem.dt_fec_mod);
                 db.AddOutParameter(cmd, "@NU_FLAG_SALIDA", DbType.Decimal, 10);
                 db.AddOutParameter(cmd, "@NU_FLAG_MESSAGE", DbType.String,500);                

                 db.ExecuteNonQuery(cmd);
                 int NU_FLAG_SALIDA = Int32.Parse(db.GetParameterValue(cmd, "@NU_FLAG_SALIDA") + "");
                 String NU_FLAG_MESSAGE = (db.GetParameterValue(cmd, "@NU_FLAG_MESSAGE") + "");
   
                  Tuple<int, string> ParamOurPut = new Tuple<int, string>(NU_FLAG_SALIDA, NU_FLAG_MESSAGE);
                  
                 return ParamOurPut;


             }
             catch (Exception ex)
             {
                 throw new Exception("PuestoDA.DeletePuesto()", ex);
             }
         }

        public int Update_Estado_Prueba_Puesto(PuestoBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_UPD_ESTADO_PRUEBA_PUESTO");

                db.AddInParameter(cmd, "@NU_ID_PUESTO", DbType.Decimal, oItem.nu_id_puesto);
                db.AddInParameter(cmd, "@NU_ID_PRUEBA", DbType.Decimal, oItem.nu_id_prueba);
                db.AddInParameter(cmd, "@VC_USR_MOD", DbType.String, oItem.vc_usr_mod);
                db.AddInParameter(cmd, "@DT_FEC_MOD", DbType.DateTime, oItem.dt_fec_mod);

                return db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception("PuestoDA.Update_Estado_Prueba_Puesto()", ex);
            }
        }

        public int Update_Usuario(PuestoBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_UPD_ESTADO_USUARIO");

                db.AddInParameter(cmd, "@NU_ID_PUESTO", DbType.Decimal, oItem.nu_id_puesto);
                db.AddInParameter(cmd, "@NU_ID_USUARIO", DbType.Decimal, oItem.nu_id_usuario);
                db.AddInParameter(cmd, "@VC_USR_MOD", DbType.String, oItem.vc_usr_mod);
                db.AddInParameter(cmd, "@DT_FEC_MOD", DbType.DateTime, oItem.dt_fec_mod);

                return db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception("PuestoDA.Update_Usuario()", ex);
            }
        }

    }
}

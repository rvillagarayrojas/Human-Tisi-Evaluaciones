using Conexiones.SQLServer;
using MultiEntidad.A_Seleccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaccion.Recursos;
using System.Configuration;

namespace Transaccion.A_Seleccion
{
    public class T_Prueba : SqlCn
    {

        public List<MME_Prueba> Sel_Prueba(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_sel_prueba_candidato"))
                {
                    db.AddInParameter(cmd, "@nu_id_usuario", DbType.Decimal, mme.me_prueba.candidato.nu_id_usuario);

                    return LsMme(db.ExecuteReader(cmd), mme.nu_ruta);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Sel_Prueba_Parte(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_sel_prueba_parte"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.candidato.nu_id_usuario);
                    db.AddInParameter(cmd, "@nu_id_prueba", DbType.Decimal, mme.me_prueba.prueba.nu_id_prueba);

                    return LsMme(db.ExecuteReader(cmd), mme.nu_ruta);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_Pregunta(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_get_pregunta"))
                {
                    db.AddInParameter(cmd, "@nu_id_prueba_candidato", DbType.Decimal, mme.me_prueba.prueba_candidato.nu_id_prueba_candidato);
                    db.AddInParameter(cmd, "@nu_id_prueba_parte", DbType.Decimal, mme.me_prueba.prueba_parte.nu_id_prueba_parte);
                    db.AddInParameter(cmd, "@nu_id_pregunta", DbType.Decimal, mme.me_prueba.pregunta.nu_id_pregunta);

                    return LsMme(db.ExecuteReader(cmd), mme.nu_ruta);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Sel_Nro_Preguntas(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_sel_nro_preguntas"))
                {
                    db.AddInParameter(cmd, "@nu_id_prueba_candidato", DbType.Decimal, mme.me_prueba.prueba_candidato.nu_id_prueba_candidato);
                    db.AddInParameter(cmd, "@nu_id_prueba_parte", DbType.Decimal, mme.me_prueba.prueba_parte.nu_id_prueba_parte);

                    return LsMme(db.ExecuteReader(cmd), mme.nu_ruta);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Sel_Alternativa(List<MME_Prueba> ls_mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_sel_alternativas"))
                {
                    db.AddInParameter(cmd, "@nu_id_prueba_candidato", DbType.Decimal, ls_mme[0].me_prueba.prueba_candidato.nu_id_prueba_candidato);
                    db.AddInParameter(cmd, "@nu_id_pregunta", DbType.Decimal, ls_mme[0].me_prueba.pregunta.nu_id_pregunta);
                    ls_mme[0].ls_mme_prueba = LsMme(db.ExecuteReader(cmd), 4);
                    for (int i = 1; i < ls_mme.Count; i++)
                    {
                        db.SetParameterValue(cmd, "@nu_id_prueba_candidato", ls_mme[i].me_prueba.prueba_candidato.nu_id_prueba_candidato);
                        db.SetParameterValue(cmd, "@nu_id_pregunta", ls_mme[i].me_prueba.pregunta.nu_id_pregunta);
                        ls_mme[i].ls_mme_prueba = LsMme(db.ExecuteReader(cmd), 4);
                    }
                    return ls_mme;
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public int Ins_Respuesta(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                cmd = db.GetStoredProcCommand("sp_ins_respuesta");
                db.AddInParameter(cmd, "@nu_id_prueba_candidato", DbType.Decimal, mme.me_prueba.prueba_candidato.nu_id_prueba_candidato);
                db.AddInParameter(cmd, "@nu_id_prueba_parte", DbType.Decimal, mme.me_prueba.prueba_parte.nu_id_prueba_parte);
                db.AddInParameter(cmd, "@nu_id_pregunta", DbType.Decimal, mme.me_prueba.pregunta.nu_id_pregunta);
                db.AddInParameter(cmd, "@nu_id_alternativa", DbType.Decimal, mme.me_prueba.alternativa.nu_id_alternativa);
                db.AddInParameter(cmd, "@vc_usr", DbType.String, mme.me_prueba.alternativa.vc_usr_reg);
                db.ExecuteNonQuery(cmd);
                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public int Upd_Tiempo(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                cmd = db.GetStoredProcCommand("sp_upd_tiempo");
                db.AddInParameter(cmd, "@nu_id_prueba_candidato", DbType.Decimal, mme.me_prueba.prueba_candidato.nu_id_prueba_candidato);
                db.AddInParameter(cmd, "@progreso_m", DbType.Decimal, mme.me_prueba.prueba_candidato.progreso_m);
                db.AddInParameter(cmd, "@progreso_s", DbType.Decimal, mme.me_prueba.prueba_candidato.progreso_s);
                db.ExecuteNonQuery(cmd);
                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public int Upd_Terminar(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                cmd = db.GetStoredProcCommand("sp_upd_terminar");
                db.AddInParameter(cmd, "@nu_id_prueba_candidato", DbType.Decimal, mme.me_prueba.prueba_candidato.nu_id_prueba_candidato);
                db.ExecuteNonQuery(cmd);
                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        
        private List<MME_Prueba> LsMme(IDataReader oR, decimal? Ruta = null)
        {
            var ls_mme = new List<MME_Prueba>();
            while (oR.Read())
            {
                ls_mme.Add(Mme(oR, Ruta));
            }
            return ls_mme;
        }

        private MME_Prueba Mme(IDataReader oR, decimal? Ruta = null)
        {
            var mme = new MME_Prueba();
            if(Ruta == 1)
            {
                mme.me_prueba.candidato.nu_id_usuario                   = oR["nu_id_usuario"].ToDecimal();
                mme.me_prueba.prueba.nu_id_prueba                       = oR["nu_id_prueba"].ToDecimal();
                mme.me_prueba.prueba_candidato.dt_fec_ini               = oR["dt_fec_ini"].ToDateTime();
                mme.me_prueba.prueba_candidato.dt_fec_fin               = oR["dt_fec_fin"].ToDateTime();
                mme.me_prueba.prueba.vc_desc_tipo_prueba                = oR["vc_desc_tipo_prueba"].ToText();
                mme.me_prueba.prueba.vc_desc_prueba                     = oR["vc_desc_prueba"].ToText();
                mme.me_prueba.prueba.vc_desc_observacion                = oR["vc_desc_observacion"].ToText();            
                mme.me_prueba.prueba.nu_nro_preguntas                   = oR["nu_nro_preguntas"].ToInt32();
                mme.me_prueba.prueba.nu_nro_partes                      = oR["nu_nro_partes"].ToInt32();
                mme.me_prueba.prueba_parte.nu_tiempo_limite_min         = oR["nu_tiempo_limite_min"].ToInt32();
                mme.me_prueba.prueba.ch_status                          = oR["ch_status"].ToText();
                mme.me_prueba.prueba.ch_tiempo                          = oR["ch_tiempo"].ToText();
                mme.me_prueba.prueba.ch_estado_prueba                   = oR["ch_estado_prueba"].ToText();
            }
            if (Ruta == 2)
            {
                mme.me_prueba.prueba_parte.nu_id_prueba                         = oR["nu_id_prueba"].ToDecimal();
                mme.me_prueba.prueba_parte.nu_id_prueba_parte                   = oR["nu_id_prueba_parte"].ToDecimal();
                mme.me_prueba.prueba_parte.nu_nro_parte                         = oR["nu_nro_parte"].ToInt32();
                mme.me_prueba.prueba_parte.vc_desc_prueba_parte                 = oR["vc_desc_prueba_parte"].ToText();
                mme.me_prueba.prueba_parte.nu_tiempo_limite_min                 = oR["nu_tiempo_limite_min"].ToInt32();
                mme.me_prueba.prueba_candidato.nu_id_prueba_candidato           = oR["nu_id_prueba_candidato"].ToDecimal();
                mme.me_prueba.prueba_candidato.ch_estado                        = oR["ch_estado"].ToText();
                mme.me_prueba.prueba_candidato.nu_tiempo_transcurrido_segundos  = oR["nu_tiempo_transcurrido_segundos"].ToInt32();
                mme.me_prueba.prueba.ch_tiempo                                  = oR["ch_tiempo"].ToText();
                mme.me_prueba.prueba_parte.vc_instruccion1                      = oR["vc_instruccion1"].ToText();
                mme.me_prueba.prueba_parte.vc_instruccion2                      = oR["vc_instruccion2"].ToText();
            }
            if (Ruta == 3)
            {
                mme.me_prueba.pregunta.nu_id_pregunta                           = oR["nu_id_pregunta"].ToDecimal();
                mme.me_prueba.prueba_parte.nu_id_prueba_parte                   = oR["nu_id_prueba_parte"].ToDecimal();
                mme.me_prueba.pregunta.nu_nro_pregunta                          = oR["nu_nro_pregunta"].ToInt32();
                mme.me_prueba.pregunta.nu_division                              = oR["nu_division"].ToInt32();
                mme.me_prueba.pregunta.vc_indicador_letra                       = oR["vc_indicador_letra"].ToText();
                mme.me_prueba.pregunta.vc_desc_criterio                         = oR["vc_desc_criterio"].ToText();
                mme.me_prueba.pregunta.vc_desc_pregunta                         = oR["vc_desc_pregunta"].ToText();
                mme.me_prueba.pregunta.vc_desc_imagen                           = oR["vc_desc_imagen"].ToText();
                mme.me_prueba.pregunta.ch_presentacion                          = oR["ch_presentacion"].ToText();
                mme.me_prueba.pregunta.ch_orientacion                           = oR["ch_orientacion"].ToText();
                mme.me_prueba.pregunta.nu_nro_alternativa                       = oR["nu_nro_alternativa"].ToInt32();
                mme.me_prueba.pregunta.nu_id_imagen                             = oR["nu_id_imagen"].ToDecimal();
                mme.me_prueba.pregunta.ch_status                                = oR["ch_status"].ToText();
                mme.me_prueba.prueba_candidato.nu_tiempo_transcurrido           = oR["nu_tiempo_transcurrido"].ToDecimal();
                mme.me_prueba.prueba_candidato.nu_tiempo_transcurrido_segundos  = oR["nu_tiempo_transcurrido_segundos"].ToDecimal();
                mme.me_prueba.prueba_candidato.progreso_m                       = oR["progreso_m"].ToDecimal();
                mme.me_prueba.prueba.ch_tiempo                                  = oR["ch_tiempo"].ToText();
                mme.me_prueba.prueba_parte.vc_instruccion2                      = oR["vc_instruccion2"].ToText();
            }
            if (Ruta == 4)
            {
                mme.me_prueba.alternativa.ch_marca                      = oR["ch_marca"].ToText();
                mme.me_prueba.alternativa.nu_id_alternativa             = oR["nu_id_alternativa"].ToDecimal();
                mme.me_prueba.alternativa.nu_id_pregunta                = oR["nu_id_pregunta"].ToDecimal();
                mme.me_prueba.alternativa.nu_nro_alternativa            = oR["nu_nro_alternativa"].ToInt32();
                mme.me_prueba.alternativa.nu_division                   = oR["nu_division"].ToInt32();
                mme.me_prueba.alternativa.vc_desc_alternativa           = oR["vc_desc_alternativa"].ToText();
                mme.me_prueba.alternativa.vc_desc_imagen                = oR["vc_desc_imagen"].ToText();
                mme.me_prueba.alternativa.ch_tipo_alternativa           = oR["ch_tipo_alternativa"].ToText();
                mme.me_prueba.alternativa.ch_presentacion               = oR["ch_presentacion"].ToText();
                mme.me_prueba.alternativa.ch_orientacion                = oR["ch_orientacion"].ToText();
                mme.me_prueba.alternativa.ch_status                     = oR["ch_status"].ToText();
            }
            if (Ruta == 5)
            {
                mme.me_prueba.pregunta.nu_id_pregunta                   = oR["nu_id_pregunta"].ToDecimal();
                mme.me_prueba.pregunta.nu_nro_pregunta                  = oR["nu_nro_pregunta"].ToInt32();
                mme.me_prueba.pregunta.ch_marca                         = oR["ch_marca"].ToText();
            }
            return mme;
        }
    }
}

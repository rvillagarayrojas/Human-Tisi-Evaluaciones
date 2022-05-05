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
using Entidad.A_Seleccion;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;

namespace Transaccion.A_Seleccion
{
    public class T_Reportes : SqlCn
    {

        public List<MME_Prueba> Sel_Candidatos_Evaluacion(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_sel_candidatos_evaluacion"))
                {
                    db.AddInParameter(cmd, "@vc_dato", DbType.String, mme.me_prueba.candidato_evaluacion.vc_dato);

                    return LsMme(db.ExecuteReader(cmd), 0);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_Prc_Wonderlic(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_prc_wonderlik"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);

                    return LsMme(db.ExecuteReader(cmd), 1);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }
        public List<MME_Prueba> Get_Prc_Excel(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("SP_PRC_EXCEL"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);

                    return LsMme(db.ExecuteReader(cmd), 1);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }
        
        public List<MME_Prueba> Get_Prc_Barsit(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("SP_PRC_BARSIT"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);

                    return LsMme(db.ExecuteReader(cmd), 1);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_Prc_Monedas1(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("SP_PRC_MONEDAS1"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);

                    return LsMme(db.ExecuteReader(cmd), 1);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_Prc_Gatb(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_prc_gatb"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);

                    return LsMme(db.ExecuteReader(cmd), 11);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_Prc_Ic(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_prc_ic"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);

                    return LsMme(db.ExecuteReader(cmd), 111);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_Prc_Disc(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_prc_disc_maximo"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);

                    return LsMme(db.ExecuteReader(cmd), 2);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_Prc_Analisis_Puesto(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_prc_disc_brecha"))
                {
                    db.AddInParameter(cmd, "@nu_id_puesto", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_puesto);

                    return LsMme(db.ExecuteReader(cmd), 16);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_Prc_Analisis_Puesto_Candidato(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_analisis_puesto_candidato"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);

                    return LsMme(db.ExecuteReader(cmd), 17);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }
        public List<MME_Prueba> Get_Prc_Cps(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_prc_cps"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);

                    return LsMme(db.ExecuteReader(cmd), 3);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_Prc_Kostick(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_prc_kostick"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);

                    return LsMme(db.ExecuteReader(cmd), 4);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_Prc_Ipv(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_prc_ipv"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);

                    return LsMme(db.ExecuteReader(cmd), 10);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_Prc_Zavic(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_prc_zavic"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);

                    return LsMme(db.ExecuteReader(cmd), 12);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_Prc_Domino(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_prc_domino"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);

                    return LsMme(db.ExecuteReader(cmd), 1);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }
        public List<MME_Prueba> Get_Prc_TIG1_Domino(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("SP_PRC_TIG1_DOMINO"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);

                    return LsMme(db.ExecuteReader(cmd), 1);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_Prc_Raven(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_prc_raven"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);

                    return LsMme(db.ExecuteReader(cmd), 1);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_Prc_Ice_Baron(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_prc_ice_baron"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);

                    return LsMme(db.ExecuteReader(cmd), 13);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_Prc_Neo_Pir(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("SP_PRC_NEO_PI"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);
                    return LsMme(db.ExecuteReader(cmd), 14);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_Prc_BFQ(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("SP_PRC_BFQ"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);
                    return LsMme(db.ExecuteReader(cmd), 14);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_Prc_16pf(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("SP_PRC_16PF"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);
                    return LsMme(db.ExecuteReader(cmd), 15);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_Prc_Minimult(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("SP_PRC_MINIMULT"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);
                    return LsMme(db.ExecuteReader(cmd), 15);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_Prc_Ingles(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_prc_ingles"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);
                    return LsMme(db.ExecuteReader(cmd), 18);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_Prc_SIV(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("SP_PRC_SIV"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);
                    return LsMme(db.ExecuteReader(cmd), 15);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_Prc_Habil_General(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("SP_PRC_HABIL_GENERAL"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);
                    return LsMme(db.ExecuteReader(cmd), 15);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_DatosPersonales_Ficha(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_get_datos_personales_ficha"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);

                    return LsMme(db.ExecuteReader(cmd), 5);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_Conocimiento_Ficha(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_get_conocimientos_ficha"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);

                    return LsMme(db.ExecuteReader(cmd), 6);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_Educacion_Ficha(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_get_educacion_ficha"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);

                    return LsMme(db.ExecuteReader(cmd), 7);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_ExperienciaLaboral_Ficha(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_get_experiencia_laboral_ficha"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);

                    return LsMme(db.ExecuteReader(cmd), 8);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<MME_Prueba> Get_Familiares_Ficha(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_get_familiares_ficha"))
                {
                    db.AddInParameter(cmd, "@nu_id_candidato", DbType.Decimal, mme.me_prueba.reporte_conocimiento.nu_id_candidato);

                    return LsMme(db.ExecuteReader(cmd), 9);
                }
            }
            catch (Exception ex) { throw ex; }
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
            if(Ruta == 0)
            {
                mme.me_prueba.candidato_evaluacion.nu_id_candidato                  = oR["nu_id_candidato"].ToDecimal();
                mme.me_prueba.candidato_evaluacion.vc_nombres_candidato             = oR["vc_nombres_candidato"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_doc_identi                    = oR["vc_doc_identi"].ToText();
                mme.me_prueba.candidato_evaluacion.nu_edad_candidato                = oR["nu_edad_candidato"].ToInt32();
                mme.me_prueba.candidato_evaluacion.vc_grado_instruccion             = oR["vc_grado_instruccion"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_profesion_candidato           = oR["vc_profesion_candidato"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_empresa_candidato             = oR["vc_empresa_candidato"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_ultima_experiencia_candidato  = oR["vc_ultima_experiencia_candidato"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_nombre_referencia_candidato   = oR["vc_nombre_referencia_candidato"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_telefono_referencia_candidato = oR["vc_telefono_referencia_candidato"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_consultor                     = oR["vc_consultor"].ToText();
            }

            if(Ruta == 1)
            {
                mme.me_prueba.reporte_conocimiento.vc_candidato                     = oR["vc_candidato"].ToText();
                mme.me_prueba.reporte_conocimiento.nu_edad                          = oR["nu_edad"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.vc_grado_instruccion             = oR["vc_grado_instruccion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_profesion                     = oR["vc_profesion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_empresa                       = oR["vc_empresa"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_ultima_experiencia            = oR["vc_ultima_experiencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia            = oR["vc_nombres_referencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia           = oR["vc_telefono_referencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia2           = oR["vc_nombres_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia2          = oR["vc_telefono_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_consultor                     = oR["vc_consultor"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_prueba                   = oR["vc_desc_prueba"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_puesto                   = oR["vc_desc_puesto"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_esperada   = oR["vc_desc_clasificacion_esperada"].ToText();
                mme.me_prueba.reporte_conocimiento.nu_rango_menor_esperado          = oR["nu_rango_menor_esperado"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_rango_mayor_esperado          = oR["nu_rango_mayor_esperado"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_rango_menor                   = oR["nu_rango_menor"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_rango_mayor                   = oR["nu_rango_mayor"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_parametro_obtenido            = oR["nu_parametro_obtenido"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_obtenida   = oR["vc_desc_clasificacion_obtenida"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_diagnostico              = oR["vc_desc_diagnostico"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_recomendacion                 = oR["vc_recomendacion"].ToText();
            }

            if(Ruta == 11)
            {
                mme.me_prueba.reporte_conocimiento.vc_candidato                     = oR["vc_candidato"].ToText();
                mme.me_prueba.reporte_conocimiento.nu_edad                          = oR["nu_edad"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.vc_grado_instruccion             = oR["vc_grado_instruccion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_profesion                     = oR["vc_profesion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_empresa                       = oR["vc_empresa"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_ultima_experiencia            = oR["vc_ultima_experiencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia            = oR["vc_nombres_referencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia           = oR["vc_telefono_referencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia2           = oR["vc_nombres_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia2          = oR["vc_telefono_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_consultor                     = oR["vc_consultor"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_prueba                   = oR["vc_desc_prueba"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_puesto                   = oR["vc_desc_puesto"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_esperada = oR["vc_desc_clasificacion_esperada"].ToText();
                mme.me_prueba.reporte_conocimiento.nu_rango_menor_esperado          = oR["nu_rango_menor_esperado"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_rango_mayor_esperado          = oR["nu_rango_mayor_esperado"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_rango_menor                   = oR["nu_rango_menor"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_rango_mayor                   = oR["nu_rango_mayor"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_parametro_obtenido            = oR["nu_parametro_obtenido"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_parametro_obtenido_v          = oR["nu_parametro_obtenido_v"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_parametro_obtenido_n          = oR["nu_parametro_obtenido_n"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_parametro_obtenido_q          = oR["nu_parametro_obtenido_q"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_parametro_obtenido_s          = oR["nu_parametro_obtenido_s"].ToDecimal();

                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_obtenida = oR["vc_desc_clasificacion_obtenida"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_obtenida_v = oR["vc_desc_clasificacion_obtenida_v"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_obtenida_n = oR["vc_desc_clasificacion_obtenida_n"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_obtenida_q = oR["vc_desc_clasificacion_obtenida_q"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_obtenida_s = oR["vc_desc_clasificacion_obtenida_s"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_diagnostico              = oR["vc_desc_diagnostico"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_diagnostico_v            = oR["vc_desc_diagnostico_v"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_diagnostico_n            = oR["vc_desc_diagnostico_n"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_diagnostico_q            = oR["vc_desc_diagnostico_q"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_diagnostico_s            = oR["vc_desc_diagnostico_s"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_recomendacion                 = oR["vc_recomendacion"].ToText();
            }

            if(Ruta == 111)
            {
                mme.me_prueba.reporte_conocimiento.vc_candidato                     = oR["vc_candidato"].ToText();
                mme.me_prueba.reporte_conocimiento.nu_edad                          = oR["nu_edad"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.vc_grado_instruccion             = oR["vc_grado_instruccion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_profesion                     = oR["vc_profesion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_empresa                       = oR["vc_empresa"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_ultima_experiencia            = oR["vc_ultima_experiencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia            = oR["vc_nombres_referencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia           = oR["vc_telefono_referencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia2           = oR["vc_nombres_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia2          = oR["vc_telefono_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_consultor                     = oR["vc_consultor"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_prueba                   = oR["vc_desc_prueba"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_puesto                   = oR["vc_desc_puesto"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_esperada   = oR["vc_desc_clasificacion_esperada"].ToText();
                mme.me_prueba.reporte_conocimiento.nu_rango_menor_esperado          = oR["nu_rango_menor_esperado"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_rango_mayor_esperado          = oR["nu_rango_mayor_esperado"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_rango_menor                   = oR["nu_rango_menor"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_rango_mayor                   = oR["nu_rango_mayor"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_parametro_obtenido            = oR["nu_parametro_obtenido"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_obtenida   = oR["vc_desc_clasificacion_obtenida"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_diagnostico              = oR["vc_desc_diagnostico"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_recomendacion                 = oR["vc_recomendacion"].ToText();
                mme.me_prueba.reporte_conocimiento.nu_categoria_esperada            = oR["nu_categoria_esperada"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_categoria_obtenida            = oR["nu_categoria_obtenida"].ToDecimal();
            }

            if(Ruta == 2)
            {
                mme.me_prueba.reporte_conocimiento.vc_candidato                     = oR["vc_candidato"].ToText();
                mme.me_prueba.reporte_conocimiento.nu_edad                          = oR["nu_edad"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.vc_grado_instruccion             = oR["vc_grado_instruccion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_profesion                     = oR["vc_profesion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_empresa                       = oR["vc_empresa"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_ultima_experiencia            = oR["vc_ultima_experiencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia            = oR["vc_nombres_referencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia           = oR["vc_telefono_referencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia2           = oR["vc_nombres_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia2          = oR["vc_telefono_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_consultor                     = oR["vc_consultor"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_prueba                   = oR["vc_desc_prueba"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_puesto                   = oR["vc_desc_puesto"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_esperada   = oR["vc_desc_clasificacion_esperada"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_puesto                   = oR["vc_desc_puesto"].ToText();
                
                mme.me_prueba.reporte_conocimiento.vc_patron_disc                   = oR["vc_patron_disc"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_descripcion_patron            = oR["vc_descripcion_patron"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_caracteristica                = oR["vc_caracteristica"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_caracteristica           = oR["vc_desc_caracteristica"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_obtenida   = oR["vc_desc_clasificacion_obtenida"].ToText();

                mme.me_prueba.reporte_conocimiento.nu_valor_d                       = oR["nu_valor_d"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_valor_i                       = oR["nu_valor_i"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_valor_s                       = oR["nu_valor_s"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_valor_c                       = oR["nu_valor_c"].ToDecimal();

            }

            if(Ruta == 3)
            {
                mme.me_prueba.reporte_conocimiento.vc_candidato                     = oR["vc_candidato"].ToText();
                mme.me_prueba.reporte_conocimiento.nu_edad                          = oR["nu_edad"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.vc_grado_instruccion             = oR["vc_grado_instruccion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_profesion                     = oR["vc_profesion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_empresa                       = oR["vc_empresa"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_ultima_experiencia            = oR["vc_ultima_experiencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia            = oR["vc_nombres_referencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia           = oR["vc_telefono_referencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia2           = oR["vc_nombres_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia2          = oR["vc_telefono_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_consultor                     = oR["vc_consultor"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_prueba                   = oR["vc_desc_prueba"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_puesto                   = oR["vc_desc_puesto"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_esperada   = oR["vc_desc_clasificacion_esperada"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_puesto                   = oR["vc_desc_puesto"].ToText();

                mme.me_prueba.reporte_conocimiento.vc_parametro_cps                 = oR["vc_parametro_cps"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_obtenida   = oR["vc_desc_clasificacion_obtenida"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_obtenida_alta = oR["vc_desc_clasificacion_obtenida_alta"].ToText();
                mme.me_prueba.reporte_conocimiento.nu_marcadas                      = oR["nu_marcadas"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_valor_s                       = oR["nu_valor_s"].ToDecimal();
            }

            if(Ruta == 4)
            {
                mme.me_prueba.reporte_conocimiento.vc_candidato                     = oR["vc_candidato"].ToText();
                mme.me_prueba.reporte_conocimiento.nu_edad                          = oR["nu_edad"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.vc_grado_instruccion             = oR["vc_grado_instruccion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_profesion                     = oR["vc_profesion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_empresa                       = oR["vc_empresa"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_ultima_experiencia            = oR["vc_ultima_experiencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia            = oR["vc_nombres_referencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia           = oR["vc_telefono_referencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia2           = oR["vc_nombres_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia2          = oR["vc_telefono_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_consultor                     = oR["vc_consultor"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_prueba                   = oR["vc_desc_prueba"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_puesto                   = oR["vc_desc_puesto"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_esperada   = oR["vc_desc_clasificacion_esperada"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_puesto                   = oR["vc_desc_puesto"].ToText();

                mme.me_prueba.reporte_conocimiento.vc_letra_parametro               = oR["vc_letra_parametro"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_obtenida   = oR["vc_desc_clasificacion_obtenida"].ToText();
                mme.me_prueba.reporte_conocimiento.nu_marcadas                      = oR["nu_marcadas"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.vc_desc_perfil                   = oR["vc_desc_perfil"].ToText();

                mme.me_prueba.reporte_conocimiento.vc_area_evaluada                 = oR["vc_descripcion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_positivo                      = oR["vc_positivo"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_negativo                      = oR["vc_negativo"].ToText();
            }
            if (Ruta == 5)
            {
                mme.me_prueba.candidato_evaluacion.vc_nombres_candidato     = oR["vc_nombres"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_apellidos_candidato   = oR["vc_apellidos"].ToText();
                mme.me_prueba.candidato_evaluacion.nu_edad_candidato        = oR["nu_edad"].ToInt();
                mme.me_prueba.candidato_evaluacion.vc_sexo_candidato        = oR["ch_sexo"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_fec_nacimiento        = oR["vc_fec_nacimiento"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_lugar_nacimiento      = oR["vc_lugar_nacimiento"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_estado_civil          = oR["vc_estado_civil"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_direccion             = oR["vc_direccion"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_lugar_nacimiento      = oR["vc_lugar_nacimiento"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_nro_telefono          = oR["vc_telefono"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_celular               = oR["vc_celular"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_brevete               = oR["vc_brevete"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_dni_candidato         = oR["vc_doc_identi"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_correo_candidato      = oR["vc_direccion_email"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_pretencion_salarial   = oR["vc_pretencion_salarial"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_desc_puesto           = oR["vc_desc_puesto"].ToText();

                mme.me_prueba.candidato_evaluacion.vc_empresa_candidato                 = oR["vc_empresa"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_nombre_referencia_candidato       = oR["vc_nombres_referencia"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_cargo_referencia                  = oR["vc_cargo_referencia"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_telefono_referencia_candidato     = oR["vc_telefono_referencia"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_empresa_referencia2               = oR["vc_empresa_referencia2"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_nombre_referencia_candidato2      = oR["vc_nombres_referencia2"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_cargo_referencia2                 = oR["vc_cargo_referencia2"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_telefono_referencia_candidato2    = oR["vc_telefono_referencia2"].ToText();

                mme.me_prueba.candidato_evaluacion.vc_busqueda_trabajo          = oR["vc_busqueda_trabajo"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_otro_proceso_seleccion    = oR["vc_otro_proceso_seleccion"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_contacto                  = oR["vc_contacto"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_telefono_empresa1         = oR["vc_telefono_empresa1"].ToText();
                mme.me_prueba.candidato_evaluacion.vc_telefono_empresa2         = oR["vc_telefono_empresa2"].ToText();
            }
            if (Ruta == 6)
            {
                mme.me_prueba.conocimiento.vc_desc_conocimiento     = oR["vc_desc_conocimiento"].ToText();
                mme.me_prueba.conocimiento.vc_nivel_conocimiento    = oR["vc_nivel_conocimiento"].ToText();
                mme.me_prueba.conocimiento.vc_desc_capacidad        = oR["vc_desc_capacidad"].ToText();
            }
            if (Ruta == 7)
            {
                mme.me_prueba.educacion.vc_desc_nivel_estudio   = oR["vc_desc_nivel_estudio"].ToText();
                mme.me_prueba.educacion.vc_desc_especialidad    = oR["vc_desc_especialidad"].ToText();
                mme.me_prueba.educacion.vc_centro_estudio       = oR["vc_centro_estudio"].ToText();
                mme.me_prueba.educacion.vc_anio_egreso          = oR["vc_anio_egreso"].ToText();
                mme.me_prueba.educacion.vc_titulo_obtenido      = oR["vc_titulo_obtenido"].ToText();
                mme.me_prueba.educacion.vc_ciclos_cursados      = oR["vc_ciclos_cursados"].ToText();
            }
            if (Ruta == 8)
            {
                mme.me_prueba.experiencia_laboral.vc_nombre_empresa     = oR["vc_nombre_empresa"].ToText();
                mme.me_prueba.experiencia_laboral.vc_puesto             = oR["vc_puesto"].ToText();
                mme.me_prueba.experiencia_laboral.dt_fec_inicio         = oR["dt_fec_inicio"].ToDateTime();
                mme.me_prueba.experiencia_laboral.dt_fec_fin            = oR["dt_fec_fin"].ToDateTime();
                mme.me_prueba.experiencia_laboral.nu_importe_sueldo     = oR["nu_importe_sueldo"].ToInt();
                mme.me_prueba.experiencia_laboral.vc_motivo_retiro      = oR["vc_motivo_retiro"].ToText();
            }
            if (Ruta == 9)
            {
                mme.me_prueba.familiares.vc_parentesco          = oR["vc_parentesco"].ToText();
                mme.me_prueba.familiares.vc_apellidos_nombres   = oR["vc_apellidos_nombres"].ToText();
                mme.me_prueba.familiares.vc_edad                = oR["vc_edad"].ToText();
                mme.me_prueba.familiares.vc_grado_academico     = oR["vc_grado_academico"].ToText();
                mme.me_prueba.familiares.vc_centro_trabajo      = oR["vc_centro_trabajo"].ToText();
                mme.me_prueba.familiares.vc_ocupacion           = oR["vc_ocupacion"].ToText();
            }
            if (Ruta == 10)
            {
                mme.me_prueba.reporte_conocimiento.vc_candidato                     = oR["vc_candidato"].ToText();
                mme.me_prueba.reporte_conocimiento.nu_edad                          = oR["nu_edad"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.vc_grado_instruccion             = oR["vc_grado_instruccion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_profesion                     = oR["vc_profesion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_empresa                       = oR["vc_empresa"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_ultima_experiencia            = oR["vc_ultima_experiencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia            = oR["vc_nombres_referencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia           = oR["vc_telefono_referencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia2           = oR["vc_nombres_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia2          = oR["vc_telefono_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_consultor                     = oR["vc_consultor"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_prueba                   = oR["vc_desc_prueba"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_puesto                   = oR["vc_desc_puesto"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_esperada   = oR["vc_desc_clasificacion_esperada"].ToText();

                mme.me_prueba.reporte_conocimiento.vc_letra_parametro               = oR["vc_letra_parametro"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_obtenida   = oR["vc_desc_clasificacion_obtenida"].ToText();
                mme.me_prueba.reporte_conocimiento.nu_marcadas                      = oR["nu_decapito_total"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.vc_desc_perfil                   = oR["vc_desc_clasificacion"].ToText();

                mme.me_prueba.reporte_conocimiento.vc_desc_diagnostico              = oR["vc_desc_diagnostico"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_positivo                      = oR["vc_positivo"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_negativo                      = oR["vc_negativo"].ToText();
            }
            if (Ruta == 12)
            {
                mme.me_prueba.reporte_conocimiento.vc_candidato                     = oR["vc_candidato"].ToText();
                mme.me_prueba.reporte_conocimiento.nu_edad                          = oR["nu_edad"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.vc_grado_instruccion             = oR["vc_grado_instruccion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_profesion                     = oR["vc_profesion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_empresa                       = oR["vc_empresa"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_ultima_experiencia            = oR["vc_ultima_experiencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia            = oR["vc_nombres_referencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia           = oR["vc_telefono_referencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia2           = oR["vc_nombres_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia2          = oR["vc_telefono_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_consultor                     = oR["vc_consultor"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_prueba                   = oR["vc_desc_prueba"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_puesto                   = oR["vc_desc_puesto"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_esperada   = oR["vc_desc_clasificacion_esperada"].ToText();

                mme.me_prueba.reporte_conocimiento.vc_letra_parametro               = oR["vc_parametro"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_obtenida   = oR["vc_desc_clasificacion"].ToText();
                mme.me_prueba.reporte_conocimiento.nu_marcadas                      = oR["nu_puntuacion"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.vc_desc_perfil                   = oR["vc_desc_parametro"].ToText();

                mme.me_prueba.reporte_conocimiento.vc_positivo                      = oR["vc_positivo"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_negativo                      = oR["vc_negativo"].ToText();
            }
            if (Ruta == 13)
            {
                mme.me_prueba.reporte_conocimiento.vc_candidato                     = oR["vc_candidato"].ToText();
                mme.me_prueba.reporte_conocimiento.nu_edad                          = oR["nu_edad"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.vc_grado_instruccion             = oR["vc_grado_instruccion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_profesion                     = oR["vc_profesion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_empresa                       = oR["vc_empresa"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_ultima_experiencia            = oR["vc_ultima_experiencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia            = oR["vc_nombres_referencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia           = oR["vc_telefono_referencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia2           = oR["vc_nombres_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia2          = oR["vc_telefono_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_consultor                     = oR["vc_consultor"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_prueba                   = oR["vc_desc_prueba"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_puesto                   = oR["vc_desc_puesto"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_esperada   = oR["vc_desc_clasificacion_esperada"].ToText();

                mme.me_prueba.reporte_conocimiento.vc_letra_indicador               = oR["vc_letra_indicador"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_letra_parametro               = oR["vc_letra_parametro"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_obtenida   = oR["vc_desc_clasificacion_obtenida"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_diagnostico              = oR["vc_desc_diagnostico"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_diagnostico_v            = oR["vc_nivel"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_recomendacion                 = oR["vc_desc_condicion"].ToText();

                mme.me_prueba.reporte_conocimiento.nu_pd                            = oR["nu_pd"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_ce                            = oR["nu_ce"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_suma                          = oR["nu_suma"].ToDecimal();
            }
            if (Ruta == 14)
            {
                mme.me_prueba.reporte_conocimiento.vc_candidato                     = oR["vc_candidato"].ToText();
                mme.me_prueba.reporte_conocimiento.nu_edad                          = oR["nu_edad"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.vc_grado_instruccion             = oR["vc_grado_instruccion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_profesion                     = oR["vc_profesion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_empresa                       = oR["vc_empresa"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_ultima_experiencia            = oR["vc_ultima_experiencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia            = oR["vc_nombres_referencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia           = oR["vc_telefono_referencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia2           = oR["vc_nombres_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia2          = oR["vc_telefono_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_consultor                     = oR["vc_consultor"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_prueba                   = oR["vc_desc_prueba"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_puesto                   = oR["vc_desc_puesto"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_esperada   = oR["vc_desc_clasificacion_esperada"].ToText();

                mme.me_prueba.reporte_conocimiento.vc_letra_indicador                   = oR["vc_letra_indicador"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_letra_parametro                   = oR["vc_letra_parametro"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_diagnostico                  = oR["vc_desc_clasificacion_obtenida"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_obtenida       = oR["vc_descripcion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_obtenida_alta  = oR["vc_descripcion_alta"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_diagnostico_v                = oR["vc_nivel"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_recomendacion                     = oR["vc_desc_condicion"].ToText();

                mme.me_prueba.reporte_conocimiento.nu_pd                                = oR["nu_pd"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_ce                                = oR["nu_ce"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_suma                              = oR["nu_suma"].ToDecimal();
            }
            if (Ruta == 15)
            {
                mme.me_prueba.reporte_conocimiento.vc_candidato                         = oR["vc_candidato"].ToText();
                mme.me_prueba.reporte_conocimiento.nu_edad                              = oR["nu_edad"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.vc_grado_instruccion                 = oR["vc_grado_instruccion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_profesion                         = oR["vc_profesion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_empresa                           = oR["vc_empresa"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_ultima_experiencia                = oR["vc_ultima_experiencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia                = oR["vc_nombres_referencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia               = oR["vc_telefono_referencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia2               = oR["vc_nombres_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia2              = oR["vc_telefono_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_consultor                         = oR["vc_consultor"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_prueba                       = oR["vc_desc_prueba"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_puesto                       = oR["vc_desc_puesto"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_esperada       = oR["vc_desc_clasificacion_esperada"].ToText();

                mme.me_prueba.reporte_conocimiento.vc_letra_indicador                   = oR["vc_letra_indicador"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_letra_parametro                   = oR["vc_letra_parametro"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_diagnostico                  = oR["vc_desc_clasificacion_obtenida"].ToText();

                mme.me_prueba.reporte_conocimiento.nu_pd                                = oR["nu_pd"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_ce                                = oR["nu_ce"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_suma                              = oR["nu_suma"].ToDecimal();

                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_obtenida       = oR["vc_desc_correccion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_obtenida_alta  = oR["vc_desc_glosa"].ToText();
            }
            if (Ruta == 16)
            {
                mme.me_prueba.reporte_conocimiento.vc_desc_puesto           = oR["vc_desc_puesto"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_recomendacion         = oR["vc_desc_obsv_puesto"].ToText();
                mme.me_prueba.reporte_conocimiento.nu_marcadas              = oR["nu_cant_personas"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.vc_letra_parametro       = oR["vc_desc_parametro"].ToText();
                mme.me_prueba.reporte_conocimiento.nu_pd                    = oR["nu_intensidad"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_porcentaje            = oR["nu_porcentajes"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.vc_desc_diagnostico      = oR["vc_desc_correccion_corta"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_diagnostico_v    = oR["vc_desc_correccion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_diagnostico_n    = oR["vc_desc_glosa"].ToText();
            }
            if (Ruta == 17)
            {
                mme.me_prueba.reporte_conocimiento.vc_candidato                 = oR["vc_candidato"].ToText();
                mme.me_prueba.reporte_conocimiento.nu_edad                      = oR["nu_edad"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.vc_grado_instruccion         = oR["vc_grado_instruccion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_profesion                 = oR["vc_profesion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_empresa                   = oR["vc_empresa"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_ultima_experiencia        = oR["vc_ultima_experiencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia        = oR["definido_por"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia       = oR["vc_telefono_referencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia2       = oR["vc_nombres_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia2      = oR["vc_telefono_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_puesto               = oR["vc_desc_puesto"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_recomendacion             = oR["vc_desc_obsv_puesto"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_letra_parametro           = oR["vc_desc_parametro"].ToText();
                mme.me_prueba.reporte_conocimiento.nu_porcentaje                = oR["nu_porcentajes"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_porcentajes_candidatos    = oR["nu_porcentajes_candidatos"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_porcentaje_adaptacion     = oR["nu_porcentaje_adaptacion"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.vc_positivo                  = oR["ch_principal"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_negativo                  = oR["ch_principal_cand"].ToText();

                mme.me_prueba.reporte_conocimiento.vc_desc_diagnostico          = oR["vc_desc_correccion_corta"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_diagnostico_v        = oR["vc_desc_correccion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_diagnostico_n        = oR["vc_desc_glosa"].ToText();

                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_esperada       = oR["vc_desc_correccion_corta_puesto"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_obtenida       = oR["vc_desc_correccion_puesto"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_clasificacion_obtenida_alta  = oR["vc_desc_glosa_puesto"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_caracteristica               = oR["vc_desc_clasificacion"].ToText();
            }
            if (Ruta == 18)
            {
                mme.me_prueba.reporte_conocimiento.vc_candidato                 = oR["vc_candidato"].ToText();
                mme.me_prueba.reporte_conocimiento.nu_edad                      = oR["nu_edad"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.vc_grado_instruccion         = oR["vc_grado_instruccion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_profesion                 = oR["vc_profesion"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_empresa                   = oR["vc_empresa"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_ultima_experiencia        = oR["vc_ultima_experiencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia        = oR["vc_nombres_referencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia       = oR["vc_telefono_referencia"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_nombres_referencia2       = oR["vc_nombres_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_telefono_referencia2      = oR["vc_telefono_referencia2"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_puesto               = oR["vc_desc_puesto"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_letra_indicador           = oR["vc_letra_indicador"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_letra_parametro           = oR["vc_letra_parametro"].ToText();
                mme.me_prueba.reporte_conocimiento.vc_desc_caracteristica       = oR["vc_desc_clasificacion_obtenida"].ToText();
                mme.me_prueba.reporte_conocimiento.nu_porcentaje                = oR["nu_punt_basico"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_porcentajes_candidatos    = oR["nu_punt_interm"].ToDecimal();
                mme.me_prueba.reporte_conocimiento.nu_porcentaje_adaptacion     = oR["nu_punt_avanza"].ToDecimal();
            }
            return mme;
        }

        public List<E_PruebaCandidato> Get_Pruebas_Candidato(MME_Prueba mme)
        {
            try
            {
                object[] Parameters = new object[] { mme.me_prueba.reporte_conocimiento.nu_id_candidato };
                var listPruebas = db.ExecuteSprocAccessor<E_PruebaCandidato>("PRUEBA_CANDIDATO_BY_ID", Parameters);

                return listPruebas.ToList();
            }
            catch (Exception ex) { throw ex; }
        }
    }
}

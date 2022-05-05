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

using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net;
using Entidad.A_Seleccion;
using Entidad.A_General;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Transaccion.A_Seleccion
{
    public class T_Candidato_Evaluacion : SqlCn
    {
        public List<MME_Prueba> Get_Candidato_Evaluacion(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                using (cmd = db.GetStoredProcCommand("sp_get_candidato_evaluacion"))
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

        public int Upd_Candidato_Evaluacion(MME_Prueba mme)
        {
            DbCommand cmd = null;
            try
            {
                cmd = db.GetStoredProcCommand("sp_upd_candidato_evaluacion");
                db.AddInParameter(cmd, "@nu_id_usuario", DbType.Decimal, mme.me_prueba.candidato.nu_id_usuario);
                db.AddInParameter(cmd, "@vc_nombres_candidato", DbType.String, mme.me_prueba.candidato_evaluacion.vc_nombres_candidato.ToUpper());
                db.AddInParameter(cmd, "@vc_apellidos_candidato", DbType.String, mme.me_prueba.candidato_evaluacion.vc_apellidos_candidato.ToUpper());
                db.AddInParameter(cmd, "@ch_sexo_candidato", DbType.String, mme.me_prueba.candidato_evaluacion.vc_sexo_candidato);
                db.AddInParameter(cmd, "@nu_edad_candidato", DbType.Decimal, mme.me_prueba.candidato_evaluacion.nu_edad_candidato);
                db.AddInParameter(cmd, "@vc_profesion_candidato", DbType.String, mme.me_prueba.candidato_evaluacion.vc_profesion_candidato);
                db.AddInParameter(cmd, "@vc_ultima_experiencia_candidato", DbType.String, mme.me_prueba.candidato_evaluacion.vc_ultima_experiencia_candidato);
                db.AddInParameter(cmd, "@vc_empresa_candidato", DbType.String, mme.me_prueba.candidato_evaluacion.vc_empresa_candidato);
                db.AddInParameter(cmd, "@nu_sueldo_candidato", DbType.String, mme.me_prueba.candidato_evaluacion.nu_sueldo_candidato);
                db.AddInParameter(cmd, "@vc_nombre_referencia_candidato", DbType.String, mme.me_prueba.candidato_evaluacion.vc_nombre_referencia_candidato.ToUpper());
                db.AddInParameter(cmd, "@vc_telefono_referencia_candidato", DbType.String, mme.me_prueba.candidato_evaluacion.vc_telefono_referencia_candidato);
                db.AddInParameter(cmd, "@vc_nombre_referencia_candidato2", DbType.String, mme.me_prueba.candidato_evaluacion.vc_nombre_referencia_candidato2);
                db.AddInParameter(cmd, "@vc_telefono_referencia_candidato2", DbType.String, mme.me_prueba.candidato_evaluacion.vc_telefono_referencia_candidato2);
                db.AddInParameter(cmd, "@nu_id_grado_instruccion", DbType.String, mme.me_prueba.candidato_evaluacion.nu_id_grado_instruccion);
                db.AddInParameter(cmd, "@vc_usr_mod", DbType.String, mme.me_prueba.candidato_evaluacion.vc_usr_mod);
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

            mme.me_prueba.candidato_evaluacion.vc_nombres_candidato                 = oR["vc_nombres_candidato"].ToText();
            mme.me_prueba.candidato_evaluacion.vc_apellidos_candidato               = oR["vc_apellidos_candidato"].ToText();
            mme.me_prueba.candidato_evaluacion.vc_sexo_candidato                    = oR["vc_sexo_candidato"].ToText();
            mme.me_prueba.candidato_evaluacion.vc_correo_candidato                  = oR["vc_correo_candidato"].ToText();
            mme.me_prueba.candidato_evaluacion.vc_dni_candidato                     = oR["vc_dni_candidato"].ToText();
            mme.me_prueba.candidato_evaluacion.vc_nro_telefono                      = oR["vc_nro_telefono"].ToText();
            mme.me_prueba.candidato_evaluacion.nu_edad_candidato                    = oR["nu_edad_candidato"].ToInt32();
            mme.me_prueba.candidato_evaluacion.vc_profesion_candidato               = oR["vc_profesion_candidato"].ToText();
            mme.me_prueba.candidato_evaluacion.vc_ultima_experiencia_candidato      = oR["vc_ultima_experiencia_candidato"].ToText();
            mme.me_prueba.candidato_evaluacion.vc_empresa_candidato                 = oR["vc_empresa_candidato"].ToText();
            mme.me_prueba.candidato_evaluacion.nu_sueldo_candidato                  = oR["nu_sueldo_candidato"].ToDecimal();
            mme.me_prueba.candidato_evaluacion.vc_nombre_referencia_candidato       = oR["vc_nombre_referencia_candidato"].ToText();
            mme.me_prueba.candidato_evaluacion.vc_telefono_referencia_candidato     = oR["vc_telefono_referencia_candidato"].ToText();
            mme.me_prueba.candidato_evaluacion.vc_nombre_referencia_candidato2      = oR["vc_nombre_referencia_candidato2"].ToText();
            mme.me_prueba.candidato_evaluacion.vc_telefono_referencia_candidato2    = oR["vc_telefono_referencia_candidato2"].ToText();
            mme.me_prueba.candidato_evaluacion.nu_id_grado_instruccion              = oR["nu_id_grado_instruccion"].ToDecimal();
            mme.me_prueba.candidato_evaluacion.ch_proteccion_datos                  = oR["ch_proteccion_datos"].ToText();

            return mme;
        }





        public Tuple<Int32, String> Ins_Candidato(ref DbCommand cmd, MME_Prueba m,out List<MME_Prueba> itemOut)
        {
            Tuple<Int32, String> Tup_Salida;
            String Mensaje_Salida = "";

            List<MME_Prueba> itemCorreos = new List<MME_Prueba>();
            try
            {                              
                using (cmd = db.GetStoredProcCommand("sp_ins_persona_usuario"))
                {
                    cmd.CommandTimeout = int.Parse(ConfigurationSettings.AppSettings["Delay"]);
                    db.AddOutParameter(cmd, "@VC_DIRECCION_EMAIL_SALIDA", DbType.String, 50);
                    db.AddOutParameter(cmd, "@VC_DATOS_USUARIO_SALIDA", DbType.String, 100);
                    db.AddOutParameter(cmd, "@VC_COD_USUARIO_SALIDA", DbType.String, 11);
                    db.AddOutParameter(cmd, "@VC_PASSWORD_SALIDA", DbType.String, 11);
                    db.AddOutParameter(cmd, "@VC_DATO_CONSUL_SALIDA", DbType.String, 100);
                    db.AddOutParameter(cmd, "@VC_CORREO_CONSUL_SALIDA", DbType.String, 100);
                    db.AddOutParameter(cmd, "@VC_MENSAJE_SALIDA", DbType.String, 200);
                    
                    db.AddInParameter(cmd, "@VC_NOMBRES", DbType.String, m.me_prueba.persona.vc_nombres);
                    db.AddInParameter(cmd, "@VC_APELLIDOS", DbType.String, m.me_prueba.persona.vc_apellidos);
                    db.AddInParameter(cmd, "@CH_SEXO", DbType.String, m.me_prueba.persona.ch_sexo);
                    db.AddInParameter(cmd, "@VC_DIRECCION_EMAIL", DbType.String, m.me_prueba.persona.vc_direccion_email);
                    db.AddInParameter(cmd, "@VC_DOC_IDENTI", DbType.String, m.me_prueba.persona.vc_doc_identi);
                    db.AddInParameter(cmd, "@VC_TELEFONO", DbType.String, m.me_prueba.persona.vc_telefono);
                    db.AddInParameter(cmd, "@VC_USR_REG", DbType.String, m.me_prueba.persona.vc_usr_reg);
                    db.AddInParameter(cmd, "@DT_FEC_REG", DbType.DateTime, m.me_prueba.persona.dt_fec_reg.Value.ToShortDateString());
                    db.AddInParameter(cmd, "@CH_STATUS", DbType.String, m.me_prueba.persona.ch_status);

                    db.AddInParameter(cmd, "@VC_COD_USUARIO", DbType.String, m.me_prueba.persona.vc_cod_usuario);
                    db.AddInParameter(cmd, "@VC_PASSWORD", DbType.String, m.me_prueba.persona.vc_password);
                    db.AddInParameter(cmd, "@NU_ID_PERFIL", DbType.Decimal, m.me_prueba.persona.nu_id_perfil);
                    db.AddInParameter(cmd, "@NU_ID_PUESTO", DbType.Decimal, m.me_prueba.persona.nu_id_puesto);

                    foreach (var item in m.ls_mme_prueba)
                    {
                        db.SetParameterValue(cmd, "@VC_NOMBRES", item.me_prueba.persona.vc_nombres);
                        db.SetParameterValue(cmd, "@VC_APELLIDOS", item.me_prueba.persona.vc_apellidos);
                        db.SetParameterValue(cmd, "@CH_SEXO", item.me_prueba.persona.ch_sexo);
                        db.SetParameterValue(cmd, "@VC_DIRECCION_EMAIL", item.me_prueba.persona.vc_direccion_email);
                        db.SetParameterValue(cmd, "@VC_DOC_IDENTI", item.me_prueba.persona.vc_doc_identi);
                        db.SetParameterValue(cmd, "@VC_TELEFONO", item.me_prueba.persona.vc_telefono);

                        db.SetParameterValue(cmd, "@VC_COD_USUARIO", item.me_prueba.persona.vc_cod_usuario);
                        db.SetParameterValue(cmd, "@VC_PASSWORD", item.me_prueba.persona.vc_password);
                        db.SetParameterValue(cmd, "@NU_ID_PUESTO", item.me_prueba.persona.nu_id_puesto);

                        if (item.me_prueba.persona.opcion == 1)
                        {
                            db.ExecuteNonQuery(cmd);

                            item.me_prueba.persona.vc_direccion_email_salida = (db.GetParameterValue(cmd, "@VC_DIRECCION_EMAIL_SALIDA") + "");
                            item.me_prueba.persona.vc_datos_usuario_salida = (db.GetParameterValue(cmd, "@VC_DATOS_USUARIO_SALIDA") + "");
                            item.me_prueba.persona.vc_cod_usuario_salida = (db.GetParameterValue(cmd, "@VC_COD_USUARIO_SALIDA") + "");
                            item.me_prueba.persona.vc_password_salida = (db.GetParameterValue(cmd, "@VC_PASSWORD_SALIDA") + "");
                            item.me_prueba.persona.vc_dato_consul_salida = (db.GetParameterValue(cmd, "@VC_DATO_CONSUL_SALIDA") + "");
                            item.me_prueba.persona.vc_correo_consul_salida = (db.GetParameterValue(cmd, "@VC_CORREO_CONSUL_SALIDA") + "");

                            String Out_Put = (db.GetParameterValue(cmd, "@VC_MENSAJE_SALIDA") + "");
                            if (Out_Put.Length > 0)
                            {
                                Mensaje_Salida = Mensaje_Salida + "</br>" + Out_Put;
                            }
                            else{
                                itemCorreos.Add(item);
                                //AC_Envio_Correo(item);
                            }
                        }
                        else
                        {
                            try
                            {
                                db.ExecuteNonQuery(cmd);

                                String Out_Put = (db.GetParameterValue(cmd, "@VC_MENSAJE_SALIDA") + "");
                                if (Out_Put.Length > 0) Mensaje_Salida = Mensaje_Salida + "</br>" + Out_Put;
                            }
                            catch (Exception)
                            {
                                
                                throw;
                            }

                        }
                    }
                    if (Mensaje_Salida.Trim().Length == 0) Mensaje_Salida = "go";
                    Tup_Salida = new Tuple<Int32, String>(1, Mensaje_Salida);                    
                }
            }
            catch (Exception ex) 
            {
                throw ex; Tup_Salida = new Tuple<Int32, String>(0, ex.Message.ToString());  m.nu_status = 0; 
            }
            if (Mensaje_Salida.Trim().Length == 0 ) Mensaje_Salida = "go";
            itemOut = itemCorreos;
            return Tup_Salida;
        }

        public Tuple<Int32, String> Ins_Candidato_EnvioCorreo(List<MME_Prueba> ls_mme_prueba, Tuple<Int32, String> Tup_OutPut)
        {
            Tuple<Int32, String> Tup_Salida;
            String Mensaje_Salida = Tup_OutPut.Item2;
            try
            {
                    foreach (var item in ls_mme_prueba)
                    {

                        if (item.me_prueba.persona.opcion == 1)
                        {
                            AC_Envio_Correo(item);
                        }
                    }
                    Tup_Salida = new Tuple<Int32, String>(1, Mensaje_Salida);
                
            }
            catch (Exception ex)
            {
                throw ex; Tup_Salida = new Tuple<Int32, String>(0, ex.Message.ToString() + "<br>" + ((Mensaje_Salida == "go") ? "" : Mensaje_Salida));                
            }
            return Tup_Salida;
        }


        public MME_Prueba AC_Envio_Correo(MME_Prueba M)
        {
            try
            {
                MailMessage correo = new MailMessage();
                correo.To.Clear();

                string xbody = string.Empty;

                string SMTP_FROM_EMAIL = ConfigurationSettings.AppSettings["SMTP_FROM_EMAIL"].ToString();
                string SMTP_SERVER = ConfigurationSettings.AppSettings["SMTP_SERVER"].ToString();
                string SMTP_PORT = ConfigurationSettings.AppSettings["SMTP_PORT"].ToString();
                string SMTP_AUTH_USER = ConfigurationSettings.AppSettings["SMTP_AUTH_USER"].ToString();
                string SMTP_AUTH_PASSWORD = ConfigurationSettings.AppSettings["SMTP_AUTH_PASSWORD"].ToString();
                string SMTP_USE_SSL = ConfigurationSettings.AppSettings["SMTP_USE_SSL"].ToString();
                string EMAIL_SUBJECT = String.Format(ConfigurationSettings.AppSettings["EMAIL_SUBJECT"].ToString(),M.me_prueba.persona.vc_empresa_postula);


                string[] parametros = { M.me_prueba.persona.vc_datos_usuario_salida, M.me_prueba.persona.vc_cod_usuario_salida, M.me_prueba.persona.vc_password_salida, M.me_prueba.persona.vc_dato_consul_salida, M.me_prueba.persona.vc_correo_consul_salida };
                xbody = string.Format(body(), parametros);
                correo.Body = xbody;
                correo.Subject = EMAIL_SUBJECT;
                correo.IsBodyHtml = true;
                correo.BodyEncoding = Encoding.UTF8;

                correo.To.Add(M.me_prueba.persona.vc_direccion_email_salida);
                correo.From = new MailAddress(SMTP_FROM_EMAIL);

                SmtpClient envio = new SmtpClient(SMTP_SERVER);
                envio.Port = Int32.Parse(SMTP_PORT);
                envio.DeliveryMethod = SmtpDeliveryMethod.Network;
                envio.UseDefaultCredentials = false;
                envio.Credentials = new NetworkCredential(SMTP_AUTH_USER, SMTP_AUTH_PASSWORD);
                envio.EnableSsl = Boolean.Parse(SMTP_USE_SSL);


                try
                {
                    envio.Send(correo);
                    M.nu_status = 1;
                }

                catch (Exception ex)
                {
                    M.nu_status = 1;
                }

                return M;

            }
            catch
            {
                throw;
            }

        }

        public E_Candidato_Evaluacion ChangePassword(E_Candidato_Evaluacion oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_UPD_CUENTA_PASSWORD");

                db.AddInParameter(cmd, "@VC_COD_USUARIO", DbType.String, oItem.vc_cod_usuario);
                db.AddInParameter(cmd, "@vc_password_old", DbType.String, oItem.vc_password_old);
                db.AddInParameter(cmd, "@vc_password_new", DbType.String, oItem.vc_password_new);
                db.AddInParameter(cmd, "@vc_password_con", DbType.String, oItem.vc_password_con);
                db.AddOutParameter(cmd, "@vc_Mensaje_Out", DbType.String, 500);

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

        public List<E_Seguimiento> Get_Candidato_Seguimiento(E_Candidato mme)
        {
            try
            {
                object[] Parameters = new object[] { mme.nu_id_usuario };
                var listSeguimiento = db.ExecuteSprocAccessor<E_Seguimiento>("SP_SEL_LOG_SEGUIMIENTO_BYID", Parameters);
                
                return listSeguimiento.ToList();
                
            }
            catch (Exception ex) { throw ex; }
        }


        public List<E_RespuestaPreguntaFile> Get_RespuestaPreguntaFile(E_Candidato mme, out String ServidorDescarga)
        {
            try
            {
                ServidorDescarga = ConfigurationManager.AppSettings["ServidorDescarga"].ToString();
                object[] Parameters = new object[] { mme.nu_id_usuario,28,39, 2292 };
                var listRespuestaPreguntaFile = db.ExecuteSprocAccessor<E_RespuestaPreguntaFile>("GET_RESPUESTA_PREGUNTA_FILE_BY_CANDIDATO", Parameters);

                return listRespuestaPreguntaFile.ToList();

            }
            catch (Exception ex) { throw ex; }
        }

        public String  Set_RespuestaPregunta_Upd(E_RespuestaPreguntaFile oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SET_RESPUESTA_PREGUNTA_FILE_UPD");


                db.AddInParameter(cmd, "@NU_ID_PRUEBA_CANDIDATO", DbType.String, oItem.NU_ID_PRUEBA_CANDIDATO);
                db.AddInParameter(cmd, "@NU_ID_PREGUNTA", DbType.String, oItem.NU_ID_PREGUNTA);
                db.AddInParameter(cmd, "@NU_ID_PRUEBA_PARTE", DbType.String, oItem.NU_ID_PRUEBA_PARTE);
                db.AddInParameter(cmd, "@TIPO_REVISION", DbType.String, oItem.TIPO_REVISION);
                db.AddInParameter(cmd, "@NUM_NOTA", DbType.String, oItem.NUM_NOTA);
                db.AddInParameter(cmd, "@VC_COMENTARIOS", DbType.String, oItem.VC_COMENTARIOS);
                db.AddOutParameter(cmd, "@Message_out", DbType.String, 500);

                db.ExecuteNonQuery(cmd);

                String vc_Mensaje_Out = db.GetParameterValue(cmd, "@Message_out").ToString();

                return vc_Mensaje_Out;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private static string body()
        {
            string html = string.Empty;
            String LINK_HSMART_CANDIDATOS = ConfigurationManager.AppSettings["LINK_HSMART_CANDIDATOS"].ToString();
            html = "<table border=0 cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" >" +
                    "<tr>" +
                        "<td>" +
                            "<table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"700\" style=\"border-collapse: collapse\">" +
                                "<thead>" +
                                    "<tr>" +
                                        "<td style= \"background-color:rgb(235,110,25); padding: 3px; align:left; border-left: 1px solid #A8A4A4; border-right: 1px solid #A8A4A4; border-top: 1px solid #A8A4A4\" width=\"100px\">" +
                                            "<p style=\"font-size:18px; font-family: Calibri, serif; color: White\"><b>Acceso a Evaluación Psicolaboral Online</b></p>" +
                                            "<hr style=\"margin: 0px;\"/>" +
                                        "</td>" +
                                    "</tr>" +
                                "</thead>" +
                                "<tbody>" +
                                    "<tr>" +
                                        "<td style=\"padding: 3px;border-left: 1px solid #A8A4A4; border-right: 1px solid #A8A4A4\">" +
                                            "<p style=\"font-family:  Calibri, Candara, Segoe, 'Segoe UI', Optima, Arial, sans-serif;\"><br/><br/>Estimad@: {0} <br/><br/>A continuación le brindamos acceso para poder realizar una bateria de evaluaciones, que nos ayudarán<br/>a conocerl@ mejor.<br/><br/> Por favor realice las pruebas en un ambiente sin interrupciones y reserve un espacio de <br/>aproximadamente 90 minutos.<br/><br/></p>" +
                                        "</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td style=\"padding: 3px; border: 1px solid #A8A4A4;background-color:#DDDDDD\">" +
                                            "<p style=\"font-family:  Calibri, Candara, Segoe, 'Segoe UI', Optima, Arial, sans-serif; text-align: center\">Usuario: <b>{1}</b></p>" +
                                        "</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td style=\"padding: 3px; border: 1px solid #A8A4A4;background-color:#DDDDDD\">" +
                                            "<p style=\"font-family:  Calibri, Candara, Segoe, 'Segoe UI', Optima, Arial, sans-serif; text-align: center\">Contraseña: <b>{2}</b></p>" +
                                        "</td>" +
                                    "</tr>" +
                        "<tr>" +
                            "<td style=\"padding: 3px; border-left: 1px solid #A8A4A4; border-right: 1px solid #A8A4A4\">" +
                                    "<p style=\"font-family:  Calibri, Candara, Segoe, 'Segoe UI', Optima, Arial, sans-serif;\">Requisitos Básicos:" +
                                    "<br/>- Usar de preferencia el navegador web <b>Mozilla Firefox</b> o <b>Google Chrome</b> tanto para PC o Móviles." +
                                    "<br/>- Tener abierto <b>solo el sistema de evaluaciones</b> en el navegador web." +
                                    "<br/>- Cerrar todas las aplicaciones que consuman internet (<b>YouTube, Facebook, Etc.</b>) para una mejor fluidez en su evaluación." +
                                    "<br/>- Tener en cuenta que <b>la conexión a internet debe ser la adecuada</b> para evitar que el sistema se cuelgue.</p>" +
                            "</td>" +
                        "</tr>" +
                                    "<tr>" +
                                        "<td style=\"padding: 3px; border-left: 1px solid #A8A4A4; border-right: 1px solid #A8A4A4\">" +
                                             "<p style=\"font-family:  Calibri, Candara, Segoe, 'Segoe UI', Optima, Arial, sans-serif;\">Instrucciones:" +
                                             "<br/>- Ingrese en la siguiente pagina web: <a href=\"" + LINK_HSMART_CANDIDATOS  + "\">" + LINK_HSMART_CANDIDATOS + "</a>" +
                                             "<br/>- Ingrese su usuario y contraseña. <br/>- Siga las instrucciones que le muestra el sistema.<br/>- El sistema es sensible a mayúsculas y minúsculas por lo que recomendamos introducir cuidadosamente cada una de las letras que conforman sus datos. " +
                                             "<br/>- El sistema no le permitirá continuar con la evaluación en caso no consigne adecuadamente los datos y formatos señalados por la propia plataforma: <b>Fecha Formato: 00/xx/aaaa</b>" +
                                             "<br/>- Recordarle que una vez iniciada la sesión, en caso de tener la sesión inactiva durante 30 min, se cerrará automáticamente y tendrá que volver a iniciar sesión" +
                                             "<br/><br/>- Si por alguna razón se interrumpe la evaluación, vuelve a dar clic en este link y continua donde te quedaste." +
                                        "</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td colspan=\"2\" style=\"padding: 3px;border-left: 1px solid #A8A4A4; border-right: 1px solid #A8A4A4\">" +
                                            "<p style=\"font-family:  Calibri, Candara, Segoe, 'Segoe UI', Optima, Arial, sans-serif;\"><br/><br/>Atentamente</p>" +
                                        "</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td colspan=\"2\" style=\"padding: 3px; border-left: 1px solid #A8A4A4; border-right: 1px solid #A8A4A4\">" +
                                            "<p style=\"font-family:  Calibri, Candara, Segoe, 'Segoe UI', Optima, Arial, sans-serif;\">HUMAN SOLUTIONS</p>" +
                                        "</td>" +
                                    "</tr>" +
                                    //"<tr>" +
                                    //    "<td colspan=\"2\" style=\"padding: 3px; border-left: 1px solid #A8A4A4; border-right: 1px solid #A8A4A4\">" +
                                    //        "<p style=\"font-family:  Calibri, Candara, Segoe, 'Segoe UI', Optima, Arial, sans-serif;\">evaluaciones@zilicomgroup.com<br/></p>" +
                                    //    "</td>" +
                                    //"</tr>" +
                                    "<tr>" +
                                        "<td style=\"background-color:#DDDDDD; padding: 3px; align:left; border-left: 1px solid #A8A4A4; border-right: 1px solid #A8A4A4\">" +
                                            "<p style=\"text-align: left; font-size: 10px; font-family:  Calibri, Candara, Segoe, 'Segoe UI', Optima, Arial, sans-serif; color: White\"><b><i></b></i></p>" +
                                        "</td>" +
                                    "</tr>" +
                                "</tbody>" +
                                "<tfoot>" +
                                    "<tr>" +
                                        "<td style=\"background-color:#f6760b; padding: 3px; align:left; border-left: 1px solid #A8A4A4; border-right: 1px solid #A8A4A4\">" +
                                            "<p style=\"text-align: left; font-size: 14px; font-family:  Calibri, Candara, Segoe, 'Segoe UI', Optima, Arial, sans-serif; color: White\"><b><i><a style=\"color:white\" href=\"http://www.humansolutions.com.pe\">www.humansolutions.com.pe</a></b></i></p>" +
                                        "</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td style=\"border-left: 1px solid #A8A4A4; border-right: 1px solid #A8A4A4; border-bottom: 1px solid #A8A4A4\">" +
                                            "<p style=\"font-size: 10px;font-family:  Calibri, Candara, Segoe, 'Segoe UI', Optima, Arial, sans-serif;\">Autorizo expresamente, de manera libre y voluntaria, a TEST HSMART para recopilar almacenar procesar y disponer de los datos que sean suministrados por mí, así como para transferir dichos datos de manera total o parcial a las personas naturales o jurídicas que hayan solicitado los servicios profesionales de TEST HSMART para la recopilación o procesamiento de esta información. Entiendo que es posible que dentro de las pruebas efectuadas se entregue información sensible de conformidad con lo dispuesto por la ley, por lo cual autorizo expresamente su procesamiento. Así mismo declaro que he sido informado respecto de la finalidad para la cual se recopiló la información, especialmente relacionada con las gestiones encomendadas por el empleador, relacionadas con la identificación de mis competencias<br/><br/></p>" +
                                        "</td>" +
                                    "</tr>" +
                                "</tfoot>" +
                            "</table>" +
                        "</td>" +
                    "</tr>" +
                    "</table>";

            return html;
        }

        
    }
}

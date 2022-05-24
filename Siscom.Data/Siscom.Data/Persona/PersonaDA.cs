using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.Common;
using Siscom.Entity.Persona;
using Siscom.Library.Common;
using System.Net.Mail;
using System.Text;
using System.Net;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Siscom.Data.Interface;

namespace Siscom.Data.Persona
{
    public class PersonaDA : BaseAbstractDA
    {
        public int Insert(PersonaBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_INS_PERSONA_USUARIO");

                db.AddOutParameter(cmd, "@VC_DIRECCION_EMAIL_SALIDA", DbType.String, 50);
                db.AddOutParameter(cmd, "@VC_DATOS_USUARIO_SALIDA", DbType.String, 100);
                db.AddOutParameter(cmd, "@VC_COD_USUARIO_SALIDA", DbType.String, 11);
                db.AddOutParameter(cmd, "@VC_PASSWORD_SALIDA", DbType.String, 11);
                db.AddOutParameter(cmd, "@VC_DATO_CONSUL_SALIDA", DbType.String, 100);
                db.AddOutParameter(cmd, "@VC_CORREO_CONSUL_SALIDA", DbType.String, 100);

                db.AddInParameter(cmd, "@VC_NOMBRES", DbType.String,oItem.vc_nombres);
                db.AddInParameter(cmd, "@VC_APELLIDOS", DbType.String, oItem.vc_apellidos);
                db.AddInParameter(cmd, "@CH_SEXO", DbType.String, oItem.ch_sexo);
                db.AddInParameter(cmd, "@VC_DIRECCION_EMAIL", DbType.String, oItem.vc_direccion_email);
                db.AddInParameter(cmd, "@VC_DOC_IDENTI", DbType.String, oItem.vc_doc_identi);
                db.AddInParameter(cmd, "@VC_TELEFONO", DbType.String, oItem.vc_telefono);
                db.AddInParameter(cmd, "@VC_USR_REG", DbType.String, oItem.vc_usr_reg);
                db.AddInParameter(cmd, "@DT_FEC_REG", DbType.DateTime, oItem.dt_fec_reg.Value.ToShortDateString());
                db.AddInParameter(cmd, "@CH_STATUS", DbType.String, oItem.ch_status);

                db.AddInParameter(cmd, "@VC_COD_USUARIO", DbType.String, oItem.vc_cod_usuario);
                db.AddInParameter(cmd, "@VC_PASSWORD", DbType.String, oItem.vc_password);
                db.AddInParameter(cmd, "@NU_ID_PERFIL", DbType.Decimal, oItem.nu_id_perfil);
                db.AddInParameter(cmd, "@NU_ID_PUESTO", DbType.Decimal, oItem.nu_id_puesto);
                
                foreach (var item in oItem.PersonaDetalle)
                {
                    db.SetParameterValue(cmd, "@VC_NOMBRES", item.vc_nombres);
                    db.SetParameterValue(cmd, "@VC_APELLIDOS", item.vc_apellidos);
                    db.SetParameterValue(cmd, "@CH_SEXO", item.ch_sexo);
                    db.SetParameterValue(cmd, "@VC_DIRECCION_EMAIL", item.vc_direccion_email);
                    db.SetParameterValue(cmd, "@VC_DOC_IDENTI", item.vc_doc_identi);
                    db.SetParameterValue(cmd, "@VC_TELEFONO", item.vc_telefono);
                    db.SetParameterValue(cmd, "@VC_USR_REG", oItem.vc_usr_reg);
                    db.SetParameterValue(cmd, "@DT_FEC_REG", oItem.dt_fec_reg.Value.ToShortDateString());
                    db.SetParameterValue(cmd, "@CH_STATUS", oItem.ch_status);

                    db.SetParameterValue(cmd, "@VC_COD_USUARIO", item.vc_cod_usuario);
                    db.SetParameterValue(cmd, "@VC_PASSWORD", item.vc_password);
                    db.SetParameterValue(cmd, "@NU_ID_PERFIL", oItem.nu_id_perfil);
                    db.SetParameterValue(cmd, "@NU_ID_PUESTO", item.nu_id_puesto);
                    if (item.opcion == 1)
                    {
                        db.ExecuteNonQuery(cmd);

                        item.vc_direccion_email_salida = (db.GetParameterValue(cmd, "@VC_DIRECCION_EMAIL_SALIDA") + "");
                        item.vc_datos_usuario_salida = (db.GetParameterValue(cmd, "@VC_DATOS_USUARIO_SALIDA") + "");
                        item.vc_cod_usuario_salida = (db.GetParameterValue(cmd, "@VC_COD_USUARIO_SALIDA") + "");
                        item.vc_password_salida = (db.GetParameterValue(cmd, "@VC_PASSWORD_SALIDA") + "");
                        item.vc_dato_consul_salida = (db.GetParameterValue(cmd, "@VC_DATO_CONSUL_SALIDA") + "");
                        item.vc_correo_consul_salida = (db.GetParameterValue(cmd, "@VC_CORREO_CONSUL_SALIDA") + "");

                        //enviacorreo(vc_direccion_email_salida, vc_datos_usuario_salida, vc_cod_usuario_salida, vc_password_salida, vc_dato_consul_salida, vc_correo_consul_salida);
                    }
                    else
                    {
                        db.ExecuteNonQuery(cmd);

                    }
                }
                return 1;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }            
        }

        public int Actualizar(PersonaBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_UPD_CANDIDATO_BANDEJA");
                
                db.AddInParameter(cmd, "@VC_NOMBRES", DbType.String, oItem.vc_nombres);
                db.AddInParameter(cmd, "@VC_APELLIDOS", DbType.String, oItem.vc_apellidos);
                db.AddInParameter(cmd, "@VC_DIRECCION_EMAIL", DbType.String, oItem.vc_direccion_email);
                db.AddInParameter(cmd, "@VC_DOC_IDENTI", DbType.String, oItem.vc_doc_identi);
                db.AddInParameter(cmd, "@VC_TELEFONO", DbType.String, oItem.vc_telefono);
                db.AddInParameter(cmd, "@VC_COD_USUARIO", DbType.String, oItem.vc_cod_usuario);
                db.AddInParameter(cmd, "@VC_USR_REG", DbType.String, oItem.vc_usr_reg);

                return db.ExecuteNonQuery(cmd); ;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public IList<PersonaBE> List(PersonaBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_SEL_USUARIOS");
                db.AddInParameter(cmd, "@NU_ID_CUENTA", DbType.Decimal, oItem.nu_id_cuenta);
                db.AddInParameter(cmd, "@NU_ID_SUBCUENTA", DbType.Decimal, oItem.nu_id_subcuenta);
                db.AddInParameter(cmd, "@NU_ID_PUESTO", DbType.Decimal, oItem.nu_id_puesto);
                db.AddInParameter(cmd, "@VC_CRITERIO", DbType.Decimal, oItem.vc_criterio);
                db.AddInParameter(cmd, "@DT_FEC_INICIO", DbType.DateTime, oItem.dt_fec_inicio);
                db.AddInParameter(cmd, "@DT_FEC_FIN", DbType.DateTime, oItem.dt_fec_fin);
                db.AddInParameter(cmd, "@CRITERIO", DbType.String, oItem.vc_criterio);
                db.AddInParameter(cmd, "@NOMBRES", DbType.String, oItem.vc_nombres);
                db.AddInParameter(cmd, "@NUM_DOCUMENTO", DbType.String, oItem.vc_doc_identi);

                using (IDataReader oR = db.ExecuteReader(cmd))
                {
                    return MapDataReaderToList(oR,1);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("PersonaDA.List()", ex);
            }
        }

        private IList<PersonaBE> MapDataReaderToList(IDataReader oR, decimal nOpcion)
        {
            var lista = new List<PersonaBE>();

            while (oR.Read())
            {
                lista.Add(MapDataReaderToEntity(oR, nOpcion));
            }

            return lista;
        }

        private List<SeguimientoBE> MapDataReaderToListSeguimiento(IDataReader oR, decimal nOpcion)
        {
            var lista = new List<SeguimientoBE>();

            while (oR.Read())
            {
                lista.Add(MapDataReaderToEntitySeguimiento(oR, nOpcion));
            }

            return lista;
        }

        private SeguimientoBE MapDataReaderToEntitySeguimiento(IDataReader oR, decimal nOpcion)
        {
            var item = new SeguimientoBE();

            item.ID = Convert.ToDecimal(oR["ID"]);
            item.NU_ID_USUARIO = oR["NU_ID_USUARIO"].ToText().ToString();
            item.VC_COD_USUARIO = oR["VC_COD_USUARIO"].ToText().ToString();
            item.VC_NOM_PROCESO = oR["VC_NOM_PROCESO"].ToText().ToString();
            item.VC_NOM_ACCION = oR["VC_NOM_ACCION"].ToText().ToString();
            item.VC_DESC_ACCION = oR["VC_DESC_ACCION"].ToText().ToString();
            item.VC_URL_REFERRER_PAGINA = oR["VC_URL_REFERRER_PAGINA"].ToText().ToString();
            item.VC_URL_PAGINA = oR["VC_URL_PAGINA"].ToText().ToString();
            item.DT_FECHA_ACCION = Convert.ToDateTime(oR["DT_FECHA_ACCION"]);

            return item;
        }
        private PersonaBE MapDataReaderToEntity(IDataReader oR,  decimal nOpcion)
        {
            var item = new PersonaBE();
            if (nOpcion == 1)
            {
                item.nu_id_persona      = oR["NU_ID_PERSONA"].ToDecimal();
                item.nu_id_usuario      = oR["NU_ID_USUARIO"].ToDecimal();
                item.vc_doc_identi      = oR["VC_DOC_IDENTI"].ToText();
                item.vc_nombres         = oR["VC_NOMBRES"].ToText();
                item.vc_apellidos       = oR["VC_APELLIDOS"].ToText();
                item.vc_direccion_email = oR["VC_DIRECCION_EMAIL"].ToText();
                item.vc_desc_puesto     = oR["VC_DESC_PUESTO"].ToText();
                item.vc_desc_cuenta     = oR["VC_DESC_CUENTA"].ToText();
                item.nu_id_puesto       = oR["NU_ID_PUESTO"].ToDecimal();
                item.vc_desc_prueba     = oR["VC_DESC_PRUEBA"].ToText();
                item.dt_fec_evaluacion  = oR["DT_FEC_REG"].ToText();
                item.vc_cod_usuario     = oR["VC_COD_USUARIO"].ToText();
                item.ch_status          = oR["CH_STATUS"].ToText();
                item.ch_estado_prueba   = oR["CH_ESTADO_PRUEBA"].ToText();
                item.ch_ficha           = oR["CH_FICHA"].ToText();
                item.ch_brecha          = oR["CH_BRECHA"].ToText();
                item.vc_telefono        = oR["VC_TELEFONO"].ToText();
                item.vc_celular         = oR["VC_CELULAR"].ToText();
            }
            if (nOpcion == 2)
            {
                item.nu_id_persona          = oR["NU_ID_PERSONA"].ToDecimal();
                item.nu_id_usuario          = oR["NU_ID_USUARIO"].ToDecimal();
                item.vc_doc_identi          = oR["VC_DOC_IDENTI"].ToText();
                item.nu_id_cuenta           = oR["NU_ID_CUENTA"].ToDecimal();
                item.vc_desc_cuenta         = oR["VC_DESC_CUENTA"].ToText();
                item.nu_id_subcuenta        = oR["NU_ID_SUBCUENTA"].ToDecimal();
                item.vc_desc_sub_cuenta     = oR["VC_DESC_SUB_CUENTA"].ToText();
                item.nu_id_puesto           = oR["NU_ID_PUESTO"].ToDecimal();
                item.vc_desc_puesto         = oR["VC_DESC_PUESTO"].ToText();
                item.vc_nombres             = oR["VC_NOMBRES"].ToText();
                item.vc_apellidos           = oR["VC_APELLIDOS"].ToText();
                item.ch_sexo                = oR["CH_SEXO"].ToText();
                item.vc_direccion_email     = oR["VC_DIRECCION_EMAIL"].ToText();
                item.vc_telefono            = oR["VC_TELEFONO"].ToText();
                item.vc_desc_prueba         = oR["VC_DESC_PRUEBA"].ToText();
                item.datos_consultor        = oR["DATOS_CONSULTOR"].ToText();
                item.dt_fec_evaluacion      = oR["DT_FEC_EVALUACION"].ToText();
                item.vc_cod_usuario         = oR["VC_COD_USUARIO"].ToText();
                item.vc_password            = oR["VC_PASSWORD"].ToText();
                item.vc_correo_consul       = oR["VC_CORREO_CONSUL"].ToText();
                   
            }
            if (nOpcion == 6)
            {
                item.nu_id_puesto           = oR["NU_ID_PUESTO"].ToInt32();
                item.vc_desc_puesto         = oR["VC_DESC_PUESTO"].ToText();
                item.nu_id_prueba           = oR["NU_ID_PRUEBA"].ToInt32();
                item.vc_desc_sub_cuenta     = oR["VC_DESC_SUB_CUENTA"].ToText();
                item.vc_desc_prueba         = oR["VC_DESC_PRUEBA"].ToText();
                item.cantidad_usuario       = oR["CANTIDAD_USUARIO"].ToInt32();
            }
            if (nOpcion == 7)
            {
                item.nu_id_puesto = oR["NU_ID_PUESTO"].ToInt32();
                item.vc_desc_puesto = oR["VC_DESC_PUESTO"].ToText();
            }
            return item;
        }

        public IList<PersonaBE> ListUsuario(PersonaBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_GET_USUARIO");
                db.AddInParameter(cmd, "@NU_ID_USUARIO", DbType.Decimal, oItem.nu_id_usuario);
                db.AddInParameter(cmd, "@NU_ID_PUESTO", DbType.Decimal, oItem.nu_id_puesto);

                using (IDataReader oR = db.ExecuteReader(cmd))
                {
                    return MapDataReaderToList(oR,2);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("PersonaDA.Get()", ex);
            }

            return null;
        }

        public int Update_Usuario(PersonaBE oItem)
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
                throw new Exception("PersonaDA.Update_Usuario()", ex);
            }
        }

        public int Reactivar_Usuario(PersonaBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_UPD_USUARIO_RECTIVAR");

                db.AddInParameter(cmd, "@NU_ID_PUESTO", DbType.Decimal, oItem.nu_id_puesto);
                db.AddInParameter(cmd, "@NU_ID_USUARIO", DbType.Decimal, oItem.nu_id_usuario);
                db.AddInParameter(cmd, "@VC_USR_MOD", DbType.String, oItem.vc_usr_mod);
                db.AddInParameter(cmd, "@DT_FEC_MOD", DbType.DateTime, oItem.dt_fec_mod);

                return db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception("PersonaDA.Reactivar_Usuario()", ex);
            }
        }

        private static string body()
        {
            string html = string.Empty;

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
                                             "<p style=\"font-family:  Calibri, Candara, Segoe, 'Segoe UI', Optima, Arial, sans-serif;\"><br/><br/>Estimad@: {0} <br/><br/>A continuación le brindamos acceso para poder realizar una bateria de evaluaciones, que nos ayudarán<br/>a conocerl@ mejor.<br/><br/> Por favor realice las pruebas en un ambiente sin interrucciones y reserve un espacio de <br/>aproximadamente 90 minutos.<br/><br/></p>" +
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
                                             "<p style=\"font-family:  Calibri, Candara, Segoe, 'Segoe UI', Optima, Arial, sans-serif;\">Instrucciones:"+
                                             "<br/>- Ingrese en la siguiente pagina web: <a href=\"http://evaluaciones.tisi.com.pe\">https://evaluaciones.tisi.com.pe</a>"+
                                             "<br/>- Ingrese su usuario y contraseña. <br/>- Siga las instrucciones que le muestra el sistema.<br/>- El sistema es sensible a mayúsculas y minúsculas por lo que recomendamos introducir cuidadosamente cada una de las letras que conforman sus datos. "+
                                             "<br/>- El sistema no le permitirá continuar con la evaluación en caso no consigne adecuadamente los datos y formatos señalados por la propia plataforma: <b>Fecha Formato: 00/xx/aaaa</b>"+
                                             "<br/><br/>- Si por alguna razón se interrumpe la evaluación, vuelve a dar clic en este link y continua donde te quedaste." +
                                         "</td>" +
                                     "</tr>" +
                        "<tr>" +
                            "<td style=\"padding: 3px; border-left: 1px solid #A8A4A4; border-right: 1px solid #A8A4A4\">" +
                                    "<p style=\"font-family:  Calibri, Candara, Segoe, 'Segoe UI', Optima, Arial, sans-serif;\">Requisitos Básicos:" +
                                    "<br/>- Usar de preferencia el navegador web <b>Mozilla Firefox</b> o <b>Google Chrome</b> tanto para PC o Móviles." +
                                    "<br/>- Tener abierto <b>solo el sistema de evaluaciones</b> en el navegador web." +
                                    "<br/>- Cerrar todas las aplicaciones que consuman internet (<b>YouTube, Facebook, Etc.</b>) para una mejor fluidez en su evaluación." +
                                    "<br/><br/>- Tener en cuenta que <b>la conexión a internet debe ser la adecuada</b> para evitar que el sistema se cuelgue.</p>" +
                            "</td>" +
                        "</tr>" +
                                     "<tr>" +
                                         "<td colspan=\"2\" style=\"padding: 3px;border-left: 1px solid #A8A4A4; border-right: 1px solid #A8A4A4\">" +
                                             "<p style=\"font-family:  Calibri, Candara, Segoe, 'Segoe UI', Optima, Arial, sans-serif;\"><br/><br/>Atentamente</p>" +
                                         "</td>" +
                                     "</tr>" +
                                     "<tr>" +
                                         "<td colspan=\"2\" style=\"padding: 3px; border-left: 1px solid #A8A4A4; border-right: 1px solid #A8A4A4\">" +
                                             "<p style=\"font-family:  Calibri, Candara, Segoe, 'Segoe UI', Optima, Arial, sans-serif;\">{3}</p>" +
                                         "</td>" +
                                     "</tr>" +
                                     "<tr>" +
                                         "<td colspan=\"2\" style=\"padding: 3px; border-left: 1px solid #A8A4A4; border-right: 1px solid #A8A4A4\">" +
                                             "<p style=\"font-family:  Calibri, Candara, Segoe, 'Segoe UI', Optima, Arial, sans-serif;\">{4}<br/></p>" +
                                         "</td>" +
                                     "</tr>" +
                                     "<tr>" +
                                         "<td style=\"background-color:#DDDDDD; padding: 3px; align:left; border-left: 1px solid #A8A4A4; border-right: 1px solid #A8A4A4\">" +
                                             "<p style=\"text-align: left; font-size: 10px; font-family:  Calibri, Candara, Segoe, 'Segoe UI', Optima, Arial, sans-serif; color: White\"><b><i></b></i></p>" +
                                         "</td>" +
                                     "</tr>" +
                                 "</tbody>" +
                                 "<tfoot>" +
                                     "<tr>" +
                                         "<td style=\"border-left: 1px solid #A8A4A4; border-right: 1px solid #A8A4A4; border-bottom: 1px solid #A8A4A4\">" +
                                             "<p style=\"font-size: 10px;font-family:  Calibri, Candara, Segoe, 'Segoe UI', Optima, Arial, sans-serif;\">Autorizo expresamente, de manera libre y voluntaria, a TEST SMART SAC para recopilar almacenar procesar y disponer de los datos que sean suministrados por mí, así como para transferir dichos datos de manera total o parcial a las personas naturales o jurídicas que hayan solicitado los servicios profesionales de TEST SMART para la recopilación o procesamiento de esta información. Entiendo que es posible que dentro de las pruebas efectuadas se entregue información sensible de conformidad con lo dispuesto por la ley, por lo cual autorizo expresamente su procesamiento. Así mismo declaro que he sido informado respecto de la finalidad para la cual se recopiló la información, especialmente relacionada con las gestiones encomendadas por el empleador, relacionadas con la identificación de mis competencias<br/><br/></p>" +
                                         "</td>" +
                                     "</tr>" +
                                 "</tfoot>" +
                             "</table>" +
                         "</td>" +
                     "</tr>" +
                     "</table>";

            return html;
        }

        private static void enviacorreo(string vc_direccion_email_salida,string vc_datos_usuario_salida,string vc_cod_usuario_salida,string vc_password_salida,string vc_dato_consul_salida,string vc_correo_consul_salida)
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
                string EMAIL_SUBJECT = ConfigurationSettings.AppSettings["EMAIL_SUBJECT"].ToString();


                string[] parametros = { vc_datos_usuario_salida, vc_cod_usuario_salida, vc_password_salida, vc_dato_consul_salida, vc_correo_consul_salida };
                xbody = string.Format(body(), parametros);
                correo.Body = xbody;
                correo.Subject =EMAIL_SUBJECT ;
                correo.IsBodyHtml = true;
                correo.BodyEncoding = Encoding.UTF8;

                correo.To.Add(vc_direccion_email_salida);
                correo.From = new MailAddress(SMTP_FROM_EMAIL);

                SmtpClient envio = new SmtpClient(SMTP_SERVER);
                envio.Port = Int32.Parse(SMTP_PORT);
                envio.DeliveryMethod = SmtpDeliveryMethod.Network;
                envio.UseDefaultCredentials = false;
                envio.Credentials = new NetworkCredential(SMTP_AUTH_USER, SMTP_AUTH_PASSWORD);
                envio.EnableSsl = Boolean.Parse(SMTP_USE_SSL);

                envio.Send(correo);
            }
            catch
            {
                throw;
            }

        }

        public IList<PersonaBE> ListPuesto(PersonaBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_SEL_PUESTO_SUBCUENTA");
                db.AddInParameter(cmd, "@NU_ID_CUENTA", DbType.Decimal, oItem.nu_id_cuenta);
                db.AddInParameter(cmd, "@NU_ID_SUBCUENTA", DbType.Decimal, oItem.nu_id_subcuenta);
                db.AddInParameter(cmd, "@NU_ID_PUESTO", DbType.String, oItem.vc_desc_puesto);

                using (IDataReader oR = db.ExecuteReader(cmd))
                {
                    return MapDataReaderToList(oR,6);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("VacacionesDA.ListEmpleados()", ex);
            }
        }

        public IList<PersonaBE> ListPuestoView(PersonaBE oItem)
        {
            try
            {
                var cmd = db.GetStoredProcCommand("SP_SEL_PUESTOS");
                db.AddInParameter(cmd, "@NU_ID_PUESTO", DbType.String, oItem.nu_id_puesto);

                using (IDataReader oR = db.ExecuteReader(cmd))
                {
                    return MapDataReaderToList(oR, 7);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("VacacionesDA.ListEmpleados()", ex);
            }
        }

        public List<PersonaBE> ListSeguimientoView(PersonaBE oItem)
        {
            try
            {
                List<SeguimientoBE> ListaSeguimiento = new List<SeguimientoBE>();
                var cmd = db.GetStoredProcCommand("SP_SEL_LOG_SEGUIMIENTO_BYID");
                db.AddInParameter(cmd, "@P_NU_ID_USUARIO", DbType.String, oItem.nu_id_usuario);

                using (IDataReader oR = db.ExecuteReader(cmd))
                {
                    ListaSeguimiento = MapDataReaderToListSeguimiento(oR, oItem.opcion);
                }
                List<PersonaBE> ListaPersona = new List<PersonaBE>();
                oItem.Seguimiento = ListaSeguimiento;
                ListaPersona.Add(oItem);
                return ListaPersona;
            }
            catch (Exception ex)
            {
                throw new Exception("PersonaDA.ListSeguimientoView()", ex);
            }
        }

    }
}

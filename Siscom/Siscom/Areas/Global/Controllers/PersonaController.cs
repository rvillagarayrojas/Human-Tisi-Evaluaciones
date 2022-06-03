using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Siscom.Controllers;
using Siscom.Entity.Global;
using Siscom.Models;
using Siscom.SClient.Global;
using Siscom.WebLib.MvcShared;
using Siscom.Utility;
using Siscom.Controllers.Base;
using Siscom.Areas.Global.Models;
using Siscom.Areas.Global.Models.Validator;
using Siscom.Entity.Persona;
using System.Net.Mail;
using OfficeOpenXml;
using System.Text;
using System.Net;
using Elmah;

using MultiEntidad.A_Seleccion;
using Procedimiento.A_Seleccion;
using System.Configuration;
using Entidad.A_Seleccion;
using Entidad.A_General;
using Siscom.codeExport;

namespace Siscom.Areas.Global.Controllers
{
    public class PersonaController : BaseModelController<PersonaModels>
    {
        PersonaRestClient oPersonaRestClient;
        
        private readonly P_Reportes _P_Reportes;
        private readonly P_Candidato_Evaluacion _P_Candidato_Evaluacion;
        public PersonaController()  :base(new PersonaModelsValidator())
        {
            oPersonaRestClient = new PersonaRestClient();
            this._P_Candidato_Evaluacion = new P_Candidato_Evaluacion();
            this._P_Reportes = new P_Reportes();
        }
        
        public ActionResult Index()
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 40001;
            /*close>> Información */
            if (UsuarioSession.Usuario1.nu_id_cuenta == null)
            {
                return RedirectToAction("Login", "Login");
            }

            try
            {
                var PersonaObj = new PersonaModels();
            
                var model = new PersonaModels(); 
                if (UsuarioSession.Usuario1.nu_id_perfil == 1 || UsuarioSession.Usuario1.nu_id_perfil == 5)
                {
                    PersonaObj.Persona.nu_id_cuenta = UsuarioSession.Usuario1.nu_id_cuenta;
                }
                else
                {
                    PersonaObj.Persona.nu_id_subcuenta = UsuarioSession.Usuario1.nu_id_subcuenta;
                }
        
                //model.ListaPuesto = MetodosApp.ListaTipoPuesto(PersonaObj.Persona);

                return View(model);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                throw;
            }

        }


        public ActionResult DownloadExcel(int IdCuenta, int IdSubCuenta, int IdPuesto, string FechaIni, string FechaFin, string tipo, string vc_nombres, string vc_doc_identi)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 40004;
            /*close>> Información */

            try
            {
                var Persona = new PersonaBE();
                var ListaPersona = new List<PersonaBE>();
                String EstadoAnterior = "";
                var model = new PersonaModels();
                Persona.nu_id_cuenta = IdCuenta;
                Persona.nu_id_subcuenta = IdSubCuenta;
                Persona.nu_id_puesto = IdPuesto;
                Persona.dt_fec_inicio = Convert.ToDateTime(FechaIni);
                Persona.dt_fec_fin = Convert.ToDateTime(FechaFin);
                if (tipo == "1")
                {
                    Persona.dt_fec_inicio = Convert.ToDateTime(FechaIni);
                    Persona.dt_fec_fin = Convert.ToDateTime(FechaFin);
                }
                else if (tipo == "0")
                {
                    Persona.vc_nombres = vc_nombres;
                    Persona.vc_doc_identi = vc_doc_identi;
                }
                Persona.vc_criterio = tipo;
                Persona.opcion = 0;
                if (Persona.nu_id_puesto == -1)
                {
                    Persona.nu_id_puesto = null;
                }

                byte[] filecontent = null;
                if (Persona.nu_id_puesto == -1)
                {
                    Persona.nu_id_puesto = null;
                }
                model.ListaPersona = oPersonaRestClient.GetByFilters(Persona);
                if (model.ListaPersona != null)
                {
                    List<PersonaBE> Personas = new List<PersonaBE>();
                    List<String> ListaEvaluaciones = new List<String>();
                    decimal? Nf = 0;
                    foreach (var item in model.ListaPersona)
                    {
                        if (Nf != item.nu_id_usuario)
                        {
                            ListaEvaluaciones.Clear();
                            Personas.Add(item);
                            Nf = item.nu_id_usuario;
                            EstadoAnterior = item.ch_estado_prueba;
                            ListaEvaluaciones.Add(item.vc_desc_prueba);
                        }
                        else
                        {
                            ListaEvaluaciones.Add(item.vc_desc_prueba);
                            Personas[Personas.Count - 1].vc_desc_prueba = MostrarEvaluacionesLineal(ListaEvaluaciones);

                            if (EstadoAnterior == item.ch_estado_prueba)
                            {
                                Personas[Personas.Count - 1].ch_estado_prueba = item.ch_estado_prueba;
                                EstadoAnterior = item.ch_estado_prueba;
                            }
                            else
                            {
                                Personas[Personas.Count - 1].ch_estado_prueba = "EN CURSO";
                                EstadoAnterior = "EN CURSO";
                            }
                        }
                    }

                    string[] columnsFields = new[] { "vc_doc_identi", "vc_nombres", "vc_apellidos", "vc_direccion_email", "vc_telefono", "vc_celular", "vc_desc_puesto", "vc_desc_cuenta", "vc_desc_prueba", "dt_fec_evaluacion", "ch_estado_prueba" };
                    string[] columnsHeaders = new[] { "DNI", "NOMBRES", "APELLIDOS", "CORREO ELECTRÓNICO", "TELEFONO", "CELULAR", "PUESTO", "CUENTA", "EVALUACIÓN", "FECHA REGISTRO", "ESTADO" };
                    filecontent = ExcelExportHelper.ExportExcel(Personas, "Bandeja de Candidatos", true, columnsHeaders, columnsFields);
                }

                //if (Request.IsAjaxRequest())
                //{
                //    return PartialView("VP_GrillaUsuarios", model);
                //}
                //else
                //{
                //    return View(model);
                //}
                return File(filecontent, ExcelExportHelper.ExcelContentType, "Candidatos.xlsx");
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                throw;                
            }
        }
   
        public ActionResult Guardar(PersonaModels Model)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 40002;
            /*close>> Información */
            Tuple<Int32, String> Param_OutPut;
            try
            {

                Model.mme_datos.me_prueba.persona.vc_usr_reg = UsuarioSession.Usuario1.vc_cod_usuario;
                Model.mme_datos.me_prueba.persona.ch_status = "A";
                Model.mme_datos.me_prueba.persona.dt_fec_reg = Convert.ToDateTime(DateTime.Now);
                Model.mme_datos.me_prueba.persona.nu_id_perfil = 3;

                //var Persona = new PersonaBE();
                //Persona = Model.Persona;
                //Persona.vc_usr_reg = UsuarioSession.Usuario1.vc_cod_usuario;
                //Persona.ch_status = "A";
                //string fechacierre;
                //fechacierre = DateTime.Now.ToShortDateString();
                //Persona.dt_fec_reg = Convert.ToDateTime(fechacierre);
                //Persona.nu_id_perfil = 3;

                //Model.Persona = oPersonaRestClient.Save(Persona);


                Param_OutPut = _P_Candidato_Evaluacion.Ins_Candidato(Model.mme_datos);



                //PersonaBE envio = new PersonaBE();

                //foreach (var item in Model.mme_datos.ls_mme_prueba)
                //{

                //    envio.vc_cod_usuario_salida = item.me_prueba.persona.vc_cod_usuario_salida;
                //    envio.vc_correo_consul_salida = item.me_prueba.persona.vc_correo_consul_salida;
                //    envio.vc_dato_consul_salida = item.me_prueba.persona.vc_dato_consul_salida;
                //    envio.vc_datos_usuario_salida = item.me_prueba.persona.vc_datos_usuario_salida;
                //    envio.vc_direccion_email_salida = item.me_prueba.persona.vc_direccion_email_salida;
                //    envio.vc_password_salida = item.me_prueba.persona.vc_password_salida;
                //    envio= enviacorreo(envio);



                //}


                //if (envio.estado == "0")
                //{

                //    return AjaxResultSuccess(envio.mensaje);
                //}
                //else
                //{

                //    
                //}

                return Json(
                new
                {
                    result = Param_OutPut.Item1,
                    message = Param_OutPut.Item2,
                    JsonRequestBehavior.AllowGet
                });
                
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                if (ex.Message.Split('\"').Length > 7)
                {
                    return AjaxResultSuccess(ex.Message.Split('\"')[7].Split('\\')[0]);
                }
                else { return AjaxResultSuccess(ex.Message); }
            }
        }


        public ActionResult Actualizar(PersonaModels Model)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 40002;
            /*close>> Información */

            try
            {
                var Persona = new PersonaBE();
                Persona = Model.Persona;
                Persona.vc_usr_reg = UsuarioSession.Usuario1.vc_cod_usuario;
                Persona.opcion = 5;

                Persona = oPersonaRestClient.Save(Persona);
                return AjaxResultSuccess("go");
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultSuccess(ex.Message.Split('\"')[7].Split('\\')[0]);
            }
        }

        public ActionResult Bandeja()
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 40003;
            /*close>> Información */
            if (UsuarioSession.Usuario1.nu_id_cuenta == null)
            {
                return RedirectToAction("Login", "Login");
            }
            try
            {
                var PersonaObj = new PersonaModels();

                var model = new PersonaModels();

                if (UsuarioSession.Usuario1.nu_id_perfil != 4)
                {
                    PersonaObj.Persona.nu_id_cuenta = UsuarioSession.Usuario1.nu_id_cuenta;
                }
                else
                {
                    PersonaObj.Persona.nu_id_cuenta = null;
                }
                PersonaObj.Persona.ch_status = "A";

                model.ListaCuenta = MetodosApp.ListaTipoCuenta(PersonaObj.Persona);
                model.ListaSubCuenta = MetodosApp.ListaTipoSubCuenta(PersonaObj.Persona);

                model.Persona.nu_id_cuenta = UsuarioSession.Usuario1.nu_id_cuenta;
                model.Persona.dt_fec_inicio = DateTime.Parse(DateTime.Now.AddDays(-14).ToShortDateString());
                model.Persona.dt_fec_fin = DateTime.Parse(DateTime.Now.AddDays(1).ToShortDateString());                

                return View(model);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                throw;
            }

        }

        public string MostrarEvaluacionesLineal(List<String> Lista)
        {
            try
            {
                string StringReturn = "";
                Lista = Lista.Distinct().ToList();
                foreach (String cad in Lista)
                {
                    StringReturn = StringReturn + " / " + cad;
                }
                if (StringReturn.Length > 0)
                {
                    StringReturn = StringReturn.Substring(3, StringReturn.Length - 3);
                }
                return StringReturn;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                throw;
            }

        }

        public ActionResult Buscar(int IdCuenta, int IdSubCuenta, int IdPuesto, string FechaIni, string FechaFin,string tipo, string vc_nombres, string vc_doc_identi)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 40004;
            /*close>> Información */

            try
            {
                var Persona = new PersonaBE();
                var ListaPersona = new List<PersonaBE>();
                String EstadoAnterior = ""; 
                var model               = new PersonaModels();
                Persona.nu_id_cuenta    = IdCuenta;
                Persona.nu_id_subcuenta = IdSubCuenta;
                Persona.nu_id_puesto    = IdPuesto;
                if (tipo == "1") {
                    Persona.dt_fec_inicio = Convert.ToDateTime(FechaIni);
                    Persona.dt_fec_fin = Convert.ToDateTime(FechaFin);
                } else if (tipo == "0") {
                    Persona.vc_nombres = vc_nombres;
                    Persona.vc_doc_identi = vc_doc_identi;
                }
                Persona.vc_criterio = tipo;
                Persona.opcion          = 0;
                if (Persona.nu_id_puesto == -1)
                {
                    Persona.nu_id_puesto = null;
                }
                model.ListaPersona = oPersonaRestClient.GetByFilters(Persona);
                if (model.ListaPersona != null) {
                    List<PersonaBE> Personas = new List<PersonaBE>();
                    List<String> ListaEvaluaciones = new List<String>();
                    decimal? Nf = 0;
                    foreach (var item in model.ListaPersona)
                    {
                        if (Nf != item.nu_id_usuario)
                        {
                            ListaEvaluaciones.Clear();
                            Personas.Add(item);
                            Nf = item.nu_id_usuario;
                            EstadoAnterior = item.ch_estado_prueba;
                            ListaEvaluaciones.Add(item.vc_desc_prueba);
                        }
                        else
                        {
                            ListaEvaluaciones.Add(item.vc_desc_prueba);
                            Personas[Personas.Count - 1].vc_desc_prueba = MostrarEvaluacionesLineal(ListaEvaluaciones);
                        
                            //if (item.ch_estado_prueba == "PENDIENTE")
                            //{
                            //    Personas[Personas.Count - 1].ch_estado_prueba = item.ch_estado_prueba;
                            //}
                            //if (item.ch_estado_prueba == "EN CURSO")
                            //{
                            //    Personas[Personas.Count - 1].ch_estado_prueba = item.ch_estado_prueba;
                            //}
                            //Personas[Personas.Count - 1].ch_estado_prueba = item.ch_estado_prueba;

                            if(EstadoAnterior==item.ch_estado_prueba){
                                Personas[Personas.Count - 1].ch_estado_prueba = item.ch_estado_prueba;
                                EstadoAnterior = item.ch_estado_prueba;
                            }else{
                                Personas[Personas.Count - 1].ch_estado_prueba = "EN CURSO";
                                EstadoAnterior = "EN CURSO";
                            }
                           

                            
                        
                        }
                    }
                model.ListaPersona = Personas;
                }

                if (Request.IsAjaxRequest())
                {
                    return PartialView("VP_GrillaUsuarios", model);
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                throw;
            }
        }

        public ActionResult Select(decimal idPuesto, decimal idUsuario)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 40005;
            /*close>> Información */


            try
            {
                var Persona = new PersonaBE();
                var ListaPersona = new List<PersonaBE>();

                var model = new PersonaModels();

                Persona.nu_id_usuario = idUsuario;
                Persona.nu_id_puesto = idPuesto;
                Persona.opcion = 1;
                /*Consultando traslado por Id*/
                model.ListaPersona = oPersonaRestClient.GetByFilters(Persona);

                if (model.ListaPersona != null)
                {
                    List<PersonaBE> Personas = new List<PersonaBE>();
                    decimal? Nf = 0;
                    foreach (var item in model.ListaPersona)
                    {
                        if (Nf != item.nu_id_usuario)
                        {
                            Personas.Add(item);
                            Nf = item.nu_id_usuario;
                        }
                        else
                        {
                            Personas[Personas.Count - 1].vc_desc_prueba += " / " + item.vc_desc_prueba;
                        }
                    }
                    model.ListaPersona = Personas;
                }

                /*Retornado la vista con el modelo seteado*/
                return PartialView("VP_DatoCandidato", model);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultError(ex.Message);
            }
        }

        public ActionResult SelectSeguimiento(decimal idUsuario, String NombreUsuario, String ApellidosUsuario)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 40011;
            /*close>> Información */

            try
            {
                var Candidato = new E_Candidato();                

                var model = new PersonaModels();
                Candidato.nu_id_usuario = idUsuario;
                
                 /*Consultando traslado por Id*/

                 P_Candidato_Evaluacion Seg_Service = new P_Candidato_Evaluacion();

                PersonaModels objPersona = new PersonaModels();
                objPersona.Persona.vc_nombres = NombreUsuario;
                objPersona.Persona.vc_apellidos = ApellidosUsuario;
                objPersona.Ls_seguimiento = Seg_Service.Get_Candidato_Seguimiento(Candidato);                

                return PartialView("VP_SeguimientoCandidato", objPersona);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultError(ex.Message);
            }
        }

        public ActionResult LoadSubCuenta(int? cuentaid)
        {
            try
            {
                var model = new PersonaModels();

                model.Persona.nu_id_cuenta = cuentaid;
                model.Persona.nu_id_subcuenta = UsuarioSession.Usuario1.nu_id_subcuenta;
                model.ListaSubCuenta = MetodosApp.ListaTipoSubCuenta(model.Persona);
                return PartialView("VP_SubCuenta", model);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                throw;
            }

        }

        public ActionResult Enviar(PersonaModels M)
            //string emailref, string datosusuref, string codusuref, string passref, string datoconsulref, string cemailconsulref)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 40005;
            /*close>> Información */


            try
            {
                M.mme_prueba.me_prueba.persona.vc_direccion_email_salida =M.Persona.vc_direccion_email_salida;
                M.mme_prueba.me_prueba.persona.vc_datos_usuario_salida =M.Persona.vc_datos_usuario_salida;
                M.mme_prueba.me_prueba.persona.vc_cod_usuario_salida =M.Persona.vc_cod_usuario_salida;
                M.mme_prueba.me_prueba.persona.vc_password_salida =M.Persona.vc_password_salida;
                M.mme_prueba.me_prueba.persona.vc_dato_consul_salida =M.Persona.vc_dato_consul_salida;
                M.mme_prueba.me_prueba.persona.vc_correo_consul_salida =M.Persona.vc_correo_consul_salida;
                M.mme_prueba.me_prueba.persona.vc_empresa_postula = M.Persona.vc_empresa_postula;
                _P_Candidato_Evaluacion.AC_Envio(M.mme_prueba);
              
                    return AjaxResultSuccess("go");
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                if (ex.Message.Split('\"').Length > 7)
                {
                    return AjaxResultSuccess(ex.Message.Split('\"')[7].Split('\\')[0]);
                }
                else { return AjaxResultSuccess(ex.Message); }
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
                                            "<hr style=\"margin: 0px;\"/>"+
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
                                             "<p style=\"font-family:  Calibri, Candara, Segoe, 'Segoe UI', Optima, Arial, sans-serif;\">Instrucciones:" +
                                             "<br/>- Ingrese en la siguiente pagina web: <a href=\"" + LINK_HSMART_CANDIDATOS + "\">" + LINK_HSMART_CANDIDATOS + "</a>" +
                                             "<br/>- Ingrese su usuario y contraseña. <br/>- Siga las instrucciones que le muestra el sistema.<br/>- El sistema es sensible a mayúsculas y minúsculas por lo que recomendamos introducir cuidadosamente cada una de las letras que conforman sus datos. " +
                                             "<br/>- El sistema no le permitirá continuar con la evaluación en caso no consigne adecuadamente los datos y formatos señalados por la propia plataforma: <b>Fecha Formato: 00/xx/aaaa</b>" +
                                             "<br/>- Recordarle que una vez iniciada la sesión, en caso de tener la sesión inactiva durante 30 min, se cerrará automáticamente y tendrá que volver a iniciar sesión" +
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
                                        "<td style=\"background-color:#f6760b; padding: 3px; align:left; border-left: 1px solid #A8A4A4; border-right: 1px solid #A8A4A4\">" +
                                            "<p style=\"text-align: left; font-size: 14px; font-family:  Calibri, Candara, Segoe, 'Segoe UI', Optima, Arial, sans-serif; color: White\"><b><i><a style=\"color:white\" href=\"http://www.humansolutions.com.pe\">www.humansolutions.com.pe</a></b></i></p>" +
                                        "</td>" +
                                    "</tr>" +
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

        private  PersonaBE enviacorreo(PersonaBE m)
        {
            try
            {
                MailMessage correo = new MailMessage();
                correo.To.Clear();

                string xbody = string.Empty;

                string SMTP_FROM_EMAIL = ConfigurationManager.AppSettings["SMTP_FROM_EMAIL"].ToString();
                string SMTP_SERVER = ConfigurationManager.AppSettings["SMTP_SERVER"].ToString();
                string SMTP_PORT = ConfigurationManager.AppSettings["SMTP_PORT"].ToString();
                string SMTP_AUTH_USER = ConfigurationManager.AppSettings["SMTP_AUTH_USER"].ToString();
                string SMTP_AUTH_PASSWORD = ConfigurationManager.AppSettings["SMTP_AUTH_PASSWORD"].ToString();
                string SMTP_USE_SSL = ConfigurationManager.AppSettings["SMTP_USE_SSL"].ToString();
                string EMAIL_SUBJECT = ConfigurationManager.AppSettings["EMAIL_SUBJECT"].ToString();

                
                string[] parametros = { m.vc_datos_usuario_salida, m.vc_cod_usuario_salida, m.vc_password_salida, m.vc_dato_consul_salida, m.vc_correo_consul_salida };
                xbody = string.Format(body(), parametros);
                correo.Body = xbody;
                correo.Subject = EMAIL_SUBJECT;
                correo.IsBodyHtml = true;
                correo.BodyEncoding = Encoding.UTF8;

                correo.To.Add(m.vc_direccion_email_salida);
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
                    m.estado = "1";
                }

                catch (SmtpException ex)
                {
                    ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                    m.mensaje = ex.Message;
                    m.estado = "0";
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                throw;
               
            }
            return m;
        }
        
        public ActionResult V_Reporte_Completo(int id)
        {
            try
            {
                var model = new PersonaModels();

                model.mme_prueba.me_prueba.reporte_conocimiento.nu_id_candidato = id;

                List<E_PruebaCandidato> listPrueba = new List<E_PruebaCandidato>();

                listPrueba = _P_Reportes.Get_Pruebas_Candidato(model.mme_prueba);

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("TEST DE WONDERLIC")).Count() > 0)
                {
                    model.ls_wonderlik = _P_Reportes.Sel_Prc_Wonderlik(model.mme_prueba);
                    if (model.ls_wonderlik.Count > 0) { model.mme_datos = DatosPersonales(model.ls_wonderlik[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("TEST DE BARSIT")).Count() > 0)
                {
                    model.ls_barsit = _P_Reportes.Sel_Prc_Barsit(model.mme_prueba);
                    if (model.ls_barsit.Count > 0) { model.mme_datos = DatosPersonales(model.ls_barsit[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("TEST DE MONEDAS")).Count() > 0)
                {
                    model.ls_Monedas1 = _P_Reportes.Sel_Prc_Monedas1(model.mme_prueba);
                    if (model.ls_Monedas1.Count > 0) { model.mme_datos = DatosPersonales(model.ls_Monedas1[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("GATB")).Count() > 0)
                {
                    model.ls_gatb = _P_Reportes.Sel_Prc_Gatb(model.mme_prueba);
                    if (model.ls_gatb.Count > 0) { model.mme_datos = DatosPersonales(model.ls_gatb[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("TEST DE IC")).Count() > 0)
                {
                    model.ls_ic = _P_Reportes.Sel_Prc_Ic(model.mme_prueba);
                    if (model.ls_ic.Count > 0) { model.mme_datos = DatosPersonales(model.ls_ic[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("DISC")).Count() > 0)
                {
                    model.ls_disc = _P_Reportes.Sel_Prc_Disc(model.mme_prueba);
                    if (model.ls_disc.Count > 0) { model.mme_datos = DatosPersonales(model.ls_disc[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("CPS")).Count() > 0)
                {
                    model.ls_cps = _P_Reportes.Sel_Prc_Cps(model.mme_prueba);
                    if (model.ls_cps.Count > 0) { model.mme_datos = DatosPersonales(model.ls_cps[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("KOSTICK")).Count() > 0)
                {
                    model.ls_kostick = _P_Reportes.Sel_Prc_Kostick(model.mme_prueba);
                    if (model.ls_kostick.Count > 0) { model.mme_datos = DatosPersonales(model.ls_kostick[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("IPV")).Count() > 0)
                {
                    model.ls_ipv = _P_Reportes.Sel_Prc_Ipv(model.mme_prueba);
                    if (model.ls_ipv.Count > 0) { model.mme_datos = DatosPersonales(model.ls_ipv[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("ZAVIC")).Count() > 0)
                {
                    model.ls_zavic = _P_Reportes.Sel_Prc_Zavic(model.mme_prueba);
                    if (model.ls_zavic.Count > 0) { model.mme_datos = DatosPersonales(model.ls_zavic[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("DOMINOS")).Count() > 0)
                {
                    model.ls_domino = _P_Reportes.Sel_Prc_Domino(model.mme_prueba);
                    if (model.ls_domino.Count > 0) { model.mme_datos = DatosPersonales(model.ls_domino[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("TIG-1")).Count() > 0)
                {
                    model.ls_tig1_domino = _P_Reportes.Sel_Prc_TIG1_Domino(model.mme_prueba);
                    if (model.ls_tig1_domino.Count > 0) { model.mme_datos = DatosPersonales(model.ls_tig1_domino[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("RAVEN")).Count() > 0)
                {
                    model.ls_raven = _P_Reportes.Sel_Prc_Raven(model.mme_prueba);
                    if (model.ls_raven.Count > 0) { model.mme_datos = DatosPersonales(model.ls_raven[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("ICE BARON")).Count() > 0)
                {
                    model.ls_ice_baron = _P_Reportes.Sel_Prc_Ice_Baron(model.mme_prueba);
                    if (model.ls_ice_baron.Count > 0) { model.mme_datos = DatosPersonales(model.ls_ice_baron[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("NEO PI-R")).Count() > 0)
                {
                    model.ls_neo_pir = _P_Reportes.Sel_Prc_Neo_Pir(model.mme_prueba);
                    if (model.ls_neo_pir.Count > 0) { model.mme_datos = DatosPersonales(model.ls_neo_pir[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("BFQ")).Count() > 0)
                {
                    model.ls_bfq = _P_Reportes.Sel_Prc_BFQ(model.mme_prueba);
                    if (model.ls_bfq.Count > 0) { model.mme_datos = DatosPersonales(model.ls_bfq[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("16 PF-5")).Count() > 0)
                {
                    model.ls_16pf = _P_Reportes.Sel_Prc_16pf(model.mme_prueba);
                    if (model.ls_16pf.Count > 0) { model.mme_datos = DatosPersonales(model.ls_16pf[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("SIV")).Count() > 0)
                {
                    model.ls_siv = _P_Reportes.Sel_Prc_SIV(model.mme_prueba);
                    if (model.ls_siv.Count > 0) { model.mme_datos = DatosPersonales(model.ls_siv[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("HABILIDADES GERENCIALES")).Count() > 0)
                {
                    model.ls_habil_general = _P_Reportes.Sel_Prc_Habil_General(model.mme_prueba);
                    if (model.ls_habil_general.Count > 0) { model.mme_datos = DatosPersonales(model.ls_habil_general[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("MINIMULT")).Count() > 0)
                {
                    model.ls_minimult = _P_Reportes.Sel_Prc_Minimult(model.mme_prueba);
                    if (model.ls_minimult.Count > 0) { model.mme_datos = DatosPersonales(model.ls_minimult[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("INGLES")).Count() > 0)
                {
                    model.ls_ingles = _P_Reportes.Sel_Prc_Ingles(model.mme_prueba);
                    if (model.ls_ingles.Count > 0) { model.mme_datos = DatosPersonales(model.ls_ingles[0]); }
                }
                
                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("PRUEBA TÉCNICA")).Count() > 0)
                {
                    model.ls_excel = _P_Reportes.Get_Prc_Excel(model.mme_prueba);
                    if (model.ls_excel.Count > 0) { model.mme_datos = DatosPersonales(model.ls_excel[0]); }
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                throw;
            }
        }

        public MME_Prueba DatosPersonales(MME_Prueba Datos)
        {
            return Datos;
        }

        public ActionResult V_Reporte_Resumen(int id)
        {
            try
            {
                var model = new PersonaModels();

                model.mme_prueba.me_prueba.reporte_conocimiento.nu_id_candidato = id;

                List<E_PruebaCandidato> listPrueba = new List<E_PruebaCandidato>();

                listPrueba = _P_Reportes.Get_Pruebas_Candidato(model.mme_prueba);

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("TEST DE WONDERLIC")).Count() > 0)
                {
                    model.ls_wonderlik = _P_Reportes.Sel_Prc_Wonderlik(model.mme_prueba);
                    if (model.ls_wonderlik.Count > 0) { model.mme_datos = DatosPersonales(model.ls_wonderlik[0]); }
                }
                
                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("PRUEBA TÉCNICA")).Count() > 0)
                {
                    model.ls_excel = _P_Reportes.Get_Prc_Excel(model.mme_prueba);
                    if (model.ls_excel.Count > 0) { model.mme_datos = DatosPersonales(model.ls_excel[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("GATB")).Count() > 0)
                {
                    model.ls_gatb = _P_Reportes.Sel_Prc_Gatb(model.mme_prueba);
                    if (model.ls_gatb.Count > 0) { model.mme_datos = DatosPersonales(model.ls_gatb[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("TEST DE IC")).Count() > 0)
                {
                    model.ls_ic = _P_Reportes.Sel_Prc_Ic(model.mme_prueba);
                    if (model.ls_ic.Count > 0) { model.mme_datos = DatosPersonales(model.ls_ic[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("DISC")).Count() > 0)
                {
                    model.ls_disc = _P_Reportes.Sel_Prc_Disc(model.mme_prueba);
                    if (model.ls_disc.Count > 0) { model.mme_datos = DatosPersonales(model.ls_disc[0]); }
                }


                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("CPS")).Count() > 0)
                {
                    model.ls_cps = _P_Reportes.Sel_Prc_Cps(model.mme_prueba);
                    if (model.ls_cps.Count > 0) { model.mme_datos = DatosPersonales(model.ls_cps[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("KOSTICK")).Count() > 0)
                {
                    model.ls_kostick = _P_Reportes.Sel_Prc_Kostick(model.mme_prueba);
                    if (model.ls_kostick.Count > 0) { model.mme_datos = DatosPersonales(model.ls_kostick[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("IPV")).Count() > 0)
                {
                    model.ls_ipv = _P_Reportes.Sel_Prc_Ipv(model.mme_prueba);
                    if (model.ls_ipv.Count > 0) { model.mme_datos = DatosPersonales(model.ls_ipv[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("ZAVIC")).Count() > 0)
                {
                    model.ls_zavic = _P_Reportes.Sel_Prc_Zavic(model.mme_prueba);
                    if (model.ls_zavic.Count > 0) { model.mme_datos = DatosPersonales(model.ls_zavic[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("DOMINOS")).Count() > 0)
                {
                    model.ls_domino = _P_Reportes.Sel_Prc_Domino(model.mme_prueba);
                    if (model.ls_domino.Count > 0) { model.mme_datos = DatosPersonales(model.ls_domino[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("TIG-1")).Count() > 0)
                {
                    model.ls_tig1_domino = _P_Reportes.Sel_Prc_TIG1_Domino(model.mme_prueba);
                    if (model.ls_tig1_domino.Count > 0) { model.mme_datos = DatosPersonales(model.ls_tig1_domino[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("TEST DE BARSIT")).Count() > 0)
                {
                    model.ls_barsit = _P_Reportes.Sel_Prc_Barsit(model.mme_prueba);
                    if (model.ls_barsit.Count > 0) { model.mme_datos = DatosPersonales(model.ls_barsit[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("TEST DE MONEDAS")).Count() > 0)
                {
                    model.ls_Monedas1 = _P_Reportes.Sel_Prc_Monedas1(model.mme_prueba);
                    if (model.ls_Monedas1.Count > 0) { model.mme_datos = DatosPersonales(model.ls_Monedas1[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("RAVEN")).Count() > 0)
                {
                    model.ls_raven = _P_Reportes.Sel_Prc_Raven(model.mme_prueba);
                    if (model.ls_raven.Count > 0) { model.mme_datos = DatosPersonales(model.ls_raven[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("ICE BARON")).Count() > 0)
                {
                    model.ls_ice_baron = _P_Reportes.Sel_Prc_Ice_Baron(model.mme_prueba);
                    if (model.ls_ice_baron.Count > 0) { model.mme_datos = DatosPersonales(model.ls_ice_baron[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("NEO PI-R")).Count() > 0)
                {
                    model.ls_neo_pir = _P_Reportes.Sel_Prc_Neo_Pir(model.mme_prueba);
                    if (model.ls_neo_pir.Count > 0) { model.mme_datos = DatosPersonales(model.ls_neo_pir[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("BFQ")).Count() > 0)
                {
                    model.ls_bfq = _P_Reportes.Sel_Prc_BFQ(model.mme_prueba);
                    if (model.ls_bfq.Count > 0) { model.mme_datos = DatosPersonales(model.ls_bfq[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("16 PF-5")).Count() > 0)
                {
                    model.ls_16pf = _P_Reportes.Sel_Prc_16pf(model.mme_prueba);
                    if (model.ls_16pf.Count > 0) { model.mme_datos = DatosPersonales(model.ls_16pf[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("SIV")).Count() > 0)
                {
                    model.ls_siv = _P_Reportes.Sel_Prc_SIV(model.mme_prueba);
                    if (model.ls_siv.Count > 0) { model.mme_datos = DatosPersonales(model.ls_siv[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("HABILIDADES GERENCIALES")).Count() > 0)
                {
                    model.ls_habil_general = _P_Reportes.Sel_Prc_Habil_General(model.mme_prueba);
                    if (model.ls_habil_general.Count > 0) { model.mme_datos = DatosPersonales(model.ls_habil_general[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("MINIMULT")).Count() > 0)
                {
                    model.ls_minimult = _P_Reportes.Sel_Prc_Minimult(model.mme_prueba);
                    if (model.ls_minimult.Count > 0) { model.mme_datos = DatosPersonales(model.ls_minimult[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("INGLES")).Count() > 0)
                {
                    model.ls_ingles = _P_Reportes.Sel_Prc_Ingles(model.mme_prueba);
                    if (model.ls_ingles.Count > 0) { model.mme_datos = DatosPersonales(model.ls_ingles[0]); }
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                throw;
            }

        }

        public ActionResult V_Reporte_Grafico(int id)
        {
            try
            {
                var model = new PersonaModels();

                model.mme_prueba.me_prueba.reporte_conocimiento.nu_id_candidato = id;

                List<E_PruebaCandidato> listPrueba = new List<E_PruebaCandidato>();

                listPrueba = _P_Reportes.Get_Pruebas_Candidato(model.mme_prueba);

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("TEST DE WONDERLIC")).Count() > 0)
                {
                    model.ls_wonderlik = _P_Reportes.Sel_Prc_Wonderlik(model.mme_prueba);
                    if (model.ls_wonderlik.Count > 0) { model.mme_datos = DatosPersonales(model.ls_wonderlik[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("PRUEBA TÉCNICA")).Count() > 0)
                {
                    model.ls_excel = _P_Reportes.Get_Prc_Excel(model.mme_prueba);
                    if (model.ls_excel.Count > 0) { model.mme_datos = DatosPersonales(model.ls_excel[0]); }
                }
                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("GATB")).Count() > 0)
                {
                    model.ls_gatb = _P_Reportes.Sel_Prc_Gatb(model.mme_prueba);
                    if (model.ls_gatb.Count > 0) { model.mme_datos = DatosPersonales(model.ls_gatb[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("TEST DE IC")).Count() > 0)
                {
                    model.ls_ic = _P_Reportes.Sel_Prc_Ic(model.mme_prueba);
                    if (model.ls_ic.Count > 0) { model.mme_datos = DatosPersonales(model.ls_ic[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("DISC")).Count() > 0)
                {
                    model.ls_disc = _P_Reportes.Sel_Prc_Disc(model.mme_prueba);
                    if (model.ls_disc.Count > 0) { model.mme_datos = DatosPersonales(model.ls_disc[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("TEST DE BARSIT")).Count() > 0)
                {
                    model.ls_barsit = _P_Reportes.Sel_Prc_Barsit(model.mme_prueba);
                    if (model.ls_barsit.Count > 0) { model.mme_datos = DatosPersonales(model.ls_barsit[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("TEST DE MONEDAS")).Count() > 0)
                {
                    model.ls_Monedas1 = _P_Reportes.Sel_Prc_Monedas1(model.mme_prueba);
                    if (model.ls_Monedas1.Count > 0) { model.mme_datos = DatosPersonales(model.ls_Monedas1[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("CPS")).Count() > 0)
                {
                    model.ls_cps = _P_Reportes.Sel_Prc_Cps(model.mme_prueba);
                    if (model.ls_cps.Count > 0) { model.mme_datos = DatosPersonales(model.ls_cps[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("KOSTICK")).Count() > 0)
                {
                    model.ls_kostick = _P_Reportes.Sel_Prc_Kostick(model.mme_prueba);
                    if (model.ls_kostick.Count > 0) { model.mme_datos = DatosPersonales(model.ls_kostick[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("IPV")).Count() > 0)
                {
                    model.ls_ipv = _P_Reportes.Sel_Prc_Ipv(model.mme_prueba);
                    if (model.ls_ipv.Count > 0) { model.mme_datos = DatosPersonales(model.ls_ipv[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("ZAVIC")).Count() > 0)
                {
                    model.ls_zavic = _P_Reportes.Sel_Prc_Zavic(model.mme_prueba);
                    if (model.ls_zavic.Count > 0) { model.mme_datos = DatosPersonales(model.ls_zavic[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("DOMINOS")).Count() > 0)
                {
                    model.ls_domino = _P_Reportes.Sel_Prc_Domino(model.mme_prueba);
                    if (model.ls_domino.Count > 0) { model.mme_datos = DatosPersonales(model.ls_domino[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("TIG-1")).Count() > 0)
                {
                    model.ls_tig1_domino = _P_Reportes.Sel_Prc_TIG1_Domino(model.mme_prueba);
                    if (model.ls_tig1_domino.Count > 0) { model.mme_datos = DatosPersonales(model.ls_tig1_domino[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("RAVEN")).Count() > 0)
                {
                    model.ls_raven = _P_Reportes.Sel_Prc_Raven(model.mme_prueba);
                    if (model.ls_raven.Count > 0) { model.mme_datos = DatosPersonales(model.ls_raven[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("ICE BARON")).Count() > 0)
                {
                    model.ls_ice_baron = _P_Reportes.Sel_Prc_Ice_Baron(model.mme_prueba);
                    if (model.ls_ice_baron.Count > 0) { model.mme_datos = DatosPersonales(model.ls_ice_baron[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("NEO PI-R")).Count() > 0)
                {
                    model.ls_neo_pir = _P_Reportes.Sel_Prc_Neo_Pir(model.mme_prueba);
                    if (model.ls_neo_pir.Count > 0) { model.mme_datos = DatosPersonales(model.ls_neo_pir[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("BFQ")).Count() > 0)
                {
                    model.ls_bfq = _P_Reportes.Sel_Prc_BFQ(model.mme_prueba);
                    if (model.ls_bfq.Count > 0) { model.mme_datos = DatosPersonales(model.ls_bfq[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("16 PF-5")).Count() > 0)
                {
                    model.ls_16pf = _P_Reportes.Sel_Prc_16pf(model.mme_prueba);
                    if (model.ls_16pf.Count > 0) { model.mme_datos = DatosPersonales(model.ls_16pf[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("SIV")).Count() > 0)
                {
                    model.ls_siv = _P_Reportes.Sel_Prc_SIV(model.mme_prueba);
                    if (model.ls_siv.Count > 0) { model.mme_datos = DatosPersonales(model.ls_siv[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("HABILIDADES GERENCIALES")).Count() > 0)
                {
                    model.ls_habil_general = _P_Reportes.Sel_Prc_Habil_General(model.mme_prueba);
                    if (model.ls_habil_general.Count > 0) { model.mme_datos = DatosPersonales(model.ls_habil_general[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("MINIMULT")).Count() > 0)
                {
                    model.ls_minimult = _P_Reportes.Sel_Prc_Minimult(model.mme_prueba);
                    if (model.ls_minimult.Count > 0) { model.mme_datos = DatosPersonales(model.ls_minimult[0]); }
                }

                if (listPrueba.Where(Prueba => Prueba.VC_DESC_PRUEBA.Equals("INGLES")).Count() > 0)
                {
                    model.ls_ingles = _P_Reportes.Sel_Prc_Ingles(model.mme_prueba);
                    if (model.ls_ingles.Count > 0) { model.mme_datos = DatosPersonales(model.ls_ingles[0]); }
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                throw;
            }


        }

        public ActionResult ActualizarUsuarios(decimal idPuesto, decimal idUsuario)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 30010;
            /*close>> Información */
            
            try
            {
                var Model = new PersonaModels();

                var Persona = new PersonaBE();
                Persona = Model.Persona;
                Persona.nu_id_puesto = idPuesto;
                Persona.nu_id_usuario = idUsuario;
                Persona.vc_usr_mod = UsuarioSession.Usuario1.vc_cod_usuario;
                Persona.opcion = 3;
                string fechacierre;
                fechacierre = DateTime.Now.ToShortDateString();
                Persona.dt_fec_mod = Convert.ToDateTime(fechacierre);

                Persona = oPersonaRestClient.Save(Persona);

                /*Retornado la vista con el modelo seteado*/
                return AjaxResultSuccess("go");
            }


            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultSuccess(ex.Message.Split('\"')[7].Split('\\')[0]);
            }
        }

        public ActionResult Reactivar(decimal idPuesto, decimal idUsuario)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 30010;
            /*close>> Información */

            try
            {
                var Model = new PersonaModels();

                var Persona = new PersonaBE();
                Persona = Model.Persona;
                Persona.nu_id_puesto = idPuesto;
                Persona.nu_id_usuario = idUsuario;
                Persona.vc_usr_mod = UsuarioSession.Usuario1.vc_cod_usuario;
                Persona.opcion = 2;
                string fechacierre;
                fechacierre = DateTime.Now.ToShortDateString();
                Persona.dt_fec_mod = Convert.ToDateTime(fechacierre);

                Persona = oPersonaRestClient.Save(Persona);

                /*Retornado la vista con el modelo seteado*/
                return AjaxResultSuccess("go");
            }


            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultSuccess(ex.Message.Split('\"')[7].Split('\\')[0]);
            }
        }

        public ActionResult LoadPuesto(string clave, int opcion)
        {
            try
            {
                var model = new PersonaModels();
                model.Persona.vc_desc_puesto = clave;
                model.Persona.nu_id_cuenta = UsuarioSession.Usuario1.nu_id_cuenta;
                model.Persona.nu_id_subcuenta = UsuarioSession.Usuario1.nu_id_subcuenta;
                model.Persona.opcion = 6;
                model.ListaPuesto = oPersonaRestClient.GetByFilters(model.Persona);
                model.Persona.opcion1 = opcion;

                    if (model.ListaPuesto != null)
                    {
                        if (model.ListaPuesto.Count > 0)
                        {
                            List<PersonaBE> Personas = new List<PersonaBE>();
                            decimal? Nf = 0;
                            foreach (var item in model.ListaPuesto)
                            {
                                if (Nf != item.nu_id_puesto)
                                {
                                    Personas.Add(item);
                                    Nf = item.nu_id_puesto;
                                }
                                else
                                {
                                    if (item.nu_id_prueba == 2)
                                    {
                                        Personas[Personas.Count - 1].cantidad_usuario = (item.cantidad_usuario / 5);
                                    }
                                    else
                                    {
                                        Personas[Personas.Count - 1].cantidad_usuario = (item.cantidad_usuario);
                                    }
                                    Personas[Personas.Count - 1].vc_desc_prueba += "/" + item.vc_desc_prueba;
                                }
                            }

                            model.ListaPuesto = Personas;
                        }
                    }
                    return PartialView("VP_ListaPuesto", model);
             }
            catch (Exception Ex)
            {
                ErrorSignal.FromCurrentContext().Raise(Ex); //ELMAH Signaling
                return AjaxResultError(Ex.Message);
            }
        }

        public ActionResult PuestoSelect(int idSeleccionado, int opcion)
        {
            try
            {
                if (idSeleccionado == 0 && UsuarioSession.id != null) { idSeleccionado = int.Parse(UsuarioSession.id+""); }
                var model = new PersonaModels();

                model.Persona.nu_id_cuenta = UsuarioSession.Usuario1.nu_id_cuenta;
                model.Persona.nu_id_subcuenta = UsuarioSession.Usuario1.nu_id_subcuenta;
                model.Persona.nu_id_puesto = idSeleccionado;
                model.Persona.opcion = 7;
                model.ListaPuesto = oPersonaRestClient.GetByFilters(model.Persona);

                if (model.ListaPuesto != null)
                {
                    if (model.ListaPuesto.Count > 0)
                    {
                        model.Puesto = model.ListaPuesto[0];
                    }
                       
                }
                UsuarioSession.id       = model.Puesto.nu_id_puesto;
                UsuarioSession.nombre   = model.Puesto.vc_desc_puesto;
                return PartialView("VP_Puesto", model);
            }
            catch (Exception Ex)
            {
                ErrorSignal.FromCurrentContext().Raise(Ex); //ELMAH Signaling
                return AjaxResultError(Ex.Message);
            }
        }

        public JsonResult AC_Adjuntar_Excel()
        {
            try
            {
                var model = new PersonaModels();
                model.Persona.vc_criterio = "Debe cargar un archivo excel.";
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;
                    try
                    {
                        using (var package = new ExcelPackage(hpf.InputStream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                            for (int row = 2; worksheet.Cells[row, 1].Value != null; row++)
                            {
                                if (worksheet.Cells[row, 1].Value.ToString() == "") { break; }
                                model.Persona = new PersonaBE();

                                if (worksheet.Cells[row, 1].Value != null)
                                    model.Persona.vc_doc_identi = worksheet.Cells[row, 1].Value.ToString();
                                if (worksheet.Cells[row, 2].Value != null)
                                    model.Persona.vc_nombres = worksheet.Cells[row, 2].Value.ToString();
                                if (worksheet.Cells[row, 3].Value != null)
                                    model.Persona.vc_apellidos = worksheet.Cells[row, 3].Value.ToString();
                                if (worksheet.Cells[row, 4].Value != null)
                                    model.Persona.vc_direccion_email = worksheet.Cells[row, 4].Value.ToString();
                                if (worksheet.Cells[row, 5].Value != null)
                                    model.Persona.vc_telefono = worksheet.Cells[row, 5].Value.ToString();

                                model.ls_Persona.Add(model.Persona);
                            }
                        }
                        model.Persona.vc_criterio = "1";
                    }
                    catch (Exception ex)
                    {
                        ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                        return Json(model);
                    }
                }
                return Json(model);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                throw;
            }

        }

        public ActionResult V_Reporte_Ficha(int id)
        {
            try
            {
                var model = new PersonaModels();

                model.mme_prueba.me_prueba.reporte_conocimiento.nu_id_candidato = id;

                model.ls_datosPersonales    = _P_Reportes.Sel_DatosPersonales(model.mme_prueba);
            
                model.ls_conocimiento       = _P_Reportes.Sel_Conocimiento(model.mme_prueba);
           
                model.ls_educacion          = _P_Reportes.Sel_Educacion(model.mme_prueba);

                model.ls_experiencia        = _P_Reportes.Sel_Experiencia(model.mme_prueba);

                model.ls_familiares         = _P_Reportes.Sel_Familiares(model.mme_prueba);

                return View(model);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                throw;
            }

        }

        public ActionResult V_Reporte_Brecha(int id)
        {
            try
            {
                var model = new PersonaModels();

                model.mme_prueba.me_prueba.reporte_conocimiento.nu_id_candidato = id;

                model.ls_disc = _P_Reportes.Sel_Prc_Analisis_Puesto_Candidato(model.mme_prueba);
                if (model.ls_disc.Count > 0) { model.mme_datos = DatosPersonales(model.ls_disc[0]); }

                return View(model);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                throw;
            }
        }

        public ActionResult SelectRevision(decimal idUsuario, String NombreUsuario, String ApellidosUsuario, decimal idPuesto)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 40011;
            /*close>> Información */

            try
            {
                String ServidorDescarga = "";
                var Candidato = new E_Candidato();

                var model = new PersonaModels();
                Candidato.nu_id_usuario = idUsuario;

                /*Consultando traslado por Id*/

                P_Candidato_Evaluacion Seg_Service = new P_Candidato_Evaluacion();

                PersonaModels objPersona = new PersonaModels();
                objPersona.Persona.vc_nombres = NombreUsuario;
                objPersona.Persona.vc_apellidos = ApellidosUsuario;
                objPersona.Ls_RespuestaPreguntaFileo = Seg_Service.Get_RespuestaPreguntaFile(Candidato, out ServidorDescarga);
                objPersona.ServidorDescarga = ServidorDescarga;

                return PartialView("VP_RevisionPruebaCandidato", objPersona);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultError(ex.Message);
            }
        }


        public ActionResult SetRespuestaPregunta(E_RespuestaPreguntaFile obj) {
            try
            {
                String Message_Out = "";
                    P_Candidato_Evaluacion Seg_Service = new P_Candidato_Evaluacion();
                Message_Out = Seg_Service.Set_RespuestaPregunta_Upd(obj);
                return Json( new { Message_Out = Message_Out });
            }
            catch (Exception ex)
            {
                return Json(new { Message_Out = ex.Message });
                throw;
            }
        }
    }
}

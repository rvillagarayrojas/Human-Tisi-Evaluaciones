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
using Siscom.Entity.Persona;
using Siscom.Areas.Global.Models;
using Elmah;

namespace Siscom.Controllers
{
    public class HomeController : BaseController
    {
        ClientUsuarioRestClient oClientUsuarioRestClient;
        PersonaRestClient oPersonaRestClient;
        CandidatosRestClient oCandidatosRestClient;

        public HomeController() 
        {
            oClientUsuarioRestClient    = new ClientUsuarioRestClient();
            oPersonaRestClient          = new PersonaRestClient();
            oCandidatosRestClient       = new CandidatosRestClient();
        }

        public string MostrarEvaluacionesLineal(List<String> Lista) {
            try
            {
                string StringReturn = "";
                Lista = Lista.Distinct().ToList();
                foreach (String cad in Lista)
                {
                    StringReturn = StringReturn + " / " + cad;
                }
                if (StringReturn.Length > 0) {
                    StringReturn = StringReturn.Substring(3, StringReturn.Length - 3);
                }
                return StringReturn;
            }
            catch (Exception Ex)
            {
                ErrorSignal.FromCurrentContext().Raise(Ex); //ELMAH Signaling
                throw;
            }

        }
        
        public ActionResult Index()
        {
            try
            {
                //99471738
                if (UsuarioSession.Usuario1.nu_id_cuenta == null) {
                   return RedirectToAction("Login", "Login");
                }
                var Persona = new PersonaBE();
                var Candidatos = new CandidatoBE();
                var ListaPersona = new List<PersonaBE>();
                String EstadoAnterior = ""; 
                var model = new PersonaModels();
                string hoy;
                hoy = DateTime.Now.ToShortDateString();

                Persona.nu_id_cuenta = UsuarioSession.Usuario1.nu_id_cuenta;
                Persona.nu_id_subcuenta = UsuarioSession.Usuario1.nu_id_subcuenta;
                Persona.nu_id_puesto = null;
                Persona.dt_fec_inicio = DateTime.Now.AddYears(-1);
                Persona.dt_fec_fin = Convert.ToDateTime(hoy);
                Persona.opcion = 0;

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
                            Personas[Personas.Count - 1].vc_desc_prueba = MostrarEvaluacionesLineal(ListaEvaluaciones);// " / " + item.vc_desc_prueba;

                            //if (item.ch_estado_prueba == "PENDIENTE")
                            //{
                            //    Personas[Personas.Count - 1].ch_estado_prueba = item.ch_estado_prueba;
                            //}
                            //if (item.ch_estado_prueba == "EN CURSO")
                            //{
                            //    Personas[Personas.Count - 1].ch_estado_prueba = item.ch_estado_prueba;
                            //}
                            //Personas[Personas.Count - 1].ch_estado_prueba = item.ch_estado_prueba;
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
                    model.ListaPersona = Personas;
                }

                Candidatos.nu_id_cuenta = UsuarioSession.Usuario1.nu_id_cuenta;
                Candidatos.nu_id_subcuenta = UsuarioSession.Usuario1.nu_id_subcuenta;
                Candidatos.opcion = 0;
                model.ls_histo_candidatos = oCandidatosRestClient.GetByFilters(Candidatos);

                Candidatos.nu_id_cuenta = UsuarioSession.Usuario1.nu_id_cuenta;
                Candidatos.nu_id_subcuenta = UsuarioSession.Usuario1.nu_id_subcuenta;
                Candidatos.opcion = 1;
                model.ls_uso_pruebas = oCandidatosRestClient.GetByFilters(Candidatos);

                return View("Inicio",model);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultSuccess(ex.Message.Split('\"')[7].Split('\\')[0]);
            }
        }

        public ActionResult Contrato()
        {
            try
            {
                return View("AcuerdoContrato");
            }
            catch (Exception Ex)
            {
                ErrorSignal.FromCurrentContext().Raise(Ex); //ELMAH Signaling
                throw;
            }
            
        }

        public ActionResult Actualizar(string Contrato)
        {
            try
            {
                var Model = new UsuarioModel();

                var Usuario = new UsuarioBE();
                Usuario = Model.Usuario1;
                Usuario.nu_id_cuenta = UsuarioSession.Usuario1.nu_id_cuenta;
                Usuario.ch_contrato = Contrato;
                Usuario.vc_usr_mod = UsuarioSession.Usuario1.vc_cod_usuario;
                Usuario.opcion = 0;
                string fechacierre;
                fechacierre = DateTime.Now.ToShortDateString();
                Usuario.dt_fec_mod = Convert.ToDateTime(fechacierre);

                Usuario = oClientUsuarioRestClient.Save(Usuario);

                /*Retornado la vista con el modelo seteado*/


                var Persona = new PersonaBE();
                var Candidatos = new CandidatoBE();
                var ListaPersona = new List<PersonaBE>();
                String EstadoAnterior = ""; 
                var model = new PersonaModels();
                string hoy;
                hoy = DateTime.Now.ToShortDateString();

                Persona.nu_id_cuenta = UsuarioSession.Usuario1.nu_id_cuenta;
                Persona.nu_id_subcuenta = UsuarioSession.Usuario1.nu_id_subcuenta;
                Persona.nu_id_puesto = null;
                Persona.dt_fec_inicio = DateTime.Now.AddYears(-1);
                Persona.dt_fec_fin = Convert.ToDateTime(hoy);
                Persona.opcion = 0;

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
                            Personas[Personas.Count - 1].vc_desc_prueba = MostrarEvaluacionesLineal(ListaEvaluaciones);// " / " + item.vc_desc_prueba;

                            //if (item.ch_estado_prueba == "PENDIENTE")
                            //{
                            //    Personas[Personas.Count - 1].ch_estado_prueba = item.ch_estado_prueba;
                            //}
                            //if (item.ch_estado_prueba == "EN CURSO")
                            //{
                            //    Personas[Personas.Count - 1].ch_estado_prueba = item.ch_estado_prueba;
                            //}
                            //    Personas[Personas.Count - 1].ch_estado_prueba = item.ch_estado_prueba;
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
                    model.ListaPersona = Personas;
                }

                UsuarioSession.Usuario1.ch_contrato = Contrato;
                Candidatos.nu_id_cuenta = UsuarioSession.Usuario1.nu_id_cuenta;
                Candidatos.nu_id_subcuenta = UsuarioSession.Usuario1.nu_id_subcuenta;
                Candidatos.opcion = 0;
                model.ls_histo_candidatos = oCandidatosRestClient.GetByFilters(Candidatos);

                Candidatos.nu_id_cuenta = UsuarioSession.Usuario1.nu_id_cuenta;
                Candidatos.nu_id_subcuenta = UsuarioSession.Usuario1.nu_id_subcuenta;
                Candidatos.opcion = 1;
                model.ls_uso_pruebas = oCandidatosRestClient.GetByFilters(Candidatos);

                return View("Inicio", model);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultSuccess(ex.Message.Split('\"')[7].Split('\\')[0]);
            }
        }
       
    }
}

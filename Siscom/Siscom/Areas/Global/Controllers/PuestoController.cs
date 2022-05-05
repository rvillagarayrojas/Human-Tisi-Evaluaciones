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
using Elmah;

using MultiEntidad.A_Seleccion;
using Procedimiento.A_Seleccion;
using System.IO;

namespace Siscom.Areas.Global.Controllers
{
    public class PuestoController : BaseModelController<PuestoModels>
    {
        PuestoRestClient    oPuestoRestClient;
        PersonaRestClient   oPersonaRestClient;

        private readonly P_Reportes _P_Reportes;
        private readonly P_Candidato_Evaluacion _P_Puesto_Evaluacion;
        public PuestoController()
            :base(new PuestoModelsValidator())
        {
            oPuestoRestClient = new PuestoRestClient();
            oPersonaRestClient = new PersonaRestClient();
            this._P_Puesto_Evaluacion = new P_Candidato_Evaluacion();
            this._P_Reportes = new P_Reportes();
        }

        public ActionResult Index()
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 30001;
            /*close>> Información */
            if (UsuarioSession.Usuario1.nu_id_cuenta == null)
            {
                return RedirectToAction("Login", "Login");
            }
            try
            {
                var model = new PuestoModels();

                if (UsuarioSession.Usuario1.nu_id_perfil > 1)
                {
                    model.Persona.vc_criterio = UsuarioSession.Usuario1.vc_desc_cuenta;
                }
                else
                {
                    model.Persona.vc_criterio = "";
                }
                model.Persona.ch_status = "A";

                model.Puesto.vc_desc_cuenta = UsuarioSession.Usuario1.vc_desc_cuenta;
                model.Puesto.vc_desc_sub_cuenta = UsuarioSession.Usuario1.vc_desc_sub_cuenta;

                return View(model);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                throw;
            }


        }

        public ActionResult PerfilDisc()
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 30001;
            /*close>> Información */

            try
            {
                var model = new PuestoModels();

                if (UsuarioSession.Usuario1.nu_id_perfil > 1)
                {
                    model.Persona.vc_criterio = UsuarioSession.Usuario1.vc_desc_cuenta;
                }
                else
                {
                    model.Persona.vc_criterio = "";
                }
                model.Persona.ch_status = "A";

                model.Puesto.vc_desc_cuenta = UsuarioSession.Usuario1.vc_desc_cuenta;
                model.Puesto.vc_desc_sub_cuenta = UsuarioSession.Usuario1.vc_desc_sub_cuenta;

                return View(model);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                throw;
            }
        }
        
        public ActionResult BuscarPruebas(int IdNivelPrueba)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 30005;
            /*close>> Información */

            try
            {
                var Puesto = new PuestoBE();
                var ListaPuesto = new List<PuestoBE>();

                var model = new PuestoModels();
                Puesto.nu_id_nivel_prueba = IdNivelPrueba;

                if (Puesto.nu_id_nivel_prueba == -1)
                {
                    Puesto.nu_id_nivel_prueba = null;
                }
                Puesto.opcion = 1;
                model.ListaPrueba = oPuestoRestClient.GetByFilters(Puesto);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("VP_GrillaPrueba", model);
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

        public ActionResult Guardar(PuestoModels Model)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 30002;
            /*close>> Información */
            
            try
            {
                var Puesto = new PuestoBE();
                Puesto = Model.Puesto;
                Puesto.nu_id_cuenta = UsuarioSession.Usuario1.nu_id_cuenta;
                Puesto.nu_id_subcuenta = UsuarioSession.Usuario1.nu_id_subcuenta;
                Puesto.vc_usr_reg = UsuarioSession.Usuario1.vc_cod_usuario;
                Puesto.ch_status = "A";
                Puesto.opcion = 0;

                string fechacierre;
                fechacierre = DateTime.Now.ToShortDateString();
                Puesto.dt_fec_reg = Convert.ToDateTime(fechacierre);
                Puesto.ch_flag = "1";
                if (Puesto.ch_estado != "P")
                {
                    Puesto.ch_estado = "C";
                }

                Puesto = oPuestoRestClient.Save(Puesto);
                return AjaxResultSuccess(Convert.ToString(Puesto.nu_id_puesto));
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
            Decimal? Idns = 30009;
            /*close>> Información */
            if (UsuarioSession.Usuario1.nu_id_cuenta == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var model = new PuestoModels();
                           
            model.Puesto.vc_desc_cuenta = UsuarioSession.Usuario1.vc_desc_cuenta;
            model.Puesto.vc_desc_sub_cuenta = UsuarioSession.Usuario1.vc_desc_sub_cuenta;

            return View(model);

        }

        public ActionResult LoadSubCuenta(int? cuentaid)
        {
            try
            {
                var model = new PuestoModels();

                model.Persona.nu_id_cuenta = cuentaid;
                if (UsuarioSession.Usuario1.nu_id_perfil != 1)
                {
                    model.Persona.vc_criterio = UsuarioSession.Usuario1.vc_desc_sub_cuenta;
                }
                model.ListaSubCuenta = MetodosApp.ListaTipoSubCuenta(model.Persona);
                return PartialView("VP_SubCuenta", model);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                throw;
            }

        }

        public ActionResult Buscar()
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 30004;
            /*close>> Información */

            try
            {
                var Puesto = new PuestoBE();
                var ListaPuesto = new List<PuestoBE>();

                var model = new PuestoModels();
                Puesto.nu_id_cuenta = UsuarioSession.Usuario1.nu_id_cuenta;
                Puesto.nu_id_subcuenta = UsuarioSession.Usuario1.nu_id_subcuenta;
                
                if (Puesto.nu_id_cuenta == -1)
                {
                    if (UsuarioSession.Usuario1.nu_id_perfil > 1)
                    {
                        Puesto.nu_id_cuenta = UsuarioSession.Usuario1.nu_id_cuenta;
                    }
                    else
                    {
                        Puesto.nu_id_cuenta = null;
                    }
                }
                if (Puesto.nu_id_subcuenta == -1)
                {
                    Puesto.nu_id_subcuenta = null;
                }
                Puesto.opcion = 0;
                model.ListaPuesto = oPuestoRestClient.GetByFilters(Puesto);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("VP_GrillaPuestos", model);
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
        
        public ActionResult BuscarPruebasPuesto(int IdPuesto)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 30006;
            /*close>> Información */

            try
            {
                var Puesto = new PuestoBE();
                var ListaPuesto = new List<PuestoBE>();

                var model = new PuestoModels();
                Puesto.nu_id_puesto = IdPuesto;

                if (Puesto.nu_id_puesto == -1)
                {
                    Puesto.nu_id_puesto = null;
                }
                Puesto.opcion = 2;
                model.ListaPruebas_Asignadas = oPuestoRestClient.GetByFilters(Puesto);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("VP_GrillaPrueba_Asignadas", model);
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

        public ActionResult BuscarCandidatosPuesto(int IdPuesto)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 30006;
            /*close>> Información */

            try
            {
                var Puesto = new PuestoBE();
                var ListaPuesto = new List<PuestoBE>();

                var model = new PuestoModels();
                Puesto.nu_id_puesto = IdPuesto;

                if (Puesto.nu_id_puesto == -1)
                {
                    Puesto.nu_id_puesto = null;
                }
                Puesto.opcion = 4;
                model.ListaCandidatos_Asignadas = oPuestoRestClient.GetByFilters(Puesto);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("VP_GrillaCandidato_Puestos", model);
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

        public ActionResult BuscarPersonalPuesto(int IdPuesto)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 30006;
            /*close>> Información */

            try
            {
                var Puesto = new PuestoBE();
                var ListaPuesto = new List<PuestoBE>();

                var model = new PuestoModels();
                Puesto.nu_id_puesto = IdPuesto;

                if (Puesto.nu_id_puesto == -1)
                {
                    Puesto.nu_id_puesto = null;
                }
                Puesto.opcion = 7;
                model.ListaCandidatos_Asignadas = oPuestoRestClient.GetByFilters(Puesto);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("VP_GrillaPersonal_Puestos", model);
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

        public ActionResult Select(decimal idPuesto)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 30007;
            /*close>> Información */

            try
            {
                var PuestoObj = new PuestoModels();
                var model = new PuestoModels();

                model.ListNivelPrueba = MetodosApp.ListaNivelPrueba(PuestoObj.Puesto);
                model.ListTipoPrueba = MetodosApp.ListaTipoPrueba(PuestoObj.Puesto);

                /*Consultando traslado por Id*/
                model.Puesto = oPuestoRestClient.GetById(idPuesto);


                /*Retornado la vista con el modelo seteado*/
                return View("Select", model); ;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultError(ex.Message);
            }
        }

        public ActionResult SelectCandidatos(decimal idPuesto)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 30008;
            /*close>> Información */

            try
            {
                var PuestoObj = new PuestoModels();
                var model = new PuestoModels();

                /*Consultando puestos por Id*/
                model.Puesto = oPuestoRestClient.GetById(idPuesto);


                /*Retornado la vista con el modelo seteado*/
                return View("Ver", model);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultError(ex.Message);
            }
        }

        public ActionResult SelectPersonal(decimal idPuesto)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 30008;
            /*close>> Información */

            try
            {
                var PuestoObj = new PuestoModels();
                var model = new PuestoModels();

                /*Consultando puestos por Id*/
                model.Puesto = oPuestoRestClient.GetById(idPuesto);


                /*Retornado la vista con el modelo seteado*/
                return View("VerPersonal", model);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultError(ex.Message);
            }
        }

        public ActionResult ActualizarPruebas(decimal idPuesto, decimal idPrueba)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 30010;
            /*close>> Información */

            try
            {
                var Model = new PuestoModels();

                var Puesto = new PuestoBE();
                Puesto = Model.Puesto;
                Puesto.nu_id_puesto = idPuesto;
                Puesto.nu_id_prueba = idPrueba;
                Puesto.vc_usr_mod = UsuarioSession.Usuario1.vc_cod_usuario;
                Puesto.opcion = 1;
                string fechacierre;
                fechacierre = DateTime.Now.ToShortDateString();
                Puesto.dt_fec_mod = Convert.ToDateTime(fechacierre);

                Puesto = oPuestoRestClient.Save(Puesto);

                /*Retornado la vista con el modelo seteado*/
                return AjaxResultSuccess("go");
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultSuccess(ex.Message.Split('\"')[7].Split('\\')[0]);
            }
        }

        public ActionResult Actualizar(PuestoModels Model)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 30010;
            /*close>> Información */

            try
            {
                var Puesto = new PuestoBE();
                Puesto = Model.Puesto;
                Puesto.nu_id_cuenta = UsuarioSession.Usuario1.nu_id_cuenta;
                Puesto.nu_id_subcuenta = UsuarioSession.Usuario1.nu_id_subcuenta;
                Puesto.vc_usr_mod = UsuarioSession.Usuario1.vc_cod_usuario;
                Puesto.ch_status = "A";
                Puesto.opcion = 2;
                string fechacierre;
                fechacierre = DateTime.Now.ToShortDateString();
                Puesto.dt_fec_reg = Convert.ToDateTime(fechacierre);
                Puesto.dt_fec_mod = Convert.ToDateTime(fechacierre);

                Puesto.ch_flag = "1";

                Puesto = oPuestoRestClient.Save(Puesto);
                return AjaxResultSuccess("go");
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultSuccess(ex.Message.Split('\"')[7].Split('\\')[0]);
            }
        }

        public ActionResult ActualizarUsuarios(decimal idPuesto, decimal idUsuario)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 30010;
            /*close>> Información */


            try
            {
                var Model = new PuestoModels();

                var Puesto = new PuestoBE();
                Puesto = Model.Puesto;
                Puesto.nu_id_puesto = idPuesto;
                Puesto.nu_id_usuario = idUsuario;
                Puesto.vc_usr_mod = UsuarioSession.Usuario1.vc_cod_usuario;
                Puesto.opcion = 3;
                string fechacierre;
                fechacierre = DateTime.Now.ToShortDateString();
                Puesto.dt_fec_mod = Convert.ToDateTime(fechacierre);

                Puesto = oPuestoRestClient.Save(Puesto);

                /*Retornado la vista con el modelo seteado*/
                return AjaxResultSuccess("go");
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultSuccess(ex.Message.Split('\"')[7].Split('\\')[0]);
            }
        }

        public ActionResult ActualizarDatosUsuarios(PuestoModels Model)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 30010;
            /*close>> Información */
            
            try
            {
                var Puesto = new PuestoBE();
                Puesto = Model.Puesto;
                Puesto.vc_usr_reg = UsuarioSession.Usuario1.vc_cod_usuario;
                Puesto.opcion = 4;
                string fechacierre;
                fechacierre = DateTime.Now.ToShortDateString();
                Puesto.dt_fec_reg = Convert.ToDateTime(fechacierre);

                Puesto = oPuestoRestClient.Save(Puesto);

                /*Retornado la vista con el modelo seteado*/
                return AjaxResultSuccess("go");
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultSuccess(ex.Message.Split('\"')[7].Split('\\')[0]);
            }
        }

        public ActionResult Reporte()
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 30009;
            /*close>> Información */

            try
            {
                var model = new PuestoModels();

                return View(model);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                throw;
            }
        }

        public ActionResult BuscarReporte()
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 30004;
            /*close>> Información */

            try
            {
                var Puesto = new PuestoBE();
                var ListaPuesto = new List<PuestoBE>();

                var model = new PuestoModels();
                Puesto.nu_id_cuenta = UsuarioSession.Usuario1.nu_id_cuenta;
                Puesto.nu_id_subcuenta = UsuarioSession.Usuario1.nu_id_subcuenta;
                       
                    if (UsuarioSession.Usuario1.nu_id_perfil == 1)
                    {
                    
                        Puesto.nu_id_subcuenta = null;
                    }
                           
                Puesto.opcion = 6;
                model.ListaPuesto = oPuestoRestClient.GetByFilters(Puesto);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("VP_GrillaReporte", model);
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

        public ActionResult ActualizarPuestos(decimal idPuesto)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 30010;
            /*close>> Información */

            try
            {
                var Model = new PuestoModels();

                var Puesto = new PuestoBE();
                Puesto = Model.Puesto;
                Puesto.nu_id_puesto = idPuesto;
                Puesto.vc_usr_mod = UsuarioSession.Usuario1.vc_cod_usuario;
                Puesto.opcion = 5;
                string fechacierre;
                fechacierre = DateTime.Now.ToShortDateString();
                Puesto.dt_fec_mod = Convert.ToDateTime(fechacierre);

                Puesto = oPuestoRestClient.Save(Puesto);

                /*Retornado la vista con el modelo seteado*/
                return AjaxResultSuccess("go");
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultSuccess(ex.Message.Split('\"')[7].Split('\\')[0]);
            }
        }

        public ActionResult ActualizarPuestosTipo(decimal idPuesto)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 30010;
            /*close>> Información */

            try
            {
                var Model = new PuestoModels();

                var Puesto = new PuestoBE();
                Puesto = Model.Puesto;
                Puesto.nu_id_puesto = idPuesto;
                Puesto.vc_usr_mod = UsuarioSession.Usuario1.vc_cod_usuario;
                Puesto.opcion = 6;
                string fechacierre;
                fechacierre = DateTime.Now.ToShortDateString();
                Puesto.dt_fec_mod = Convert.ToDateTime(fechacierre);

                Puesto = oPuestoRestClient.Save(Puesto);

                /*Retornado la vista con el modelo seteado*/
                return AjaxResultSuccess("go");
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultSuccess(ex.Message.Split('\"')[7].Split('\\')[0]);
            }
        }

        public ActionResult GuardarUsuarios(PersonaModels Model)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 40002;
            /*close>> Información */
            try
            {
            //    var Persona = new PersonaBE();
            //    Persona = Model.Persona;
            //    Persona.vc_usr_reg = UsuarioSession.Usuario1.vc_cod_usuario;
            //    Persona.ch_status = "A";
            //    string fechacierre;
            //    fechacierre = DateTime.Now.ToShortDateString();
            //    Persona.dt_fec_reg = Convert.ToDateTime(fechacierre);
            //    Persona.nu_id_perfil = 6;

            //    Persona = oPersonaRestClient.Save(Persona);
                Model.mme_datos.me_prueba.persona.vc_usr_reg = UsuarioSession.Usuario1.vc_cod_usuario;
                Model.mme_datos.me_prueba.persona.ch_status = "A";
                Model.mme_datos.me_prueba.persona.dt_fec_reg = Convert.ToDateTime(DateTime.Now);
                Model.mme_datos.me_prueba.persona.nu_id_perfil = 6;

                _P_Puesto_Evaluacion.Ins_Candidato(Model.mme_datos);

                return AjaxResultSuccess("go");
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultSuccess(ex.Message.Split('\"')[7].Split('\\')[0]);
            }
        }

        public ActionResult EliminarPuesto(decimal idPuesto)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 30002;
            /*close>> Información */
            
            try
            {
                var Model = new PuestoModels();

                var Puesto = new PuestoBE();
                Puesto = Model.Puesto;
                Puesto.nu_id_puesto = idPuesto;
                Puesto.vc_usr_mod = UsuarioSession.Usuario1.vc_cod_usuario;
                Puesto.opcion = 7;
                string fechacierre;
                fechacierre = DateTime.Now.ToShortDateString();
                Puesto.dt_fec_mod = Convert.ToDateTime(fechacierre);

                Puesto = oPuestoRestClient.Save(Puesto);
                return Json(new { cod_error = Puesto.nu_cod_error, descrip_error = Puesto.vc_desc_error }); ;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultSuccess(ex.Message.Split('\"')[7].Split('\\')[0]);
            }
        }

        
        public ActionResult V_Reporte_Analisis_Brechas(int id)
        {
            try
            {
                var model = new PuestoModels();

                model.mme_prueba.me_prueba.reporte_conocimiento.nu_id_puesto = id;

                model.ls_disc = _P_Reportes.Sel_Prc_Analisis_Puesto(model.mme_prueba);
                if (model.ls_disc.Count > 0) { model.mme_datos = DatosPersonales(model.ls_disc[0]); }
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
            try
            {
                return Datos;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                throw;
            }
            
        }

        public ActionResult LoadFile(string idPuesto,string idPrueba) {
            // Checking no of files injected in Request object
            if (Request.Files.Count > 0)
            {
                try
                {
                    // Get all files from Request object
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + “Uploads/”;
                        //string filename = Path.GetFileName(Request.Files[i].FileName);
                        HttpPostedFileBase file = files[i];
                        string fname;
                        // Checking for Internet Explorer
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];    
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        fname = "Prueba_" + idPuesto + idPrueba + Path.GetExtension(fname);
                    // Get the complete folder path and store the file inside it.
                    var FolderPath = Server.MapPath("~/Archivos/");
                    if (!Directory.Exists(FolderPath))
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(FolderPath);
                    }
                    fname = Path.Combine(FolderPath, fname);
                    file.SaveAs(fname);
                    }
                    // Returns message that successfully uploaded
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                    {
                        return Json("Error occurred.Error details: " +ex.Message);
                    }
                }
                else
                {
                    return Json("No files selected.");
                }
            //return Json(new { idPuesto = idPuesto, estado = "OK" }); ;
        }
    }
}

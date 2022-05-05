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

namespace Siscom.Areas.Global.Controllers
{
    public class SubCuentasController : BaseModelController<SubCuentasModels>
    {
        SubCuentaRestClient oSubCuentaRestClient;

        public SubCuentasController()
            :base(new SubCuentasModelsValidator())
        {
            oSubCuentaRestClient = new SubCuentaRestClient();
        }
        
        public ActionResult Index()
        {
            /*open>>> Información: Código de identificación estándar */
                      Decimal? Idns = 20001;
            /*close>> Información */
            if (UsuarioSession.Usuario1.nu_id_cuenta == null)
            {
                return RedirectToAction("Login", "Login");
            }
            try
            {
                var SubCuentaObj = new SubCuentasModels();

                var model = new SubCuentasModels();
                if (UsuarioSession.Usuario1.nu_id_perfil > 1)
                {
                    SubCuentaObj.Persona.vc_criterio = UsuarioSession.Usuario1.vc_desc_cuenta;
                }
                else
                {
                    SubCuentaObj.Persona.vc_criterio = "";
                }
                SubCuentaObj.Persona.ch_status = "A";
                model.SubCuenta.vc_desc_cuenta = UsuarioSession.Usuario1.vc_desc_cuenta;

                return View(model);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling          
                throw;
            }

        }

        public ActionResult Guardar(SubCuentasModels Model)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 20002;
            /*close>> Información */


            try
            {
                var SubCuenta = new SubCuentaBE();
                SubCuenta = Model.SubCuenta;
                SubCuenta.vc_usr_reg = UsuarioSession.Usuario1.vc_cod_usuario;
                SubCuenta.ch_status = "A";
                string fechacierre;
                fechacierre = DateTime.Now.ToShortDateString();
                SubCuenta.dt_fec_reg = Convert.ToDateTime(fechacierre);
                SubCuenta.nu_id_cuenta = UsuarioSession.Usuario1.nu_id_cuenta;
                SubCuenta.nu_id_perfil = 2;
                SubCuenta.opcion = 0;
                SubCuenta = oSubCuentaRestClient.Save(SubCuenta);
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
            Decimal? Idns = 20003;
            /*close>> Información */
            if (UsuarioSession.Usuario1.nu_id_cuenta == null)
            {
                return RedirectToAction("Login", "Login");
            }
            try
            {
                var SubCuenta = new SubCuentaBE();
                var ListaSubCuenta = new List<SubCuentaBE>();

                var model = new SubCuentasModels();


                if (UsuarioSession.Usuario1.nu_id_perfil == 2)
                {
                    SubCuenta.nu_id_cuenta = UsuarioSession.Usuario1.nu_id_cuenta;
                    SubCuenta.vc_criterio = UsuarioSession.Usuario1.vc_desc_sub_cuenta;
                }
                else
                {

                    SubCuenta.nu_id_cuenta = null;
                    SubCuenta.vc_criterio = "";
                }
                model.ListaSubCuenta = oSubCuentaRestClient.GetByFilters(SubCuenta);
            
                return View(model);
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
            Decimal? Idns = 20004;
            /*close>> Información */

            try
            {
                var SubCuenta = new SubCuentaBE();
                var ListaSubCuenta = new List<SubCuentaBE>();

                var model = new SubCuentasModels();

                SubCuenta.nu_id_cuenta = UsuarioSession.Usuario1.nu_id_cuenta;

                if (UsuarioSession.Usuario1.nu_id_perfil == 2)
                {
                    SubCuenta.nu_id_cuenta = UsuarioSession.Usuario1.nu_id_cuenta;
                    SubCuenta.vc_criterio = UsuarioSession.Usuario1.vc_desc_sub_cuenta;
                }
                if (UsuarioSession.Usuario1.nu_id_perfil == 1)
                {
                    SubCuenta.nu_id_cuenta = UsuarioSession.Usuario1.nu_id_cuenta;
                    SubCuenta.vc_criterio = "";
                }
                else
                {

                    SubCuenta.nu_id_cuenta = null;
                    SubCuenta.vc_criterio = "";
                }

                model.ListaSubCuenta = oSubCuentaRestClient.GetByFilters(SubCuenta);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("VP_GrillaSubCuentas", model);
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

        public ActionResult Select(int IdSubCuenta)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 20005;
            /*close>> Información */


            try
            {
                var model = new SubCuentasModels();

                /*Consultando traslado por Id*/
                model.SubCuenta = oSubCuentaRestClient.GetById(IdSubCuenta);

                /*Retornado la vista con el modelo seteado*/
                return View("Select", model); ;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultError(ex.Message);
            }
        }

        public ActionResult Ficha()
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 20005;
            /*close>> Información */


            try
            {
                var model = new SubCuentasModels();
                int IdSubCuenta;
                IdSubCuenta = Convert.ToInt32(UsuarioSession.Usuario1.nu_id_subcuenta);
                /*Consultando traslado por Id*/
                model.SubCuenta = oSubCuentaRestClient.GetById(IdSubCuenta);

                /*Retornado la vista con el modelo seteado*/
                return View("Ficha", model); ;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultError(ex.Message);
            }
        }

        public ActionResult Actualizar(SubCuentasModels Model)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 10002;
            /*close>> Información */


            try
            {
                var Subcuenta = new SubCuentaBE();
                Subcuenta = Model.SubCuenta;
                Subcuenta.vc_usr_mod = UsuarioSession.Usuario1.vc_cod_usuario;
                string fechacierre;
                fechacierre = DateTime.Now.ToShortDateString();
                Subcuenta.dt_fec_mod = Convert.ToDateTime(fechacierre);
                Subcuenta.opcion = 1;

                Subcuenta = oSubCuentaRestClient.Save(Subcuenta);
                return AjaxResultSuccess("go");
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultSuccess(ex.Message.Split('\"')[7].Split('\\')[0]);
            }
        }

        public ActionResult ActualizarUsuarios(decimal idUsuario)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 30010;
            /*close>> Información */

            try
            {
                var Model = new SubCuentasModels();

                var Subcuenta = new SubCuentaBE();
                Subcuenta = Model.SubCuenta;
                Subcuenta.nu_id_usuario = idUsuario;
                Subcuenta.vc_usr_mod = UsuarioSession.Usuario1.vc_cod_usuario;
                Subcuenta.opcion = 2;
                string fechacierre;
                fechacierre = DateTime.Now.ToShortDateString();
                Subcuenta.dt_fec_mod = Convert.ToDateTime(fechacierre);

                Subcuenta = oSubCuentaRestClient.Save(Subcuenta);

                /*Retornado la vista con el modelo seteado*/
                return AjaxResultSuccess("go");
            }


            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultSuccess(ex.Message.Split('\"')[7].Split('\\')[0]);
            }
        }
    }
}

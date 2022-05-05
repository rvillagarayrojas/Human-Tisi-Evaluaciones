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
using Procedimiento.A_Seleccion;
using Entidad.A_Seleccion;

namespace Siscom.Areas.Global.Controllers
{
    public class CuentaController : BaseModelController<CuentaModels>
    {
        CuentaRestClient oCuentaRestClient;


       public CuentaController()
            :base(new CuentaModelsValidator())
        {
            oCuentaRestClient = new CuentaRestClient();
        }

        public ActionResult Index()
        {
            if (UsuarioSession.Usuario1.nu_id_cuenta == null)
            {
                return RedirectToAction("Login", "Login");
            }
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 10001;
            /*close>> Información */

            try
            {
                var CuentaObj = new CuentaModels();

                var model = new CuentaModels();
            
                return View(model);
            }
            catch (Exception Ex)
            {
                ErrorSignal.FromCurrentContext().Raise(Ex); //ELMAH Signaling
                throw;
            }

        }

        public ActionResult Guardar(CuentaModels Model)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 10002;
            /*close>> Información */

            try
            {
                var Cuenta = new CuentaBE();
                Cuenta = Model.Cuenta;
                Cuenta.vc_usr_reg = UsuarioSession.Usuario1.vc_cod_usuario;
                Cuenta.ch_status = "A";
                string fechacierre;
                fechacierre = DateTime.Now.ToShortDateString();
                Cuenta.dt_fec_reg = Convert.ToDateTime(fechacierre);
                Cuenta.nu_id_perfil = 1;
                Cuenta.opcion = 0;
                Cuenta = oCuentaRestClient.Save(Cuenta);
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
            Decimal? Idns = 10003;
            /*close>> Información */
            if (UsuarioSession.Usuario1.nu_id_cuenta == null)
            {
                return RedirectToAction("Login", "Login");
            }
            try
            {
                var Cuenta = new CuentaBE();
                var ListaCuenta = new List<CuentaBE>();

                var model = new CuentaModels();
                Cuenta.nu_id_cuenta = UsuarioSession.Usuario1.nu_id_cuenta;

                //if (Cuenta.nu_id_cuenta != 1)
                //{
                //    Cuenta.nu_id_cuenta = null;
                //}
                model.ListaCuenta = oCuentaRestClient.GetByFilters(Cuenta);

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
            Decimal? Idns = 10004;
            /*close>> Información */

            try
            {
                var Cuenta = new CuentaBE();
                var ListaCuenta = new List<CuentaBE>();

                var model = new CuentaModels();
                Cuenta.nu_id_cuenta = UsuarioSession.Usuario1.nu_id_cuenta;

                //if (Cuenta.nu_id_cuenta != 1)
                //{
                //    Cuenta.nu_id_cuenta = null;
                //}
                model.ListaCuenta = oCuentaRestClient.GetByFilters(Cuenta);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("VP_GrillaCuenta", model);
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

        public ActionResult Select(int idCuenta)
        {

            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 10005;
            /*close>> Información */


            try
            {
                var model = new CuentaModels();

                /*Consultando traslado por Id*/
                model.Cuenta = oCuentaRestClient.GetById(idCuenta);

                /*Retornado la vista con el modelo seteado*/
                return View("Select", model); ;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultError(ex.Message);
            }
        }

        public ActionResult Actualizar(CuentaModels Model)
        {
            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 10002;
            /*close>> Información */


            try
            {
                var Cuenta = new CuentaBE();
                Cuenta = Model.Cuenta;
                Cuenta.vc_usr_mod = UsuarioSession.Usuario1.vc_cod_usuario;
                string fechacierre;
                fechacierre = DateTime.Now.ToShortDateString();
                Cuenta.dt_fec_mod = Convert.ToDateTime(fechacierre);
                Cuenta.opcion = 1;

                Cuenta = oCuentaRestClient.Save(Cuenta);
                return AjaxResultSuccess("go");
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultSuccess(ex.Message.Split('\"')[7].Split('\\')[0]);
            }
        }

        public ActionResult Ficha()
        {

            /*open>>> Información: Código de identificación estándar */
            Decimal? Idns = 10005;
            /*close>> Información */


            try
            {
                var model = new CuentaModels();

                /*Consultando traslado por Id*/
                int idCuenta;
                idCuenta = Convert.ToInt32(UsuarioSession.Usuario1.nu_id_cuenta);
                model.Cuenta = oCuentaRestClient.GetById(idCuenta);

                /*Retornado la vista con el modelo seteado*/
                return View("Ficha", model); ;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                return AjaxResultError(ex.Message);
            }
        }

        public ActionResult ChangePassword()
        {
            if (UsuarioSession.Usuario1.nu_id_cuenta == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        public ActionResult Cambiar(E_Candidato_Evaluacion pCuenta)
        {
            try
            {
                P_Candidato_Evaluacion obj = new P_Candidato_Evaluacion();
               pCuenta.vc_cod_usuario = UsuarioSession.Usuario1.vc_cod_usuario;

                string hoy;
                hoy = DateTime.Now.ToShortDateString();
                E_Candidato_Evaluacion output = new E_Candidato_Evaluacion();
                output = obj.ChangePassword(pCuenta);
                return AjaxResultSuccess(output.vc_Mensaje_Out);
            }
            catch (Exception Ex)
            {
                ErrorSignal.FromCurrentContext().Raise(Ex); //ELMAH Signaling
                return AjaxResultError(Ex.Message);
            }
        }
    }
}

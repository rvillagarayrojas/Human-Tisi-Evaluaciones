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
using Elmah;

namespace Siscom.Controllers
{
    public class LoginController : BaseController
    {
        ClientUsuarioRestClient oClientUsuarioRestClient; 
        public LoginController() 
        {
            try
            {
                oClientUsuarioRestClient = new ClientUsuarioRestClient();
            }
            catch (Exception Ex)
            {
                ErrorSignal.FromCurrentContext().Raise(Ex); //ELMAH Signaling
                throw;
            }
            
        }
        
        public ActionResult Login()
        {
            try
            {
                UsuarioModel model = new UsuarioModel();
                return View(model);
            }
            catch (Exception Ex)
            {
                ErrorSignal.FromCurrentContext().Raise(Ex); //ELMAH Signaling
                throw;
            }
        }

        public ActionResult IniciarSesion(UsuarioBE Usuario1)
        {
            try
            {
                var model = new UsuarioModel();
                string hoy;
                string mensaje = "";
                hoy = DateTime.Now.ToShortDateString();
                model.ListaUsuario = oClientUsuarioRestClient.Select(Usuario1);
                if(model.ListaUsuario.Count > 0)
                {
                    
                    foreach (var item in model.ListaUsuario)
                    { string fecha = item.dt_fecha_fin.Value.ToString("dd/MM/yyyy");

                        
                        if (item.nu_id_perfil != 1)
                        {
                            if (Convert.ToDateTime(fecha) < Convert.ToDateTime(hoy))
                            {
                                mensaje = mensaje + "error";
                            }
                            else
                            {
                                if (item.ch_contrato != "A")
                                {

                                    mensaje = mensaje + "error";
                                }
                                else
                                {
                                    UsuarioSession.Usuario1 = item;
                                    mensaje = mensaje + "go";
                                }

                            }
                        }
                        else
                        {
                            if (Convert.ToDateTime(fecha) < Convert.ToDateTime(hoy))
                            {
                                mensaje = mensaje + "error";
                            }
                            else
                            {
                                if (item.ch_contrato != "A")
                                {

                                    mensaje = mensaje + "acuerdo";
                                }
                                else
                                {
                                    UsuarioSession.Usuario1 = item;
                                    mensaje = mensaje + "go";
                                }

                            }
                        }
                       
                        
                    }
                    return AjaxResultSuccess(mensaje);
                }
                else
                {

                    return AjaxResultSuccess(mensaje);
                }
            }
            catch (Exception Ex)
            {
                ErrorSignal.FromCurrentContext().Raise(Ex); //ELMAH Signaling
                return AjaxResultError(Ex.Message);
            }
        }

        public ActionResult PageRestart(UsuarioBE Usuario1)
        {
            return View();
        }

        public ActionResult LogOut(UsuarioBE Usuario1)
        {
            Session[UsuarioModel.SessionName]=null;

            return RedirectToAction("Login","Login");
        }
    }
}

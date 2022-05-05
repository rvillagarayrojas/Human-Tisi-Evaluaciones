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
using Siscom.Models.Validator;
using Elmah;

namespace Siscom.Controllers
{
    public class CambioContrasenaController : BaseModelController<CambioContrasenaModel>
    {
        ClientUsuarioRestClient oClientUsuarioRestClient;

        public CambioContrasenaController()
            : base(new CambioContrasenaModelValidator())
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


        //public ActionResult CambioContrasena()
        //{
        //     var Usuario = new UsuarioBE();
        //        var ListUsuario = new List<UsuarioBE>();
        //        var model = new CambioContrasenaModel();
        //        string co_empr, usuario;

        //        Usuario.co_empr = UsuarioSession.Usuario1.co_empr;
        //        Usuario.usuario = UsuarioSession.Usuario1.usuario;
        //        co_empr = Usuario.co_empr;
        //        usuario = Usuario.usuario;

        //        model.LstUsuario = oClientUsuarioRestClient.Select(Usuario);

        //        cacheProvider.Set(model.Id.Value.ToString(), model);

        //         if (Request.IsAjaxRequest())
        //    {
        //        return PartialView("CambioContraseña", model);
        //    }
        //    else
        //    {
        //        return View(model);
        //    }
        //}

        //public ActionResult Guardar(string contra, string nueva_contra)
        //{
        //    var Usuario = new UsuarioBE();
        //    var ListUsuario = new List<UsuarioBE>();
        //    var model = new CambioContrasenaModel();

        //    Usuario.co_empr = UsuarioSession.Usuario1.co_empr;
        //    Usuario.usuario = UsuarioSession.Usuario1.usuario;
        //    if (contra != UsuarioSession.Usuario1.contrasena) {
        //        return AjaxResultSuccess("La contraseña actual no coincide.");
        //    }
        //    Usuario.contrasena = contra;
        //    Usuario.nueva_contrasena = nueva_contra;

        //    model.LstUsuario = oClientUsuarioRestClient.Select1(Usuario);
        //    if (model.LstUsuario.Count > 0)
        //    {
        //        foreach (var item in model.LstUsuario)
        //        {
        //            UsuarioSession.Usuario1 = item;
        //        }
        //        return AjaxResultSuccess("go");
        //    }
        //    else
        //    {
        //        return AjaxResultSuccess("La usuario o la contraseña no coincide.");
        //    }
        //}

    }
}

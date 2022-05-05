using System.Web;
using System.Web.Mvc;
using Siscom.Models;

namespace Siscom.Web.Controllers
{
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    { 
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ctx = HttpContext.Current;

            // check if session is supported
            if (ctx.Session[UsuarioModel.SessionName] == null
                && (filterContext.ActionDescriptor).ActionName != "Login"
                && (filterContext.ActionDescriptor).ActionName != "LogOn")
            {
                //ctx.Response.Redirect("~/Seguridad/Login");
                ctx.Response.Redirect("/");
            }

            //if (false)
            //{
            //    ctx.Response.Redirect("~/Seguridad/Login");
            //}               

            base.OnActionExecuting(filterContext);
        }
    } 
}

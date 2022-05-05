using System.Web.Mvc;
using Siscom.Models.Base;
using Siscom.Models;
using Siscom.WebLib.MvcShared;


namespace Siscom.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        protected BaseModel _model = null;

        public UsuarioModel UsuarioSession
        {
            get
            {
                if (Session[UsuarioModel.SessionName] == null)
                    Session[UsuarioModel.SessionName] = new UsuarioModel();
                return (UsuarioModel)Session[UsuarioModel.SessionName];
            }
            set
            {
                Session[UsuarioModel.SessionName] = value;
            }
        }

        private ICacheProvider _cacheProvider = null;

        public ICacheProvider cacheProvider
        {
            get { return _cacheProvider ?? (_cacheProvider = new CacheProvider()); }
            set { _cacheProvider = value; }
        }

        public JsonResult AjaxResultSuccessNoParam()
        {
            return Json(
              new
              {
                  result = true
              });
        }

        public JsonResult AjaxResultSuccess(string message = null)
        {
            return Json(
                new
                {
                    result = true,
                    message = message,
                    JsonRequestBehavior.AllowGet
                });
        }

        public JsonResult AjaxResultError(string errorMessage = null)
        {
            return Json(
              new
              {
                  result = false,
                  errorMessage = errorMessage
              });
        }
    }
}

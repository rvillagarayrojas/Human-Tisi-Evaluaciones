using Siscom.Entity.Global;
using Siscom.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Net.Http;
using System.Net;
using System.Web.Http.Results;

namespace Siscom.Service.Areas.Global.Controllers
{
    [RoutePrefix("global_api/TipoCuenta")]
    public class TipoCuentaController : ApiController
    {
        private readonly TipoCuentaBL _TipoCuentaBL;

       public TipoCuentaController()
        {
            _TipoCuentaBL = new TipoCuentaBL();
        }

        /// <summary>
        /// Obtener todos las Provicias
        /// </summary>
        /// <remarks>Obtener todas los Provincias</remarks>
        [Route("")]
        [ResponseType(typeof(List<TipoCuentaBE>))]
        public IHttpActionResult Get()
        {
            var tipoCuentaList = _TipoCuentaBL.List(new TipoCuentaBE());
            return Ok(tipoCuentaList);
        }

        /// <summary>
        /// Obtener TipoMoneda por filtros
        /// </summary>
        /// <remarks>Obtener TipoMoneda por filtros</remarks> 
        [Route("")]
        [ResponseType(typeof(List<TipoCuentaBE>))]
        public IHttpActionResult Post(TipoCuentaBE filters)
        {
            var tipoCuentaList = _TipoCuentaBL.List(filters);
            return Ok(tipoCuentaList);
        }
    }
}

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
    [RoutePrefix("global_api/TipoSubcuenta")]
    public class TipoSubCuentaController : ApiController
    {
        private readonly TipoSubcuentaBL _TipoSubcuentaBL;

        public TipoSubCuentaController()
        {
            _TipoSubcuentaBL = new TipoSubcuentaBL();
        }

        /// <summary>
        /// Obtener todos las Provicias
        /// </summary>
        /// <remarks>Obtener todas los Provincias</remarks>
        [Route("")]
        [ResponseType(typeof(List<TipoSubCuentaBE>))]
        public IHttpActionResult Get()
        {
            var tipoPerfilList = _TipoSubcuentaBL.List(new TipoSubCuentaBE());
            return Ok(tipoPerfilList);
        }

        /// <summary>
        /// Obtener TipoMoneda por filtros
        /// </summary>
        /// <remarks>Obtener TipoMoneda por filtros</remarks> 
        [Route("")]
        [ResponseType(typeof(List<TipoSubCuentaBE>))]
        public IHttpActionResult Post(TipoSubCuentaBE filters)
        {
            var tipoSubCuentaList = _TipoSubcuentaBL.List(filters);
            return Ok(tipoSubCuentaList);
        }
    }
}

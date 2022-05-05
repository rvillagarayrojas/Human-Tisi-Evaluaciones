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
    [RoutePrefix("global_api/TipoPuesto")]
    public class TipoPuestoController : ApiController
    {
        private readonly TipoPuestoBL _TipoPuestoBL;

        public TipoPuestoController()
        {
            _TipoPuestoBL = new TipoPuestoBL();
        }

        /// <summary>
        /// Obtener todos las Provicias
        /// </summary>
        /// <remarks>Obtener todas los Provincias</remarks>
        [Route("")]
        [ResponseType(typeof(List<TipoPuestoBE>))]
        public IHttpActionResult Get()
        {
            var tipoPerfilList = _TipoPuestoBL.List(new TipoPuestoBE());
            return Ok(tipoPerfilList);
        }

        /// <summary>
        /// Obtener TipoMoneda por filtros
        /// </summary>
        /// <remarks>Obtener TipoMoneda por filtros</remarks> 
        [Route("")]
        [ResponseType(typeof(List<TipoPuestoBE>))]
        public IHttpActionResult Post(TipoPuestoBE filters)
        {
            var tipoSubCuentaList = _TipoPuestoBL.List(filters);
            return Ok(tipoSubCuentaList);
        }

    }
}

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
    [RoutePrefix("global_api/TipoPrueba")]
    public class TipoPruebaController : ApiController
    {
        private readonly TipoPruebaBL _TipoPruebaBL;

        public TipoPruebaController()
        {
            _TipoPruebaBL = new TipoPruebaBL();
        }

        /// <summary>
        /// Obtener todos las Provicias
        /// </summary>
        /// <remarks>Obtener todas los Provincias</remarks>
        [Route("")]
        [ResponseType(typeof(List<TipoPruebaBE>))]
        public IHttpActionResult Get()
        {
            var tipoPruebaList = _TipoPruebaBL.List(new TipoPruebaBE());
            return Ok(tipoPruebaList);
        }

        /// <summary>
        /// Obtener TipoMoneda por filtros
        /// </summary>
        /// <remarks>Obtener TipoMoneda por filtros</remarks> 
        [Route("")]
        [ResponseType(typeof(List<TipoPruebaBE>))]
        public IHttpActionResult Post(TipoPruebaBE filters)
        {
            var tipoPruebaList = _TipoPruebaBL.List(filters);
            return Ok(tipoPruebaList);
        }
    }
}

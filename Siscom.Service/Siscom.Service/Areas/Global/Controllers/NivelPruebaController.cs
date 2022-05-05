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
    [RoutePrefix("global_api/NivelPrueba")]
    public class NivelPruebaController : ApiController
    {
        private readonly NivelPruebaBL _NivelPruebaBL;

       public NivelPruebaController()
        {
            _NivelPruebaBL = new NivelPruebaBL();
        }

        /// <summary>
        /// Obtener todos las Provicias
        /// </summary>
        /// <remarks>Obtener todas los Provincias</remarks>
        [Route("")]
        [ResponseType(typeof(List<NivelPruebaBE>))]
        public IHttpActionResult Get()
        {
            var tipoPruebaList = _NivelPruebaBL.List(new NivelPruebaBE());
            return Ok(tipoPruebaList);
        }

        /// <summary>
        /// Obtener TipoMoneda por filtros
        /// </summary>
        /// <remarks>Obtener TipoMoneda por filtros</remarks> 
        [Route("")]
        [ResponseType(typeof(List<NivelPruebaBE>))]
        public IHttpActionResult Post(NivelPruebaBE filters)
        {
            var tipoPruebaList = _NivelPruebaBL.List(filters);
            return Ok(tipoPruebaList);
        }

    }
}

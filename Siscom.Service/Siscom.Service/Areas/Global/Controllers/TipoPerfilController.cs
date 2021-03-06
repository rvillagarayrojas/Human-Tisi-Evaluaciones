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
    [RoutePrefix("global_api/TipoPerfil")]
    public class TipoPerfilController : ApiController
    {
        private readonly TipoPerfilBL _TipoPerfilBL;

        public TipoPerfilController()
        {
            _TipoPerfilBL = new TipoPerfilBL();
        }

        /// <summary>
        /// Obtener todos las Provicias
        /// </summary>
        /// <remarks>Obtener todas los Provincias</remarks>
        [Route("")]
        [ResponseType(typeof(List<TipoPerfilBE>))]
        public IHttpActionResult Get()
        {
            var tipoPerfilList = _TipoPerfilBL.List(new TipoPerfilBE());
            return Ok(tipoPerfilList);
        }

        /// <summary>
        /// Obtener TipoMoneda por filtros
        /// </summary>
        /// <remarks>Obtener TipoMoneda por filtros</remarks> 
        [Route("")]
        [ResponseType(typeof(List<TipoPerfilBE>))]
        public IHttpActionResult Post(TipoPerfilBE filters)
        {
            var tipoPerfilList = _TipoPerfilBL.List(filters);
            return Ok(tipoPerfilList);
        }

    }
}

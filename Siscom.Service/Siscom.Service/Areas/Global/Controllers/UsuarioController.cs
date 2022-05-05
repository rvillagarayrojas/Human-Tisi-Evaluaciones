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
    [RoutePrefix("global_api/Usuarios")]
    public class UsuarioController : ApiController
    {
        private readonly UsuarioBL _UsuarioBL;

        public UsuarioController()
        {
            _UsuarioBL = new UsuarioBL();
        }

        /// <summary>
        /// Obtener usuario por filtros
        /// </summary>
        /// <remarks>Obtener usuario por filtros</remarks> 
        [Route("")]
        [ResponseType(typeof(List<UsuarioBE>))]
        public IHttpActionResult Post(UsuarioBE filters)
        {
            var usuarioList = _UsuarioBL.List(filters);
            return Ok(usuarioList);
        }

        [Route("")]
        [ResponseType(typeof(UsuarioBE))]
        public IHttpActionResult Put(UsuarioBE oItem)
        {
            if (oItem.opcion == 0)
            {
                _UsuarioBL.Update(oItem);
            }
            return Ok(oItem);

        }
    }
}

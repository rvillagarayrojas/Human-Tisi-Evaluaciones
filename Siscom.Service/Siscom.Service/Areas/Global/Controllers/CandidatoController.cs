using Siscom.Entity.Persona;
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
    [RoutePrefix("global_api/Candidatos")]
    public class CandidatoController : ApiController
    {
        private readonly CandidatosBL _CandidatoBL;

        public CandidatoController()
        {
            _CandidatoBL = new CandidatosBL();
        }

       /// <summary>
       /// Obtener cajacierre por filtros
       /// </summary>
       /// <remarks>Obtener cajacierre por filtros</remarks> 
       [Route("")]
       [ResponseType(typeof(List<CandidatoBE>))]
        public IHttpActionResult Post(CandidatoBE filters)
       {
           {
               return Ok(_CandidatoBL.List(filters));
           }
       }

    }
}

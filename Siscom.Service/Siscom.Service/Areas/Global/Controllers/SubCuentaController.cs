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
using Siscom.Entity.Global;

namespace Siscom.Service.Areas.Global.Controllers
{
    [RoutePrefix("global_api/SubCuenta")]
    public class SubCuentaController : ApiController
    {
        private readonly SubCuentaBL _SubCuentaBL;

        public SubCuentaController()
        {
            _SubCuentaBL = new SubCuentaBL();
        }
        /// <summary>
        /// Obtener cajacierre por filtros
        /// </summary>
        /// <remarks>Obtener cajacierre por filtros</remarks> 
        [Route("")]
        [ResponseType(typeof(List<SubCuentaBE>))]
        public IHttpActionResult Post(SubCuentaBE filters)
        {
            {
                return Ok(_SubCuentaBL.List(filters));
            }
        }

        /// <summary>
        /// Agregar o Actualizar Caja Cierre
        /// </summary>
        /// <param name="opcion">opcion</param>
        /// <remarks>Agregar o Actualizar CajaCierre</remarks>
        [Route("")]
        [ResponseType(typeof(SubCuentaBE))]
        public IHttpActionResult Put(SubCuentaBE oItem)
        {
            if (oItem.opcion == 0)
            {
                _SubCuentaBL.Insert(oItem);
            }
            if (oItem.opcion == 1)
            {
                _SubCuentaBL.Update(oItem);
            }
            else
            {
                _SubCuentaBL.UpdateUsuario(oItem); 
            }
            return Ok(oItem);

        }

        /// <summary>
        /// Obtener Subcuentas por id
        /// <param name="idPersona">persona id</param>
        /// <param name="idUsuario">usuario id</param>
        /// </summary>
        /// <remarks>Obtener Usuarios por id</remarks>
        [Route(Name = "GetSubCuentasById")]
        [Route("{idSubCuenta:decimal}")]
        [ResponseType(typeof(SubCuentaBE))]
        public IHttpActionResult Get(decimal idSubCuenta)
        {
            var subcuenta = _SubCuentaBL.Get(new SubCuentaBE()
            {
                nu_id_subcuenta = idSubCuenta
            });

            if (subcuenta == null)
            {
                return NotFound();
            }

            return Ok(subcuenta);
        }
    }
}

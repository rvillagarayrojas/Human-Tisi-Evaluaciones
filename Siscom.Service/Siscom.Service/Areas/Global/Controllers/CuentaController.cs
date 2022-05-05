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
    [RoutePrefix("global_api/Cuenta")]
    public class CuentaController : ApiController
    {
       private readonly CuentaBL _CuentaBL;

       public CuentaController()
        {
            _CuentaBL = new CuentaBL();
        }
       /// <summary>
       /// Obtener cajacierre por filtros
       /// </summary>
       /// <remarks>Obtener cajacierre por filtros</remarks> 
       [Route("")]
       [ResponseType(typeof(List<CuentaBE>))]
       public IHttpActionResult Post(CuentaBE filters)
       {
           {
               return Ok(_CuentaBL.List(filters));
           }
       }

       /// <summary>
       /// Agregar o Actualizar Caja Cierre
       /// </summary>
       /// <param name="opcion">opcion</param>
       /// <remarks>Agregar o Actualizar CajaCierre</remarks>
       [Route("")]
       [ResponseType(typeof(CuentaBE))]
       public IHttpActionResult Put(CuentaBE oItem)
       {
            if (oItem.opcion == 0)
            {
                _CuentaBL.Insert(oItem);
            }else if (oItem.opcion == 2)
            {
                oItem = _CuentaBL.ChangePassword(oItem);
            }
            else
            {
               _CuentaBL.Update(oItem);
           }

           return Ok(oItem);


       }


       /// <summary>
       /// Obtener Usuarios por id
       /// <param name="idPersona">persona id</param>
       /// <param name="idUsuario">usuario id</param>
       /// </summary>
       /// <remarks>Obtener Usuarios por id</remarks>
       [Route(Name = "GetCuentasById")]
       [Route("{idCuenta:int}")]
       [ResponseType(typeof(CuentaBE))]
       public IHttpActionResult Get(int idCuenta)
       {
           var cuenta = _CuentaBL.Get(new CuentaBE()
           {
               nu_id_cuenta = idCuenta
           });

           if (cuenta == null)
           {
               return NotFound();
           }

           return Ok(cuenta);
       }
    }
}

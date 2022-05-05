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
using System.Web.Mvc;

namespace Siscom.Service.Areas.Global.Controllers
{
    [RoutePrefix("global_api/Persona")]
    public class PersonaController : ApiController
    {
        private readonly PersonaBL _PersonaBL;

       public PersonaController()
        {
            _PersonaBL = new PersonaBL();
        }

       /// <summary>
       /// Obtener cajacierre por filtros
       /// </summary>
       /// <remarks>Obtener cajacierre por filtros</remarks> 
       [Route("")]
       [ResponseType(typeof(List<PersonaBE>))]
       public IHttpActionResult Post(PersonaBE filters)
       {
           if (filters.opcion == 11)
           {
               var OutPut = _PersonaBL.ListSeguimiento(filters);
               return Ok(OutPut);
           }
           else
           {
               var OutPut = _PersonaBL.List(filters);
               return Ok(OutPut);
           }
            
       }

       /// <summary>
       /// Agregar o Actualizar Caja Cierre
       /// </summary>
       /// <param name="opcion">opcion</param>
       /// <remarks>Agregar o Actualizar CajaCierre</remarks>
       [Route("")]
       [ResponseType(typeof(PersonaBE))]
       public IHttpActionResult Put(PersonaBE oItem)
       {
           if (oItem.opcion == 5)
           {
               _PersonaBL.Actualizar(oItem);
           }
           else if (oItem.opcion == 3)
           {
               _PersonaBL.Update_Usuario(oItem);
           }

           else if (oItem.opcion == 2)
           {
               _PersonaBL.Reactivar_Usuario(oItem);
           }
           else
           {
               _PersonaBL.Insert(oItem);
           }
           return Ok(oItem);

       }

       /// <summary>
       /// Obtener Usuarios por id
       /// <param name="idPersona">persona id</param>
       /// <param name="idUsuario">usuario id</param>
       /// </summary>
       /// <remarks>Obtener Usuarios por id</remarks>
       //[Route(Name = "GetUsuarioById")]
       //[Route("{idPuesto:decimal}/{idUsuario:decimal}")]
       //[ResponseType(typeof(PersonaBE))]
       //public IHttpActionResult Get(decimal idPuesto, decimal idUsuario)
       //{
       //    var persona = _PersonaBL.Get(new PersonaBE()
       //    {
       //       nu_id_usuario = idUsuario,
       //       nu_id_puesto = idPuesto
       //    });

       //    if (persona == null)
       //    {
       //        return NotFound();
       //    }

       //    return Ok(persona);
       //}
    }
}

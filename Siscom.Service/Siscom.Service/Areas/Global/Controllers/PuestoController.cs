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
    [RoutePrefix("global_api/Puesto")]
    public class PuestoController : ApiController
    {
        private readonly PuestoBL _PuestoBL;

       public PuestoController()
        {
            _PuestoBL = new PuestoBL();
        }
       /// <summary>
       /// Obtener cajacierre por filtros
       /// </summary>
       /// <remarks>Obtener cajacierre por filtros</remarks> 
       [Route("")]
       [ResponseType(typeof(List<PuestoBE>))]
       public IHttpActionResult Post(PuestoBE filters)
       {
           if (filters.opcion == 0)
           {
               return Ok(_PuestoBL.List(filters));
           }
           if (filters.opcion == 1)
           {
               return Ok(_PuestoBL.ListPruebas(filters));
           }
           if (filters.opcion == 2)
           {
               return Ok(_PuestoBL.ListaPruebaPuesto(filters));
           }
           if (filters.opcion == 6)
           {
               return Ok(_PuestoBL.ListaReporte(filters));
           }
           else
           {
               return Ok(_PuestoBL.ListaCandidatosPuesto(filters));
           }
       }

       /// <summary>
       /// Agregar o Actualizar Caja Cierre
       /// </summary>
       /// <param name="opcion">opcion</param>
       /// <remarks>Agregar o Actualizar CajaCierre</remarks>
       [Route("")]
       [ResponseType(typeof(PuestoBE))]
       public IHttpActionResult Put(PuestoBE oItem)
       {
           if (oItem.opcion == 0)
           {
               _PuestoBL.Insert(oItem);
           }
           else if (oItem.opcion == 1)
           {
               _PuestoBL.Update_Estado_Prueba_Puesto(oItem);
           }
           else if (oItem.opcion == 2)
            {
                _PuestoBL.Update(oItem);
            }
           else if (oItem.opcion == 3)
           {
               _PuestoBL.Update_Usuario(oItem);
           }
           else if (oItem.opcion == 4)
           {
               _PuestoBL.UpdateUsuarios(oItem);
           }
           else if (oItem.opcion == 5)
           {
               _PuestoBL.UpdatePuesto(oItem);
           }
           else if (oItem.opcion == 6)
           {
               _PuestoBL.UpdatePuestoTipo(oItem);
           }
           else if (oItem.opcion == 7)
           {
               Tuple<Int32,String> paramOutPut =_PuestoBL.DeletePuesto(oItem);
               oItem.nu_cod_error = paramOutPut.Item1;
               oItem.vc_desc_error = paramOutPut.Item2;
           }
           return Ok(oItem);

       }


       /// <summary>
       /// Obtener Usuarios por id
       /// <param name="idPersona">persona id</param>
       /// <param name="idUsuario">usuario id</param>
       /// </summary>
       /// <remarks>Obtener Usuarios por id</remarks>
       [Route(Name = "GetPuestosById")]
       [Route("{idPuesto:decimal}")]
       [ResponseType(typeof(PuestoBE))]
       public IHttpActionResult Get(decimal idPuesto)
       {
           var puesto = _PuestoBL.Get(new PuestoBE()
           {
               nu_id_puesto = idPuesto
           });

           if (puesto == null)
           {
               return NotFound();
           }

           return Ok(puesto);
       }

    }
}

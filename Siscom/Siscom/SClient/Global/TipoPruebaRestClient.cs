using RestSharp;
using Siscom.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Siscom.Entity.Persona;
using Siscom.Entity.Global;

namespace Siscom.SClient.Global
{
    public class TipoPruebaRestClient
    {
         private readonly RestSharp.RestClient _restTipoPrueba;

         public TipoPruebaRestClient()
        {
            var PtoGlobal = System.Configuration.ConfigurationManager.AppSettings["Pto"];
            _restTipoPrueba = new RestClient("http://localhost:" + PtoGlobal);
        }

         public List<TipoPruebaBE> GetAll()
         {
             var request = new RestRequest("global_api/TipoPrueba", Method.GET);
             var response = _restTipoPrueba.Execute<List<TipoPruebaBE>>(request);

             return response.Data;
         }

         public List<TipoPruebaBE> GetByFilters(PuestoBE tipoprueba)
         {
             var request = new RestRequest("global_api/TipoPrueba", Method.POST) { RequestFormat = DataFormat.Json };
             request.AddBody(tipoprueba);

             var response = _restTipoPrueba.Execute<List<TipoPruebaBE>>(request);

             if (response.StatusCode != HttpStatusCode.OK)
                 throw new Exception(response.Content);

             return response.Data;
         }
    }
}
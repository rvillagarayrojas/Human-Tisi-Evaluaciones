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
    public class CuentaRestClient
    {
         private readonly RestClient _restClient;

         public CuentaRestClient()
        {
            var PtoGlobal = System.Configuration.ConfigurationManager.AppSettings["Pto"];
            _restClient = new RestClient("http://localhost:" + PtoGlobal);
        }

         public CuentaBE Save(CuentaBE cuenta)
        {
            var request = new RestRequest("global_api/Cuenta", Method.PUT) { RequestFormat = DataFormat.Json };
            request.AddBody(cuenta);

            var response = _restClient.Execute<CuentaBE>(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.Content);

            return response.Data;
        }

         public List<CuentaBE> GetByFilters(CuentaBE persona)
         {
             var request = new RestRequest("global_api/Cuenta", Method.POST) { RequestFormat = DataFormat.Json };
             request.AddBody(persona);

             var response = _restClient.Execute<List<CuentaBE>>(request);

             if (response.StatusCode != HttpStatusCode.OK)
                 throw new Exception(response.Content);

             return response.Data;
         }

         public CuentaBE GetById(int idCuenta)
         {
             var request = new RestRequest("global_api/Cuenta/{idCuenta}", Method.GET);
             request.AddParameter("idCuenta", idCuenta, ParameterType.UrlSegment);

             var response = _restClient.Execute<CuentaBE>(request);

             return response.Data;
         }


    }
}
using RestSharp;
using Siscom.Entity.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Siscom.Entity.Persona;

namespace Siscom.SClient.Global
{
    public class TipoSubCuentaRestClient
    {
         private readonly RestSharp.RestClient _restTipoSubCuenta;

         public TipoSubCuentaRestClient()
        {
            var PtoGlobal = System.Configuration.ConfigurationManager.AppSettings["Pto"];
            _restTipoSubCuenta = new RestClient("http://localhost:" + PtoGlobal);
        }

        public List<TipoSubCuentaBE> GetAll()
        {
            var request = new RestRequest("global_api/TipoSubcuenta", Method.GET);
            var response = _restTipoSubCuenta.Execute<List<TipoSubCuentaBE>>(request);

            return response.Data;
        }

        public List<TipoSubCuentaBE> GetByFilters(PersonaBE tiposubcuenta)
        {
            var request = new RestRequest("global_api/TipoSubcuenta", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(tiposubcuenta);

            var response = _restTipoSubCuenta.Execute<List<TipoSubCuentaBE>>(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.Content);

            return response.Data;
        }
    }
}
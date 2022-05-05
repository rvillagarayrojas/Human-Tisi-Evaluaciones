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
    public class SubCuentaRestClient
    {
        private readonly RestClient _restClient;

        public SubCuentaRestClient()
        {
            var PtoGlobal = System.Configuration.ConfigurationManager.AppSettings["Pto"];
            _restClient = new RestClient("http://localhost:" + PtoGlobal);
        }

        public SubCuentaBE Save(SubCuentaBE subcuenta)
        {
            var request = new RestRequest("global_api/SubCuenta", Method.PUT) { RequestFormat = DataFormat.Json };
            request.AddBody(subcuenta);

            var response = _restClient.Execute<SubCuentaBE>(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.Content);

            return response.Data;
        }

        public List<SubCuentaBE> GetByFilters(SubCuentaBE subcuenta)
        {
            var request = new RestRequest("global_api/SubCuenta", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(subcuenta);

            var response = _restClient.Execute<List<SubCuentaBE>>(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.Content);

            return response.Data;
        }

        public SubCuentaBE GetById(decimal idSubCuenta)
        {
            var request = new RestRequest("global_api/SubCuenta/{idSubCuenta}", Method.GET);
            request.AddParameter("idSubCuenta", idSubCuenta, ParameterType.UrlSegment);

            var response = _restClient.Execute<SubCuentaBE>(request);

            return response.Data;
        }
    }
}
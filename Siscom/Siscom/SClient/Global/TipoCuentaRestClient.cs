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
    public class TipoCuentaRestClient
    {
        private readonly RestSharp.RestClient _restTipoCuenta;

        public TipoCuentaRestClient()
        {
            var PtoGlobal = System.Configuration.ConfigurationManager.AppSettings["Pto"];
            _restTipoCuenta = new RestClient("http://localhost:" + PtoGlobal);
        }

        public List<TipoCuentaBE> GetAll()
        {
            var request = new RestRequest("global_api/TipoCuenta", Method.GET);
            var response = _restTipoCuenta.Execute<List<TipoCuentaBE>>(request);

            return response.Data;
        }

        public List<TipoCuentaBE> GetByFilters(PersonaBE oItem)
        {
            var request = new RestRequest("global_api/TipoCuenta", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(oItem);

            var response = _restTipoCuenta.Execute<List<TipoCuentaBE>>(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.Content);

            return response.Data;
        }
    }
}
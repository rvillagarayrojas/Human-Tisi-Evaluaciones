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
    public class TipoPuestoRestClient
    {
        private readonly RestSharp.RestClient _restTipoPuesto;

        public TipoPuestoRestClient()
        {
            var PtoGlobal = System.Configuration.ConfigurationManager.AppSettings["Pto"];
            _restTipoPuesto = new RestClient("http://localhost:" + PtoGlobal);
        }

        public List<TipoPuestoBE> GetAll()
        {
            var request = new RestRequest("global_api/TipoPuesto", Method.GET);
            var response = _restTipoPuesto.Execute<List<TipoPuestoBE>>(request);

            return response.Data;
        }

        public List<TipoPuestoBE> GetByFilters(PersonaBE tipopuesto)
        {
            var request = new RestRequest("global_api/TipoPuesto", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(tipopuesto);

            var response = _restTipoPuesto.Execute<List<TipoPuestoBE>>(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.Content);

            return response.Data;
        }
    }
}
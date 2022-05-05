using RestSharp;
using Siscom.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Siscom.Entity.Persona;

namespace Siscom.SClient.Global
{
    public class CandidatosRestClient
    {
        private readonly RestClient _restClient;

        public CandidatosRestClient()
        {
            var PtoGlobal = System.Configuration.ConfigurationManager.AppSettings["Pto"];
            _restClient = new RestClient("http://localhost:" + PtoGlobal);
        }

        public List<CandidatoBE> GetByFilters(CandidatoBE candidato)
        {
            var request = new RestRequest("global_api/Candidatos", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(candidato);

            var response = _restClient.Execute<List<CandidatoBE>>(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.Content);

            return response.Data;
        }

    }
}
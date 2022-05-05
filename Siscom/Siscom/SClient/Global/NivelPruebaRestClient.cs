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
    public class NivelPruebaRestClient
    {
         private readonly RestSharp.RestClient _restNivelPrueba;

         public NivelPruebaRestClient()
        {
            var PtoGlobal = System.Configuration.ConfigurationManager.AppSettings["Pto"];
            _restNivelPrueba = new RestClient("http://localhost:" + PtoGlobal);
        }

        public List<NivelPruebaBE> GetAll()
        {
            var request = new RestRequest("global_api/NivelPrueba", Method.GET);
            var response = _restNivelPrueba.Execute<List<NivelPruebaBE>>(request);

            return response.Data;
        }

        public List<NivelPruebaBE> GetByFilters(PuestoBE nivelprueba)
        {
            var request = new RestRequest("global_api/NivelPrueba", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(nivelprueba);

            var response = _restNivelPrueba.Execute<List<NivelPruebaBE>>(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.Content);

            return response.Data;
        }
    }
}
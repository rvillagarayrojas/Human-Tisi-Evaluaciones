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
    public class PuestoRestClient
    {
        private readonly RestClient _restClient;

        public PuestoRestClient()
        {
            var PtoGlobal = System.Configuration.ConfigurationManager.AppSettings["Pto"];
            _restClient = new RestClient("http://localhost:" + PtoGlobal);
        }

        public PuestoBE Save(PuestoBE puesto)
        {
            var request = new RestRequest("global_api/Puesto", Method.PUT) { RequestFormat = DataFormat.Json };
            request.AddBody(puesto);

            var response = _restClient.Execute<PuestoBE>(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.Content);

            return response.Data;
        }

        public PuestoBE Eliminar(PuestoBE puesto)
        {
            var request = new RestRequest("global_api/Puesto", Method.PUT) { RequestFormat = DataFormat.Json };
            request.AddBody(puesto);

            var response = _restClient.Execute<PuestoBE>(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.Content);

            return response.Data;
        }

        public List<PuestoBE> GetByFilters(PuestoBE puesto)
         {
             var request = new RestRequest("global_api/Puesto", Method.POST) { RequestFormat = DataFormat.Json };
             request.AddBody(puesto);

             var response = _restClient.Execute<List<PuestoBE>>(request);

             if (response.StatusCode != HttpStatusCode.OK)
                 throw new Exception(response.Content);

             return response.Data;
         }

        public PuestoBE GetById(decimal idPuesto)
         {
             var request = new RestRequest("global_api/Puesto/{idPuesto}", Method.GET);
             request.AddParameter("idPuesto", idPuesto, ParameterType.UrlSegment);

             var response = _restClient.Execute<PuestoBE>(request);

             return response.Data;
         }
    }
}
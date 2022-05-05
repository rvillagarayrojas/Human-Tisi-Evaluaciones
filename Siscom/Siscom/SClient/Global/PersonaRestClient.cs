using RestSharp;
using Siscom.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Siscom.Entity.Persona;

using System.Web.Mvc;

namespace Siscom.SClient.Global
{
    public class PersonaRestClient
    {
        private readonly RestClient _restClient;

        public PersonaRestClient()
        {
            var PtoGlobal = System.Configuration.ConfigurationManager.AppSettings["Pto"];
            _restClient = new RestClient("http://localhost:" + PtoGlobal);
        }

        public PersonaBE Save(PersonaBE persona)
        {
            var request = new RestRequest("global_api/Persona", Method.PUT) { RequestFormat = DataFormat.Json }; 
            request.AddBody(persona);

            var response = _restClient.Execute<PersonaBE>(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.Content);

            return response.Data;
        }

       

        public List<PersonaBE> GetByFilters(PersonaBE persona)
        {
            var request = new RestRequest("global_api/Persona", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(persona);

            var response = _restClient.Execute<List<PersonaBE>>(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.Content);

            return response.Data;
        }

        public PersonaBE GetById(decimal idPrueba, decimal idUsuario)
        {
            var request = new RestRequest("global_api/Persona/{idPrueba}/{idUsuario}", Method.GET);
            request.AddParameter("idPrueba", idPrueba, ParameterType.UrlSegment);
            request.AddParameter("idUsuario", idUsuario, ParameterType.UrlSegment);

            var response = _restClient.Execute<PersonaBE>(request);

            return response.Data;
        }
       
    }
}
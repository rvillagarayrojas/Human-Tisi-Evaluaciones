using RestSharp;
using Siscom.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Siscom.Entity.Global;

namespace Siscom.SClient.Global
{
    public class ClientUsuarioRestClient
    {
        private readonly RestClient _restClient;

        public ClientUsuarioRestClient()
        {
            var PtoGlobal = System.Configuration.ConfigurationManager.AppSettings["Pto"];
            _restClient = new RestClient("http://localhost:" + PtoGlobal);
        }

        public UsuarioBE Save(UsuarioBE usuario)
        {
            var request = new RestRequest("global_api/Usuarios", Method.PUT) { RequestFormat = DataFormat.Json };
            request.AddBody(usuario);

            var response = _restClient.Execute<UsuarioBE>(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.Content);

            return response.Data;
        }

        public List<UsuarioBE> Select(UsuarioBE Usuario)
        {
            var request = new RestRequest("global_api/Usuarios", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(Usuario);
            var response = _restClient.Execute<List<UsuarioBE>>(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content);
            }
            return response.Data;
        }

        public List<UsuarioBE> Select1(UsuarioBE Usuario)
        {
            var request = new RestRequest("global_api/CambioContrasena", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(Usuario);
            var response = _restClient.Execute<List<UsuarioBE>>(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content);
            }
            return response.Data;
        }
    }
}
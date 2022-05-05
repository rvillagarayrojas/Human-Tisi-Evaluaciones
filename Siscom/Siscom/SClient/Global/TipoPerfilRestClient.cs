using RestSharp;
using Siscom.Entity.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Siscom.SClient.Global
{
    public class TipoPerfilRestClient
    {
        private readonly RestSharp.RestClient _restTipoPerfil;

        public TipoPerfilRestClient()
        {
            var PtoGlobal = System.Configuration.ConfigurationManager.AppSettings["Pto"];
            _restTipoPerfil = new RestClient("http://localhost:" + PtoGlobal);
        }

        public List<TipoPerfilBE> GetAll()
        {
            var request = new RestRequest("global_api/TipoPerfil", Method.GET);
            var response = _restTipoPerfil.Execute<List<TipoPerfilBE>>(request);

            return response.Data;
        }

        public List<TipoPerfilBE> GetByFilters(TipoPerfilBE tipoperfil)
        {
            var request = new RestRequest("global_api/TipoPerfil", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(tipoperfil);

            var response = _restTipoPerfil.Execute<List<TipoPerfilBE>>(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.Content);

            return response.Data;
        }
    }
}
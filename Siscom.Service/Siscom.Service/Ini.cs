using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Siscom.Service.App_Start;

[assembly: OwinStartup(typeof(Siscom.Service.Ini))]
namespace Siscom.Service
{
    public class Ini
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            Swashbuckle.Bootstrapper.Init(config);
            app.UseWebApi(config);
        }
    }
}
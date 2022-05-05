using System.Web.Http;
using Siscom.Service.Global;
using WebActivatorEx;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Siscom.Service.Global
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            Swashbuckle.Bootstrapper.Init(GlobalConfiguration.Configuration);
            SwaggerSpecConfig.Customize(c =>
            {
                c.IncludeXmlComments(GetXmlCommentsPath());
            });
        }

        protected static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\bin\Siscom.Service.XML", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
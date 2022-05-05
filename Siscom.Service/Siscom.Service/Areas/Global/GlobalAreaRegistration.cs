using System.Web.Mvc;

namespace Siscom.Service.Areas.Global
{
    public class GlobalAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Global";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
           context.MapRoute(
                "Global_Usuario",
                "global_api",
                new { controller = "Usuario" }
            );
           context.MapRoute(
               "Global_Persona",
               "global_api",
               new { controller = "Persona" }
           );
           context.MapRoute(
               "Global_Candidatos",
               "global_api",
               new { controller = "Candidatos" }
           );
           context.MapRoute(
              "Global_TipoPerfil",
              "global_api",
              new { controller = "TipoPerfil" }
          );
           context.MapRoute(
             "Global_TipoCuenta",
             "global_api",
             new { controller = "TipoCuenta" }
         );
       }
    }
}

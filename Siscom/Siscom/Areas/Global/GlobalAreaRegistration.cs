using System.Web.Mvc;
using Elmah;

namespace Siscom.Areas.Global
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
            try
            {
                context.MapRoute(
                    "Global_default",
                    "Global/{controller}/{action}/{id}",
                    new { action = "Index", id = UrlParameter.Optional }
                );
                context.MapRoute(
                        name: "AC_Adjuntar_Excel",
                        url: "AdjuntarExcelPlaces",
                        defaults: new { controller = "Persona", action = "AC_Adjuntar_Excel" }
                    );
                context.MapRoute(
                    name: "AC_Descargar_Formato_Excel",
                    url: "DescargarFormatoExcel",
                    defaults: new { controller = "Persona", action = "AC_Descargar_Formato_Excel" }
                );
            }
            catch (System.Exception Ex)
            {
                ErrorSignal.FromCurrentContext().Raise(Ex); //ELMAH Signaling
                throw;
            }

        }
    }
}

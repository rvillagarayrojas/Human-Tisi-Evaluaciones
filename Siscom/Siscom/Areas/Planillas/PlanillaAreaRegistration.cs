using System.Web.Mvc;
using Elmah;
namespace Siscom.Areas.Planilla
{
    public class PlanillaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Planilla";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            try
            {
                context.MapRoute(
                    "Planilla_default",
                    "Planilla/{controller}/{action}/{id}",
                    new { action = "Index", id = UrlParameter.Optional }
                );
            }
            catch (System.Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex); //ELMAH Signaling
                throw;
            }

        }
    }
}

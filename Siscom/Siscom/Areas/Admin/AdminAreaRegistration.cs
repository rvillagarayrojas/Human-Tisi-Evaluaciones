namespace Siscom.Areas.Admin 
{
    using System;
    using System.Web.Mvc;
    using Elmah;

    // ReSharper disable UnusedMember.Global
    public class AdminAreaRegistration : AreaRegistration
    // ReSharper restore UnusedMember.Global
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            try
            {
                if (context == null)
                {
                    throw new ArgumentNullException("context");
                }

                context.MapRoute(
                    "Admin_default",
                    "Admin/{controller}/{action}/{id}",
                    new { action = "Index", id = UrlParameter.Optional });
            }
            catch (Exception Ex)
            {
                ErrorSignal.FromCurrentContext().Raise(Ex); //ELMAH Signaling
                throw;
            }

        }
    }
}

using System.Web.Mvc;

namespace UI.Areas.Depot
{
    public class DepotAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Depot";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Depot_default",
                "Depot/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
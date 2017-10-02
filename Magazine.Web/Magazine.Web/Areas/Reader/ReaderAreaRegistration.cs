using System.Web.Mvc;

namespace MagazineApp.Web.Areas.Reader
{
    public class ReaderAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Reader";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Reader_default",
                "Reader/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MagazineApp.Web.Areas.Reader.Controllers" }
            );
        }
    }
}
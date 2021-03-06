﻿using System.Web.Mvc;

namespace MagazineApp.Web.Areas.Journalist
{
    public class JournalistAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Journalist";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Journalist_default",
                "Journalist/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MagazineApp.Web.Areas.Journalist.Controllers" }
            );
        }
    }
}
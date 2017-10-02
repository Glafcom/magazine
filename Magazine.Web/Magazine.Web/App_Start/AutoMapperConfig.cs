using AutoMapper;
using MagazineApp.Web.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagazineApp.Web.App_Start {
    public class AutomapperConfig {
        public static void CreateMap() {
            Mapper.Initialize(cfg => cfg.AddProfile<WebMappingProfile>());
            Mapper.AssertConfigurationIsValid();
        }
    }
}
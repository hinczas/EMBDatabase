using EMBDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMBDatabase.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.Initialize(
                cfg => {
                    cfg.CreateMap<Part, ExportPart>();
                    cfg.CreateMap<Manufacturer, ExportManufacturer>();
                });
        }
    }
}
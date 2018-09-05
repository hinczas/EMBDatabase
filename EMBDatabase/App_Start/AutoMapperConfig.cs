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
                    cfg.CreateMap<ExportPart, Part>();
                    cfg.CreateMap<Manufacturer, ExportManufacturer>();
                    cfg.CreateMap<ExportManufacturer, Manufacturer>();
                    cfg.CreateMap<Part, ApiPart>()
                        .ForMember(p => p.Manufacturer_Name, opts => opts.MapFrom(s => s.Manufacturer.Name))
                        .ForMember(p => p.Location_Name, opts => opts.MapFrom(s => s.Location.Name))
                        .ForMember(p => p.Package_Name, opts => opts.MapFrom(s => s.Package.Name))
                        .ForMember(p => p.Type_Name, opts => opts.MapFrom(s => s.Type.Name));
                    cfg.CreateMap<ApiPart, Part>();
                });
        }
    }
}
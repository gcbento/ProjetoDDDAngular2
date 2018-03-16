using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.Mappings
{
    public static class AutoMappingConfiguration
    {
        public static void Initialize()
        {
            Mapper.Initialize((cfg) =>
            {
                cfg.AddProfiles(IoC.AutoMapperConfiguration.GetAutoMapperProfiles());
            });
        }
    }
}

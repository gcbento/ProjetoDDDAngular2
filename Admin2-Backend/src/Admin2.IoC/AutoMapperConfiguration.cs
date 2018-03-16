using Admin2.AppServices.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.IoC
{
    public static class AutoMapperConfiguration
    {
        public  static IEnumerable<Type> GetAutoMapperProfiles()
        {
            var result = new List<Type>();
            result.Add(typeof(MappingProfile));
            return result;
        }
    }
}

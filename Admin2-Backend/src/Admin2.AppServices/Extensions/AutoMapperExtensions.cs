using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.AppServices.Extensions
{
    internal static class AutoMapperExtensions
    {
        public static T MapTo<T>(this object value)
        {
            return Mapper.Map<T>(value);
        }

        public static IEnumerable<T> EnumerableTo<T>(this object value)
        {
            return Mapper.Map<IEnumerable<T>>(value);
        }
    }
}

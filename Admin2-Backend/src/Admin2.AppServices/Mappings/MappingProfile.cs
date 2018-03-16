using Admin2.Domain.Models;
using Admin2.Domain.Filters;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.AppServices.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<CompraDto, Compra>().ReverseMap();
            //CreateMap<CompraFilterDto, CompraFilter>().ReverseMap();
        }
    }
}

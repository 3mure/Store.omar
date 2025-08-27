using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Shared;

namespace Services
{
    public class MappingProfil : Profile
    {
        public MappingProfil()
        {
            CreateMap<Product, ProductResultDto>()
                .ForMember(S => S.broductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(S => S.broductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                //.ForMember(S=>S.PictureUrl , o => o.MapFrom(S =>$"https://localhost:7178/{S.PictureUrl}"))
                .ForMember(S=>S.PictureUrl , o=>o.MapFrom<PictureUrlResolver>());
            CreateMap<ProductType, TypeResultDto>();
            CreateMap<ProductBrand, BrandResultDto>();



        }
    }
}

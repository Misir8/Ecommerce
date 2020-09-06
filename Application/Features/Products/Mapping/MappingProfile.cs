using System.IO;
using Application.Features.Products.Dto;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Application.Features.Products.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            CreateMap<Product, ProductReturnDto>()
                .ForMember(x => x.ProductBrand, o =>
                    o.MapFrom(x => x.ProductBrand.Name))
                .ForMember(x => x.ProductType, o =>
                    o.MapFrom(x => x.ProductType.Name))
                .ForMember(x => x.PictureUrl, o =>
                    o.MapFrom(x => config["ApiUrl"] + x.PictureUrl));
        }
    }
}
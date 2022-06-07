using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WebApiFHT.Entities;
using WebApiFHT.Models;

namespace WebApiFHT
{
    public class FhtMappingProfile : Profile
    {
        public FhtMappingProfile()
        {
            CreateMap<Company, FhtDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            CreateMap<Product, ProductDto>();


            CreateMap<CreateCompanyDto, Company>()
                .ForMember(r => r.Address, c => c.MapFrom(dto => new Address()
                { City = dto.City, PostalCode = dto.PostalCode, Street = dto.Street }));

            CreateMap<CreateProductDto, Product>();
        }
    }
}

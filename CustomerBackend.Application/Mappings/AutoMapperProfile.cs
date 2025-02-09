using CustomerBackend.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using CustomerBackend.Domain.Entities;

namespace CustomerBackend.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Customer -> CustomerDTO
            CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.CompanyName,
                    opt => opt.MapFrom(src => src.Company != null ? src.Company.CompanyName : null));

            // CreateCustomerDTO -> Customer
            CreateMap<CreateCustomerDTO, Customer>()
                .ForMember(dest => dest.CreatedDate,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive,
                    opt => opt.MapFrom(src => true));

            // UpdateCustomerDTO -> Customer
            CreateMap<UpdateCustomerDTO, Customer>()
                .ForMember(dest => dest.UpdatedDate,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Company -> CompanyDTO (si lo necesitamos más adelante)
            CreateMap<Company, CompanyDTO>();
        }
    }
}

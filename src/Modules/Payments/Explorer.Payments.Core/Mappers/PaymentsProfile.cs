using AutoMapper;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Dtos.BundleDto;
using Explorer.Payments.Core.Domain;
using Explorer.Tours.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Mappers
{
    public class PaymentsProfile : Profile
    {
        public PaymentsProfile()
        {
            CreateMap<CreateBundleDto, Bundle>()
                .ForCtorParam("price", opt => opt.MapFrom(dto => new Money(dto.Price.Amount, dto.Price.Currency)))
                .ForCtorParam("bundleItems", opt => opt.MapFrom(dto => dto.BundleItems));

            CreateMap<UpdateBundleDto, Bundle>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => !string.IsNullOrWhiteSpace(src.Name)))
                .ForMember(dest => dest.Price, opt => opt.Condition(src => src.Price != null))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new Money(src.Price.Amount, src.Price.Currency)));

            CreateMap<Bundle, BundleDetailsDto>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new ShoppingMoneyDto
                {
                    Amount = src.Price.Amount,
                    Currency = src.Price.Currency
                }))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<Bundle, BundleSummaryDto>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new ShoppingMoneyDto
                {
                    Amount = src.Price.Amount,
                    Currency = src.Price.Currency
                }))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        }

    }
    
}

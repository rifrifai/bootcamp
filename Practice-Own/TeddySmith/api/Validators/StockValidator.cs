using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace api.Validators
{
    public class StockValidator : Profile
    {
        public StockValidator()
        {
            CreateMap<Dtos.Stock.CreateStockRequestDto, Models.Stock>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dest => dest.Purchase, opt => opt.MapFrom(src => src.Purchase))
                .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol))
                .ForMember(dest => dest.Industry, opt => opt.MapFrom(src => src.Industry))
                .ForMember(dest => dest.LastDiv, opt => opt.MapFrom(src => src.LastDiv))
                .ForMember(dest => dest.MarketCap, opt => opt.MapFrom(src => src.MarketCap));
                

            CreateMap<Dtos.Stock.UpdateStockRequestDto, Models.Stock>()
               .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dest => dest.Purchase, opt => opt.MapFrom(src => src.Purchase))
                .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol))
                .ForMember(dest => dest.Industry, opt => opt.MapFrom(src => src.Industry))
                .ForMember(dest => dest.LastDiv, opt => opt.MapFrom(src => src.LastDiv))
                .ForMember(dest => dest.MarketCap, opt => opt.MapFrom(src => src.MarketCap));
        }
    }
}
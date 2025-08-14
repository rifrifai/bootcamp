using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;
using AutoMapper;
using api.Dtos.Comment;

namespace api.Mappers
{



    // public class StockMappers : Profile
    // {
    //     public StockMappers()
    //     {
    //         CreateMap<Stock, StockDto>()
    //             .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol))
    //             .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
    //             .ForMember(dest => dest.Purchase, opt => opt.MapFrom(src => src.Purchase))
    //             .ForMember(dest => dest.LastDiv, opt => opt.MapFrom(src => src.LastDiv))
    //             .ForMember(dest => dest.Industry, opt => opt.MapFrom(src => src.Industry))
    //             .ForMember(dest => dest.MarketCap, opt => opt.MapFrom(src => src.MarketCap))
    //             .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.Select(c => c.ToCommentDto()).ToList()));

    //         CreateMap<Stock, CreateStockRequestDto>()
    //             .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol))
    //             .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
    //             .ForMember(dest => dest.Purchase, opt => opt.MapFrom(src => src.Purchase))
    //             .ForMember(dest => dest.LastDiv, opt => opt.MapFrom(src => src.LastDiv))
    //             .ForMember(dest => dest.Industry, opt => opt.MapFrom(src => src.Industry))
    //             .ForMember(dest => dest.MarketCap, opt => opt.MapFrom(src => src.MarketCap));
    //     }
    // }


    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel?.Comments?.Select(c => c.ToCommentDto()).ToList()
            };
        }

        public static Stock ToStockFromCreateDto(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };
        }
    }
}
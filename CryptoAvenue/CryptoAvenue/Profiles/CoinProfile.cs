using AutoMapper;
using CryptoAvenue.Domain.Models;
using CryptoAvenue.Dtos.CoinDtos;

namespace CryptoAvenue.Profiles
{
    public class CoinProfile : Profile
    {
        public CoinProfile()
        {
            CreateMap<CoinPutPostDto, Coin>()
                .ForMember(c => c.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(c => c.Abbreviation, opt => opt.MapFrom(s => s.Abbreviation))
                .ForMember(c => c.ValueInEUR, opt => opt.MapFrom(s => s.ValueInEUR))
                .ForMember(c => c.ValueInUSD, opt => opt.MapFrom(s => s.ValueInUSD))
                .ReverseMap();

            CreateMap<CoinGetDto, Coin>()
                .ForMember(c => c.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(c => c.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(c => c.Abbreviation, opt => opt.MapFrom(s => s.Abbreviation))
                .ForMember(c => c.ValueInEUR, opt => opt.MapFrom(s => s.ValueInEUR))
                .ForMember(c => c.ValueInUSD, opt => opt.MapFrom(s => s.ValueInUSD))
                .ReverseMap();
        }
    }
}

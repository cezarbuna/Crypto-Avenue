using AutoMapper;
using CryptoAvenue.Domain.Models;
using CryptoAvenue.Dtos.WalletDtos;

namespace CryptoAvenue.Profiles
{
    public class WalletProfile : Profile
    {
        public WalletProfile()
        {
            CreateMap<WalletGetDto, Wallet>()
                .ForMember(w => w.Id, opt => opt.MapFrom(w => w.Id))
                .ForMember(w => w.CoinAmount, opt => opt.MapFrom(w => w.CoinAmount))
                .ForMember(w => w.CoinId, opt => opt.MapFrom(w => w.CoinId))
                .ForMember(w => w.Coin, opt => opt.MapFrom(w => w.Coin))
                .ForMember(w => w.UserId, opt => opt.MapFrom(w => w.UserId))
                .ForMember(w => w.User, opt => opt.MapFrom(w => w.User))
                .ReverseMap();

            CreateMap<WalletPutPostDto, Wallet>()
                .ForMember(w => w.CoinId, opt => opt.MapFrom(w => w.CoinId))
                .ForMember(w => w.UserId, opt => opt.MapFrom(w => w.UserId))
                .ForMember(w => w.CoinAmount, opt => opt.MapFrom(w => w.CoinAmount))
                .ReverseMap();
        }
    }
}

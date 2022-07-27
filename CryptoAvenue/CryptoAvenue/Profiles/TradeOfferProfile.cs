using AutoMapper;
using CryptoAvenue.Domain.Models;
using CryptoAvenue.Dtos.TradeOfferDtos;

namespace CryptoAvenue.Profiles
{
    public class TradeOfferProfile : Profile
    {
        public TradeOfferProfile()
        {
            CreateMap<TradeOfferGetDto, TradeOffer>()
                .ForMember(t => t.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(t => t.SentCoinAmount, opt => opt.MapFrom(d => d.SentCoinAmount))
                .ForMember(t => t.ReceivedCoinAmount, opt => opt.MapFrom(d => d.ReceivedCoinAmount))
                .ForMember(t => t.SenderId, opt => opt.MapFrom(d => d.SenderId))
                .ForMember(t => t.Sender, opt => opt.MapFrom(d => d.Sender))
                .ForMember(t => t.RecipientId, opt => opt.MapFrom(d => d.RecipientId))
                .ForMember(t => t.Recipient, opt => opt.MapFrom(d => d.Recipient))
                .ForMember(t => t.SentCoinId, opt => opt.MapFrom(d => d.SentCoinId))
                .ForMember(t => t.SentCoin, opt => opt.MapFrom(d => d.SentCoin))
                .ForMember(t => t.ReceivedCoinId, opt => opt.MapFrom(d => d.ReceivedCoinId))
                .ForMember(t => t.ReceivedCoin, opt => opt.MapFrom(d => d.ReceivedCoin))
                .ReverseMap();

            CreateMap<TradeOfferPutPostDto, TradeOffer>()
                .ForMember(t => t.ReceivedCoinAmount, opt => opt.MapFrom(d => d.ReceivedCoinAmount))
                //.ForMember(t => t.ReceivedAmount, opt => opt.MapFrom(d => d.ReceivedAmount))
                .ForMember(t => t.SenderId, opt => opt.MapFrom(d => d.SenderId))
                .ForMember(t => t.RecipientId, opt => opt.MapFrom(d => d.RecipientId))
                .ForMember(t => t.SentCoinId, opt => opt.MapFrom(d => d.SentCoinId))
                .ForMember(t => t.ReceivedCoinId, opt => opt.MapFrom(d => d.ReceivedCoinId))
                .ReverseMap();
        }
    }
}

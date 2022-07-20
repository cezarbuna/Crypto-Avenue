using AutoMapper;
using CryptoAvenue.Domain.Models;
using CryptoAvenue.Dtos.UserDtos;

namespace CryptoAvenue.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserGetDto, User>()
                .ForMember(u => u.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(u => u.Username, opt => opt.MapFrom(u => u.Username))
                .ForMember(u => u.Email, opt => opt.MapFrom(u => u.Email))
                .ForMember(u => u.Password, opt => opt.MapFrom(u => u.Password))
                .ForMember(u => u.Age, opt => opt.MapFrom(u => u.Age))
                .ForMember(u => u.SecurityQuestion, opt => opt.MapFrom(u => u.SecurityQuestion))
                .ForMember(u => u.SecurityAnswer, opt => opt.MapFrom(u => u.SecurityAnswer))
                .ForMember(u => u.PrivateProfile, opt => opt.MapFrom(u => u.PrivateProfile))
                .ReverseMap();

            CreateMap<UserPutPostDto, User>()
                .ForMember(u => u.Username, opt => opt.MapFrom(u => u.Username))
                .ForMember(u => u.Email, opt => opt.MapFrom(u => u.Email))
                .ForMember(u => u.Password, opt => opt.MapFrom(u => u.Password))
                .ForMember(u => u.Age, opt => opt.MapFrom(u => u.Age))
                .ForMember(u => u.SecurityQuestion, opt => opt.MapFrom(u => u.SecurityQuestion))
                .ForMember(u => u.SecurityAnswer, opt => opt.MapFrom(u => u.SecurityAnswer))
                .ForMember(u => u.PrivateProfile, opt => opt.MapFrom(u => u.PrivateProfile))
                .ReverseMap();
        }
    }
}

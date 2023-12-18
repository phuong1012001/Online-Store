using AutoMapper;
using OnlineStore.Api.Models.Requests.Auth;
using OnlineStore.Api.Models.Responses.Auth;
using OnlineStore.BusinessLogic.Dtos.Auth;
using OnlineStore.DataAccess.Entities;

namespace OnlineStore.Api.Mappers
{
    public class AuthMapper : Profile
    {
       public AuthMapper()
        {
            CreateMap<LoginReq, LoginDto>();

            CreateMap<LoginResultDto, LoginRes>();

            CreateMap<RegisterReq, RegisterDto>();

            CreateMap<RegisterResultDto, RegisterRes>();

            CreateMap<RegisterDto, User>();

            CreateMap<ChangePasswordReq, ChangePasswordDto>();

            CreateMap<ResultDto, UserResultRes>();
        }
    }
}

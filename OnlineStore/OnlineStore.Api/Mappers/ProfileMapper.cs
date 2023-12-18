using AutoMapper;
using OnlineStore.Api.Models.Requests.ProfileRes;
using OnlineStore.Api.Models.Responses.ProfileRes;
using OnlineStore.BusinessLogic.Dtos.Profile;
using OnlineStore.DataAccess.Entities;

namespace OnlineStore.Api.Mappers
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<ProfileReq, ProfileDto>();

            CreateMap<User,  ProfileDto>();

            CreateMap<ProfileDto, ProfileRes>();
        }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.BusinessLogic.Constants;
using OnlineStore.BusinessLogic.Dtos.Profile;
using OnlineStore.DataAccess.DbContexts;

namespace OnlineStore.BusinessLogic.Services
{
    public interface IProfileService
    {
        Task<ProfileDto?> DetailProfile(int id);
    }

    public class ProfileService : IProfileService
    {
        private readonly IMapper _mapper;
        private readonly OnlineStoreDbContext _context;

        public ProfileService(IMapper mapper, OnlineStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ProfileDto?> DetailProfile(int id)
        {
            var profileDto = new ProfileDto();

            var profileRepoId = await _context.Users
                    .FirstOrDefaultAsync(m => m.Id == id);

            if (profileRepoId == null)
            {
                profileDto.ErrorMessage = ErrorCodes.NotFoundProfile;
                return profileDto;
            }

            profileDto = _mapper.Map<ProfileDto>(profileRepoId);
            return profileDto;
        }
    }
}

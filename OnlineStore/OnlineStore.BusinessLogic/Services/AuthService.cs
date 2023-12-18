using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.BusinessLogic.Constants;
using OnlineStore.BusinessLogic.Dtos.Auth;
using OnlineStore.DataAccess.DbContexts;
using OnlineStore.DataAccess.Entities;

namespace OnlineStore.BusinessLogic.Services
{
    public interface IAuthService
    {
        Task<LoginResultDto> LoginAsync(LoginDto loginDto);
        Task<RegisterResultDto> RegisterAsync(RegisterDto registerDto);
        Task<ResultDto> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
    }

    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly OnlineStoreDbContext _context;

        public AuthService(IMapper mapper, OnlineStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<LoginResultDto> LoginAsync(LoginDto loginDto)
        {
            var result = new LoginResultDto();

            if (_context.Users == null)
            {
                result.Error = ErrorCodes.NotFoundUser;
                return result;
            }

            var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Email == loginDto.Email && x.Password == loginDto.Password);
            
            if (user == null)
            {
                result.Success = false;
                result.Error = ErrorCodes.NotFoundUser;
                return result;
            }

            return result;
        }

        public async Task<RegisterResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var result = new RegisterResultDto();
            if (_context.Users == null)
            {
                result.Error = ErrorCodes.NotFoundUser;
                return result;
            }

            var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Email == registerDto.Email);
            if (user != null)
            {
                result.Error = ErrorCodes.AccountAlreadyExists;
                return result;
            }

            var userEntity = _mapper.Map<User>(registerDto);
            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();

            result.Success = true;
            return result;
        }

        public async Task<ResultDto> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            var result = new ResultDto();

            if (_context.Users == null)
            {
                result.ErrorCode = ErrorCodes.NotFoundUser;
                return result;
            }

            var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Email == changePasswordDto.Email);

            if (user == null)
            {
                result.ErrorCode = ErrorCodes.IncorrectEmail;
                return result;
            }

            if (user.Password != changePasswordDto.PasswordOld)
            {
                result.ErrorCode = ErrorCodes.IncorrectPassword;
                return result;
            }

            user.Password = changePasswordDto.PasswordNew;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            result.Success = true;

            return result;
        }
    }
}

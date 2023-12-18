using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Api.Models.Requests.ProfileRes;
using OnlineStore.Api.Models.Responses.Base;
using OnlineStore.Api.Models.Responses.ProfileRes;
using OnlineStore.BusinessLogic.Constants;
using OnlineStore.BusinessLogic.Dtos.Profile;
using OnlineStore.BusinessLogic.Services;

namespace OnlineStore.Api.Controllers
{
    [Route("api/Profile")]
    public class ProfileController : BaseController
    {
        private readonly IProfileService _profileService;

        public ProfileController(
            ILogger<BaseController> logger,
            IMapper mapper,
            IProfileService profileService)
            : base(logger, mapper)
        {
            _profileService = profileService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResultRes<ProfileRes>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProfileAsync(int id)
        {
            var response = new ResultRes<ProfileRes>();

            try
            {
                var result = await _profileService.DetailProfile(id);

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    switch(result.ErrorMessage)
                    {
                        case ErrorCodes.NotFoundProfile:
                            response.Errors = SetError("Profile does not exist.");
                            break;
                    }

                    return BadRequest(response);
                }

                response.Result = Mapper.Map<ProfileRes>(result);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                Logger.LogError("Get profile failed: {ex}", ex);
                return InternalServerError(response);
            }
            return Ok(response);
        }
    }
}

using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    [ApiExplorerSettings(GroupName = "v1")]

    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;

        public AuthenticationController(IServiceManager service)
        {
            _service = service;
        }


        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUserAsync([FromBody]UserForRegistrationDto userForRegistrationDto)
        {
            var result = await _service.AuthenticationService.RegisterUserAsync(userForRegistrationDto);
            
            if(!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return StatusCode(201);
        }


        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AuthenticateAsync([FromBody]UserForAuthenticationDto user)
        {
            if (!await _service.AuthenticationService.ValidateUserAsync(user))
            {
                return Unauthorized(); // 401
            }

            var tokenDto = await _service.AuthenticationService.CreateTokenAsync(populateExp : true);

            return Ok(tokenDto);
        }


        [HttpPost("refresh")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RefreshAsync([FromBody]TokenDto tokenDto)
        {
            var tokenDtoToReturn = await _service.AuthenticationService.RefreshTokenAsync(tokenDto);
            return Ok(tokenDtoToReturn);
        } 

    }
}

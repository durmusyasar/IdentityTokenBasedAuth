using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityTokenBasedAuth.Domain.Response;
using IdentityTokenBasedAuth.Domain.Services;
using IdentityTokenBasedAuth.ResourceViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityTokenBasedAuth.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService service)
        {
            this.authenticationService = service;
        }

        [HttpGet]
        public ActionResult IsAuthentication()
        {
            return Ok(User.Identity.IsAuthenticated);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserViewModelResource userViewModelResource)
        {
            BaseResponse<UserViewModelResource> response =  await this.authenticationService.SignUp(userViewModelResource);

            if (response.Success)
            {
                return Ok(response.Extra);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
    
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModelResource signInViewModel)
        {
            var response = await authenticationService.SignIn(signInViewModel);

            if (response.Success)
            {
                return Ok(response.Extra);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> TokenByRefreshToken(RefreshTokenViewModelResource refreshTokenViewModel)
        {
            var response = await authenticationService.CreateAccessTokenByRefreshToken(refreshTokenViewModel);

            if (response.Success)
            {
                return Ok(response.Extra);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenViewModelResource refreshTokenViewModel)
        {
            var response = await authenticationService.RevokeRefreshTopken(refreshTokenViewModel);

            if (response.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

    }
}

using IdentityTokenBasedAuth.Domain.Response;
using IdentityTokenBasedAuth.ResourceViewModel;
using IdentityTokenBasedAuth.Security.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityTokenBasedAuth.Domain.Services
{
    public interface IAuthenticationService
    {
        Task<BaseResponse<UserViewModelResource>> SignUp(UserViewModelResource userViewModel);
        Task<BaseResponse<AccessToken>> SignIn(SignInViewModelResource signInViewModel);
        Task<BaseResponse<AccessToken>> CreateAccessTokenByRefreshToken(RefreshTokenViewModelResource refreshTokenViewModel);
        Task<BaseResponse<AccessToken>> RevokeRefreshTopken(RefreshTokenViewModelResource refreshTokenViewModel);
    }
}

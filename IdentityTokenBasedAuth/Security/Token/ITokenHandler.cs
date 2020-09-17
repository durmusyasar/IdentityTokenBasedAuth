

using IdentityTokenBasedAuth.Models;

namespace IdentityTokenBasedAuth.Security.Token
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(AppUser user);
        void RevokeRefreshToken(AppUser user);
    }
}

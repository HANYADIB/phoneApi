using phoneApi.Models.Dto;
using System.Security.Claims;

namespace phoneApi.Models.Res
{
    public interface ITokenService
    {
        TokenResponse GetToken(IEnumerable<Claim> claim);
        string GetRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
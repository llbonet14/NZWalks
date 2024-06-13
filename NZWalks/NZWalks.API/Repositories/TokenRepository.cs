using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            throw new NotImplementedException();
        }
    }
}

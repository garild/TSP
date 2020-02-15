using Microsoft.AspNetCore.Http;
using Tsp.Authorization.JWT;

namespace Tsp.Authorization
{
    public interface IJwtAuthorizeHandler
    {
        void AuthorizeUser(JwtUserDto userDto, HttpContext context);
        bool TokenExpired(string token);
    }
}
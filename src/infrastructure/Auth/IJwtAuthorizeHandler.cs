using Auth.JWT;
using Microsoft.AspNetCore.Http;

namespace Auth
{
    public interface IJwtAuthorizeHandler
    {
        void AuthorizeUser(JwtUserDto userDto, HttpContext context);
        bool TokenExpired(string token);
    }
}
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Auth.JWT;
using Microsoft.AspNetCore.Http;

namespace Auth
{
    public class JwtAuthorizeHandler : IJwtAuthorizeHandler
    {
        private readonly IJwtProvider _jwtProvider;

        public JwtAuthorizeHandler(IJwtProvider jwtProvider)
        {
            _jwtProvider = jwtProvider;
        }

        public void AuthorizeUser(JwtUserDto userDto,HttpContext context)
        {
            var jwt = _jwtProvider.Create(userDto, userDto.Roles);
            context.User = new ClaimsPrincipal(jwt.Identity);
            userDto.AccessToken = jwt.AccessToken;
        }

        public bool TokenExpired(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token.Trim());

            var now = DateTime.UtcNow;

            return jwtToken.ValidTo < now;
        }
    }
}

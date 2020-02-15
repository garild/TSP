using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Tsp.Authorization.JWT;

namespace Tsp.Authorization
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
            jwt.Claims.TryGetValue(JwtRegisteredClaimNames.GivenName, out var firstName);
            jwt.Claims.TryGetValue(JwtRegisteredClaimNames.FamilyName, out var lastName);

            context.User = new ClaimsPrincipal(jwt.Identity);
            userDto.AccessToken = jwt.AccessToken;
            userDto.Id = jwt.Id;
            userDto.FirstName = firstName;
            userDto.LastName = lastName;
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

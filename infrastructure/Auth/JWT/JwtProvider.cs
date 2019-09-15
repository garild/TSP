using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Auth.JWT
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtSettings _jwtOptions;
      
        public JwtProvider(IOptionsMonitor<JwtSettings> jwtOptions)
        {
            _jwtOptions = jwtOptions.CurrentValue;
        }

        public JsonWebToken Create(JwtUserDto userDto, string[] userRole)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;

            var genericIdentity = BuildClaims(userDto, userRole);

            var expires = now.AddMinutes(_jwtOptions.ExpiryMinutes);

            var jwt = new JwtSecurityToken(
                _jwtOptions.Issuer,
                claims: genericIdentity.Claims,
                notBefore: now,
                expires: expires,
                audience: _jwtOptions.Audience,
                signingCredentials: credentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonWebToken
            {
                AccessToken = token,
                Identity = genericIdentity.Identity
            };
        }

        private static GenericPrincipal BuildClaims(JwtUserDto userDto, string[] userRole)
        {
            var claimsIdentity = new ClaimsIdentity("password", ClaimTypes.Name, "AuthApiPolicy");
            claimsIdentity.AddClaims(new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, userDto.Login),
             });

            if (userRole != null)
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role,string.Join(",", userRole)));

            var genericPrincipal = new GenericPrincipal(claimsIdentity, userRole);

            return genericPrincipal;
        }
    }
}


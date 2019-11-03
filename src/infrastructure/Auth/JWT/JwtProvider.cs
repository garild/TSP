using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Auth.JWT
{
    public class JwtProvider : IJwtProvider
    {
        private static readonly ISet<string> DefaultClaims = new HashSet<string>
        {
            JwtRegisteredClaimNames.Sub,
            JwtRegisteredClaimNames.UniqueName,
            JwtRegisteredClaimNames.Jti,
            JwtRegisteredClaimNames.Iat,
            ClaimTypes.Role,
        };

        private readonly JwtSettings _jwtOptions;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly SigningCredentials _signingCredentials;

        public JwtProvider(IOptionsMonitor<JwtSettings> jwtOptions)
        {
            _jwtOptions = jwtOptions.CurrentValue;
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            _signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256);

            _tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = issuerSigningKey,
                ValidIssuer = _jwtOptions.Issuer,
                //ValidAudience = _jwtOptions.ValidAudience,
                //ValidateAudience = _jwtOptions.ValidateAudience,
                //ValidateLifetime = _jwtOptions.ValidateLifetime
            };
        }

        public JsonWebToken Create(JwtUserDto userDto, string[] userRole)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

            var now = DateTime.UtcNow;

            var genericIdentity = BuildClaims(userDto, userRole);

            var expires = now.AddMinutes(_jwtOptions.ExpiryMinutes);

            var jwt = new JwtSecurityToken(
                _jwtOptions.Issuer,
                claims: genericIdentity.Claims,
                notBefore: now,
                expires: expires,
                audience: _jwtOptions.Audience,
                signingCredentials: _signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonWebToken
            {
                AccessToken = token,
                Identity = genericIdentity.Identity,
                Claims = genericIdentity.Claims.ToDictionary(p=> p.Type, p=> p.Value),
                Expires = ToTimestamp(expires),
                Id = userDto.Id,
                RefreshToken = string.Empty,
            };
        }

        public JsonWebTokenPayload GetTokenPayload(string accessToken)
        {
            _jwtSecurityTokenHandler.ValidateToken(accessToken, _tokenValidationParameters,
                out var validatedSecurityToken);

            if (!(validatedSecurityToken is JwtSecurityToken jwt))
            {
                return null;
            }

            return new JsonWebTokenPayload
            {
                Subject = jwt.Subject,
                Role = jwt.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Role)?.Value,
                Expires = ToTimestamp(jwt.ValidTo),
                Claims = jwt.Claims.Where(x => !DefaultClaims.Contains(x.Type))
                    .ToDictionary(k => k.Type, v => v.Value)
            };
        }

        private static GenericPrincipal BuildClaims(JwtUserDto userDto, string[] userRole)
        {
            var claimsIdentity = new ClaimsIdentity("password", ClaimTypes.Name, "AuthApiPolicy");
            var now = DateTime.UtcNow;

            claimsIdentity.AddClaims(new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, userDto.Id),
                new Claim(JwtRegisteredClaimNames.GivenName, userDto.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, userDto.LastName),
                new Claim(JwtRegisteredClaimNames.UniqueName, userDto.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, ToTimestamp(now).ToString()),
             });

            if (userRole != null)
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role,string.Join(",", userRole)));

            var genericPrincipal = new GenericPrincipal(claimsIdentity, userRole);

            return genericPrincipal;
        }

        private static long ToTimestamp(DateTime dateTime)
        {
            var centuryBegin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var expectedDate = dateTime.Subtract(new TimeSpan(centuryBegin.Ticks));

            return expectedDate.Ticks / 10000;
        }
    }
}


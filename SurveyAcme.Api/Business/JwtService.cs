using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SurveyAcme.Models.Utilities;
using SurveyAcme.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using SurveyAcme.Models.Outputs;

namespace SurveyAcme.Api.Business
{
    public class JwtService
    {
 
        public TokenOut GenerateToken(int expirationTime, string origin) => GenerateJWTTokens(expirationTime, origin);

        public TokenOut GenerateRefreshToken(int expirationTime, string origin) => GenerateJWTTokens(expirationTime, origin);

        private byte[] GetJwtSecretKey() => Encoding.UTF8.GetBytes("xjl7ADOyYV7kLacFu1F6BarBX98LTsdt3NpYd0GsD2xRSODQrXi6xKMr4MhIJSRuG7s4EcjXJmBV67f95D3ID69S8oFKEbvIlRmqH1V0ZB3RODMd/68tcELikBlM6Ya/");

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var Key = GetJwtSecretKey();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new CustomException("El Token de autenticación que intenta refrescar es invalido", HttpStatusCode.Unauthorized);

            return principal;
        }

        private TokenOut GenerateJWTTokens(int expirationTime, string origin)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = GetJwtSecretKey();
                var _claims = GenerateClaims(origin);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(_claims),
                    NotBefore = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddMinutes(expirationTime),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var refreshToken = GenerateRefreshToken();
                return new TokenOut { AccessToken = tokenHandler.WriteToken(token), RefreshToken = refreshToken };
            }
            catch (Exception ex)
            {
                throw new CustomException("No fue posible generar el Token de autenticación ", ex.Message, HttpStatusCode.Unauthorized);
            }
        }

        private List<Claim> GenerateClaims(string origin)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, "")
       
            };
            return claims;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

    }
}

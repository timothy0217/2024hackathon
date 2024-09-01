using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Hackathon_WebAPI.Services.Common
{
    public class JwtTokenService
    {
        private readonly IConfiguration Configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>生成JWT Token</summary>
        public string GenerateToken(string userName, int expireMinutes = 30)
        {
            var issuer = Configuration.GetValue<string>("JwtSettings:Issuer");
            var signKey = Configuration.GetValue<string>("JwtSettings:SecretKey");

            var claims = new List<Claim>();

            // User.Identity.Name
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, userName));

            // JWT ID
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            claims.Add(new Claim(ClaimTypes.Role, "Users"));

            var userClaimsIdentity = new ClaimsIdentity(claims);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signKey!));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Subject = userClaimsIdentity,
                Expires = DateTime.Now.AddMinutes(expireMinutes),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JsonWebTokenHandler();
            var serializeToken = tokenHandler.CreateToken(tokenDescriptor);

            return serializeToken;
        }
    }
}

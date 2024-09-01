using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Hackathon_WebAPI.Filters
{
    public class AuthenticationFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;

        public AuthenticationFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string token = context.HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(token))
            {
                // 如果沒有提供驗證的 JWT Token，返回未授權錯誤
                context.Result = new UnauthorizedResult();
                return;
            }

            try
            {
                // 驗證 JWT Token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]!);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                // 將驗證後的使用者資訊存入 HttpContext
                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;

                if (userId == null)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                context.HttpContext.Items["userId"] = userId;
            }
            catch (Exception)
            {
                //這邊待修正調整
                // 驗證失敗，返回未授權錯誤
                context.Result = new UnauthorizedResult();
            }
        }
    }
}

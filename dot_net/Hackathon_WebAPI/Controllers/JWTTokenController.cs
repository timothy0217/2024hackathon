using Hackathon_WebAPI.Dtos.Common;
using Hackathon_WebAPI.Enums;
using Hackathon_WebAPI.Interface.Common;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Hackathon_WebAPI.Controllers
{
    public class JWTTokenController : Controller
    {
        private readonly IJwtToken _jwtToken;
        private readonly IConfiguration Configuration;

        public JWTTokenController(IJwtToken jwtToken, IConfiguration configuration)
        {
            _jwtToken = jwtToken;
            Configuration = configuration;
        }

        /// <summary>取得JWT Token</summary>
        [HttpPost(Name = "GetToken:update")]
        public JsonResult CreateJWTToken()
        {
            JwtTokenDto jwtTokenDto = new JwtTokenDto();
            var user = Configuration.GetValue<string>("JwtSettings:JwtUser");
            jwtTokenDto.Token = _jwtToken.GenerateToken(user!, 30);
            return Json(new ResponseDto<JwtTokenDto>(jwtTokenDto, ResultEnum.Success, true));
        }
    }
}


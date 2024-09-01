namespace Hackathon_WebAPI.Interface.Common
{
    public interface IJwtToken
    {
        /// <summary>產出JWT Token</summary>
        /// <param name="userName">角色</param>
        /// <param name="expireMinutes">過期時間</param>
        string GenerateToken(string userName, int expireMinutes = 30);
    }
}

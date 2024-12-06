using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace WukongDemo.Util
{
    public class AuthUtils
    {
        // 从JWT Token中提取用户ID
        public static int GetUserIdFromToken(string authorization)
        {
            var token = authorization?.Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Invalid token");
            }

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            var userId = jsonToken?.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

            if (userId == null)
                throw new UnauthorizedAccessException("Invalid token");

            if (!int.TryParse(userId, out int userIdInt))
                throw new UnauthorizedAccessException("Invalid userId in token");

            return userIdInt;
        }
    }
}

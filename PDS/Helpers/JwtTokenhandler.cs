using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PDS.Helpers
{
    public static class JwtTokenhandler
    {
        public static string generateJwtToken(TokenData tokenData)
        {
            // generate token that is valid for 15 min

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: ConfigurationManager.AppSetting["JWT:ValidIssuer"],
                audience: ConfigurationManager.AppSetting["JWT:ValidAudience"],
                claims: new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, ConfigurationManager.AppSetting["JWT:Subject"]),
                        new Claim("UserID", tokenData.Id),
                        new Claim("DisplayName", tokenData.DisplayName),
                        new Claim("TokenType", tokenData.TokenType)
                    },
                expires: DateTime.Now.AddMonths(100),
                signingCredentials: signinCredentials
            );
            return new JwtSecurityTokenHandler().WriteToken(tokeOptions).ToString();
        }
    }
}

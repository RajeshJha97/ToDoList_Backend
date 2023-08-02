using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ToDoList.Utilities
{
    public class Token
    {
        public string GenerateToken(List<Claim> claims, DateTime expires_At, string secretKey)
        {
            var key=Encoding.ASCII.GetBytes(secretKey);
            var jwt = new JwtSecurityToken(
                
                claims:claims,
                notBefore:DateTime.Now,
                expires:expires_At,
                signingCredentials:new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                    
                    )
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}

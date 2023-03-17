using AuthService.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Models
{
    public class TokenManager
    {
        private readonly IConfiguration _configuration;

        public TokenManager(IConfiguration configuration) 
        {
            _configuration = configuration;
        }   

        public string CreateToken(string username)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: cred
                    );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        //public static string GenerateToken(string username)
        //{
        //    byte[] key = Convert.FromBase64String(Secret);
        //    SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
        //    SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[] {
        //              new Claim(ClaimTypes.Name, username)}),
        //        Expires = DateTime.UtcNow.AddMinutes(30),
        //        SigningCredentials = new SigningCredentials(securityKey,
        //        SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        //    JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
        //    return handler.WriteToken(token);
        //}



        private string CdreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: cred
                    );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }





        //public static ClaimsPrincipal GetPrincipal(string token)
        //{
        //    try
        //    {
        //        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        //        JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
        //        if (jwtToken == null)
        //            return null;
        //        byte[] key = Convert.FromBase64String(Secret);
        //        TokenValidationParameters parameters = new TokenValidationParameters()
        //        {
        //            RequireExpirationTime = true,
        //            ValidateIssuer = false,
        //            ValidateAudience = false,
        //            IssuerSigningKey = new SymmetricSecurityKey(key)
        //        };
        //        SecurityToken securityToken;
        //        ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
        //              parameters, out securityToken);
        //        return principal;
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }
        //}

        //public static string ValidateToken(string token)
        //{
        //    string username = null;
        //    ClaimsPrincipal principal = GetPrincipal(token);
        //    if (principal == null)
        //        return null;
        //    ClaimsIdentity identity = null;
        //    try
        //    {
        //        identity = (ClaimsIdentity)principal.Identity;
        //    }
        //    catch (NullReferenceException)
        //    {
        //        return null;
        //    }
        //    Claim usernameClaim = identity.FindFirst(ClaimTypes.Name);
        //    username = usernameClaim.Value;
        //    return username;
        //}





    }
}

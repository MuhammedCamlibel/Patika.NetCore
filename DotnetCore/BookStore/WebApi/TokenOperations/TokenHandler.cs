using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities;
using WebApi.TokenOperations.Models;

namespace WebApi.TokenOperations
{
    public class TokenHandler
    {
        public IConfiguration Configuration;
        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Token CreateAccessToken(User user)
        {
            Token token = new Token();
            
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Securitykey"]));
            SigningCredentials credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            token.Expiration = DateTime.Now.AddMinutes(20);

            var claims = new []
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Sub,user.Name)
            };

            JwtSecurityToken securityToken = new JwtSecurityToken
            (
                issuer: Configuration["Token:Issuer"],
                audience: Configuration["Token:Audience"],
                expires: token.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: credentials,
                claims:claims
            
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            //Token Üretildi
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            token.RefreshToken = CreateRefreshToken();


            return token;
        }

        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }


    }
}
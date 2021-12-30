using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using WebApi.DBOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {

        private readonly IBookStoreDbContext _context;
        private readonly IConfiguration _configuration;
        public string RefreshToken; 

        public RefreshTokenCommand(IBookStoreDbContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public Token Handle()
        {
            var user = _context.Users.SingleOrDefault(u=>u.RefreshToken == RefreshToken && u.RefreshTokenExpireDate > DateTime.Now);
            if(user is not null)
            {
                TokenHandler handler = new TokenHandler(_configuration);
                var token = handler.CreateAccessToken(user);
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _context.SaveChanges();

                return token;
            }
            else
              throw new InvalidOperationException("Refresh Token Hatalı Lüffen Tekrar oturum açın !");

             
        }
    }
}
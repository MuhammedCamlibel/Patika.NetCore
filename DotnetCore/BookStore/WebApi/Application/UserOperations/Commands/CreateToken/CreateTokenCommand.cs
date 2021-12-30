using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WebApi.DBOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
      private readonly IBookStoreDbContext _context;
      private readonly IMapper _mapper;
      private readonly IConfiguration _configuration;
      public CreateTokenModel Model;
      
      public CreateTokenCommand(IBookStoreDbContext context,IMapper mapper,IConfiguration configuration)
      {
          _context = context;
          _mapper = mapper;
          _configuration = configuration;
      }

      public Token Handle()
      {
          var user = _context.Users.SingleOrDefault(u=>u.Email == Model.Email);

          if(user is not null)
          {
              // Create Token
              TokenHandler tokenHandler = new TokenHandler(_configuration);
              Token token = tokenHandler.CreateAccessToken(user);

              user.RefreshToken = token.RefreshToken;
              user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);

              _context.SaveChanges();

              return token; 
          }
          else
           throw new InvalidOperationException("Kullanıcı Email - Şifre Hatalı");

      }
    }

    public class CreateTokenModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
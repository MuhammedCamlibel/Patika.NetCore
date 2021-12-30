
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Application.UserOperations.Commands.CreateToken;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.Application.UserOperations.Commands.RefreshToken;
using WebApi.DBOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Controllesrs
{
    [Route("[controller]s")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public UserController(IBookStoreDbContext context,IMapper mapper,IConfiguration configuration)
        {
           
           _context = context;
           _mapper = mapper;
           _configuration = configuration;
        }


        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserModel model)
        {
            CreateUserCommand command = new CreateUserCommand(_context,_mapper);
            CreateUserCommandValidator validator = new CreateUserCommandValidator();
            command.Model = model;
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok("Kullanıcı başarıyla Oluşturuldu");
        } 

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel model)
        {
            CreateTokenCommand command = new CreateTokenCommand(_context,_mapper,_configuration);
            command.Model = model;
            
            var token = command.Handle();

            return token;
        }

        [HttpGet("refreshToken")]
        public ActionResult<Token> RefreshToken([FromQuery] string refreshtoken)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_context,_configuration);
            command.RefreshToken = refreshtoken;
            var resultToken = command.Handle();
            return resultToken;
        }

        
    }
}
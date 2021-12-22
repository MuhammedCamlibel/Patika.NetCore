namespace WebApi.Controllers
{
    using AutoMapper;
    using FluentValidation;
    using global::WebApi.Application.AuthorOperations.Commands.CreateAuthor;
    using global::WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
    using global::WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
    using global::WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
    using global::WebApi.Application.AuthorOperations.Queries.GetAuthors;
    using global::WebApi.DBOperations;
    using Microsoft.AspNetCore.Mvc;
    
    
    namespace WebApi.Controllers
    {
        [Route("[controller]s")]
        [ApiController]
        public class AuthorController : ControllerBase
        {
            private readonly BookStoreDbContext _context;
            private readonly IMapper _mapper;
            public AuthorController(BookStoreDbContext context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }


            [HttpGet]
            public IActionResult GetAuthors()
            {
                GetAuthorsQuery query = new GetAuthorsQuery(_context,_mapper);
                var authors = query.Handle();
                return Ok(authors);
            }

            [HttpGet("{id}")]
            public IActionResult GetAuthorDetail(int id)
            {
                GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context,_mapper);
                query.AuthorId = id;
                GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
                validator.ValidateAndThrow(query);
                var author = query.Handle();
                return Ok(author);
            }

            [HttpPost]
            public IActionResult AddAuthor(CreateAuthorModel model)
            {
                CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
                command.model = model;
                CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();

                return Ok("Yazar Başarıyla Oluşturuldu");
            }

            [HttpDelete("{id}")]
            public IActionResult DeleteAuthor(int id)
            {
                DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
                command.AuthorId = id;
                DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
                
                return Ok("Yazar Başarıyla Silindi");
            }


            [HttpPut("{id}")]
            public IActionResult UpdateAuthor(int id,[FromBody]UpdateAuthorModel model )
            {
                UpdateAuthorCommand command = new UpdateAuthorCommand(_context,_mapper);
                command.AuthorId = id;
                command.model = model;
                UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
                
                return Ok("Yazar başarıyla Güncellendi");

            }


    
           
        }
    }
    
}
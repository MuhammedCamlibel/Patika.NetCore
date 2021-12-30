namespace WebApi.Controllers
{
    using AutoMapper;
    using FluentValidation;
    using global::WebApi.Application.GenreOperations.Commands.CreateGenre;
    using global::WebApi.Application.GenreOperations.Commands.DeleteGenre;
    using global::WebApi.Application.GenreOperations.Commands.UpdateGenre;
    using global::WebApi.Application.GenreOperations.Queries.GetGenreDetail;
    using global::WebApi.Application.GenreOperations.Queries.GetGenres;
    using global::WebApi.DBOperations;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    
    namespace WebApi.Controllers
    {
        [Authorize]
        [Route("[controller]s")]
        [ApiController]
        public class GenreController : ControllerBase
        {
            private readonly IBookStoreDbContext _context;
            private readonly IMapper _mapper;
            public GenreController(IBookStoreDbContext context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            [HttpGet]
            public IActionResult GetGenres()
            {
                GetGenresQuery query = new GetGenresQuery(_context,_mapper);
                var genres = query.Handle();
                return Ok(genres);
            }

            [HttpGet("{id}")]
            public IActionResult GetGenreDetail(int id)
            {
                GetGenreDetailQuery query = new GetGenreDetailQuery(_context,_mapper);
                query.GenreId = id;
                GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
                validator.ValidateAndThrow(query);
                var model =  query.Handle();

                return Ok(model);
            }

            [HttpPost]
            public IActionResult AddGenre([FromBody] CreateGenreModel model)
            {
                CreateGenreCommand command = new CreateGenreCommand(_context,_mapper);
                command.model = model;
                CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
                return Ok("Tür Başarıyla Eklendi");
            }

            [HttpDelete("{id}")]
            public IActionResult DeleteGenre(int id)
            {
                DeleteGenreCommand command = new DeleteGenreCommand(_context);
                command.GenreId = id;
                DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
                return Ok("Tür başarıyla silindi");
            }

            [HttpPut("{id}")]
            public IActionResult UpdateGenre(int id , [FromBody]UpdateGenreModel model)
            {
                UpdateGenreCommand command = new UpdateGenreCommand(_context);
                command.GenreId = id;
                command.model = model;
                UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
                return Ok("Tür Başarıyla Güncellendi");
            }
    
           
        }
    }
    
}
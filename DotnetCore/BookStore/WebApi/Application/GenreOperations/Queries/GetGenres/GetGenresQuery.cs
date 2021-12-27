using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        private readonly IBookStoreDbContext _context;
        private IMapper _mapper;

        public GetGenresQuery(IBookStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GenreViewModel> Handle()
        {
            var Genres = _context.Genres.Where(g=> g.IsActive == true).OrderBy(g=>g.Id).ToList();

            var GenresViewModel = _mapper.Map<List<GenreViewModel>>(Genres);
            
            return GenresViewModel; 
        }
    }

    public class GenreViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
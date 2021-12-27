using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        private readonly IBookStoreDbContext _context;
        private IMapper _mapper;
        public int GenreId;

        public GetGenreDetailQuery(IBookStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
             var genre = _context.Genres.Where(g=>g.IsActive == true).SingleOrDefault(g=>g.Id == GenreId);
             if(genre is null)
              throw new InvalidOperationException("Kitap türü bulunamadı");
             var vm = _mapper.Map<GenreDetailViewModel>(genre);

             return vm;
        }

        
    }


    public class GenreDetailViewModel 
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }  



}
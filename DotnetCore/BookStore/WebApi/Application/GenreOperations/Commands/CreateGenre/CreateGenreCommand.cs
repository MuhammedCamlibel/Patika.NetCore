using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel model;
        private readonly IBookStoreDbContext _context;
        private IMapper _mapper;

        public CreateGenreCommand(IBookStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
             var genre = _context.Genres.SingleOrDefault(g=>g.Name == model.Name);
             if(genre is not null)
               throw new InvalidOperationException("Kitap türü zaten mevcut");

            _context.Genres.Add(_mapper.Map<Genre>(model));
            _context.SaveChanges();
        }
    }

    public class CreateGenreModel
    {
        public string Name { get; set; }
    }

    
}
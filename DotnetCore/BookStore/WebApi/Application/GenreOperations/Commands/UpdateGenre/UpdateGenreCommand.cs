using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId;
        public UpdateGenreModel model;
        private readonly IBookStoreDbContext _context;
        

        public UpdateGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
           
        }

        public void Handle()
        {
           var genre = _context.Genres.SingleOrDefault(g=>g.Id == GenreId);
           if(genre is null)
            throw new InvalidOperationException("Güncellenecek Kitap Bulunamadı");
           
           if(_context.Genres.Any(g=> g.Name.ToLower() == model.Name.ToLower() && g.Id != GenreId))
            throw new InvalidOperationException("Aynı isimde zaten kitap türü bulunmakta"); 
                       
            genre.Name = string.IsNullOrEmpty( model.Name.Trim()) ? genre.Name : model.Name;
            genre.IsActive = model.IsActive;
            genre.Id = GenreId; 
             
            _context.SaveChanges();
        }



        
    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }=true;

    }
}
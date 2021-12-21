using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {

        public int GenreId;
        private readonly BookStoreDbContext _context;
        
        public DeleteGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }


        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(g=>g.Id == GenreId);

            if(genre is null)
              throw new InvalidOperationException("Böyle bir kitap türü bulunmamakta");

            _context.Genres.Remove(genre);
            _context.SaveChanges();  
        }



    }
}
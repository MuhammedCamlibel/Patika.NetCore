using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly IBookStoreDbContext _context;
        public int BookId { get; set; }
        public UpdateBookModel model { get; set; }
        public UpdateBookCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
             var book = _context.Books.Where(b=>b.Id == BookId).SingleOrDefault();
             if(book is null)
               throw new InvalidOperationException("Kitap Bulunamad─▒");
             book.Title = model.Title != default ? model.Title : book.Title;
             book.GenreId = model.GenreId != default ? model.GenreId : book.GenreId;

             _context.SaveChanges(); 

        }



    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }

    }
}
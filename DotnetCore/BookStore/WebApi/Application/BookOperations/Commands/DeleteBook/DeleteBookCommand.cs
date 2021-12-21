using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {

        private readonly BookStoreDbContext _context;
        public int BookId { get; set; }
        public DeleteBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }


        public void Handle()
        {
           var book = _context.Books.Where(b=>b.Id == BookId).SingleOrDefault();

           if(book is null)
             throw new InvalidOperationException("Kitap Bulunamadı");

           _context.Books.Remove(book);
           _context.SaveChanges();  
        }
    }
}
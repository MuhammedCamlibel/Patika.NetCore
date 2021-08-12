using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model;
        private readonly BookStoreDbContext _context;
        public CreateBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
             var book = _context.Books.FirstOrDefault(b=>b.Title == Model.Title);
             
             if(book != null)
                throw new InvalidOperationException("Kitap zaten mevcut");
             book = new Book
             {
                Title = Model.Title,
                GenreId = Model.GenreId,
                PageCount = Model.PageCount,
                PublishDate = Model.PublishDate 
             };
             _context.Books.Add(book);
             _context.SaveChanges();   
        }
    }

    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
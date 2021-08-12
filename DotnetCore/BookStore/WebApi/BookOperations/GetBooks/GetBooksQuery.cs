using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
       private readonly  BookStoreDbContext _context;
      public GetBooksQuery(BookStoreDbContext context)
      {
          _context = context;
      }

      public List<BookViewModel> Handle()
      {
          var bookList = _context.Books.OrderBy(b=>b.Id).ToList();
          List<BookViewModel> vm = new List<BookViewModel>();

          foreach (var book in bookList)
          {
              vm.Add(new BookViewModel()
              {
                 Title = book.Title,
                 Genre = ((GenreEnum)book.GenreId).ToString(),
                 PublisDate = book.PublishDate.Date.ToString("dd/mm/yyyy"),
                 PageCount = book.PageCount 
              });
          }

          return vm;
      }


    }

    public class BookViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublisDate { get; set; }
        public string Genre { get; set; }
    }
}
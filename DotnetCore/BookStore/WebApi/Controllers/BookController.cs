using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
using WebApi.DBOperations;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }
        

      [HttpGet] 
      public IActionResult GetBooks()
      {
          //return _context.Books.OrderBy(b=>b.Id).ToList<Book>();
          GetBooksQuery query = new GetBooksQuery(_context);
          var result = query.Handle(); 
          return Ok(result); 

      }

      [HttpGet("{id}")] // route dan alırız 
      public Book GetById(int id)
      {
          var book = _context.Books.Where(b=>b.Id == id).FirstOrDefault();
          if (book == default)
          {
             Console.WriteLine("Böyle bir kitap yok !"); 
          }

          return book;
      }

    //   [HttpGet]  // query den alırız
    //   public Book GetByID( [FromQuery] int id)
    //   {
    //       var book = BookList.Where(b=>b.Id == id).FirstOrDefault();
    //       if (book == default)
    //       {
    //          Console.WriteLine("Böyle bir kitap yok !"); 
    //       }

    //       return book;
    //   }
      [HttpPost]
      public IActionResult AddBook( [FromBody] CreateBookModel newBook)
      {
         CreateBookCommand command = new CreateBookCommand(_context);
         command.Model = newBook;
          try
          {
              command.Handle(); 
          }
          catch (Exception ex)
          {
              return BadRequest(ex.Message);
          } 
          
        
         return Ok(new {message = "Kitap başarı ile eklendi"});  
      } 

      [HttpPut("{id}")]
      public IActionResult UpdateBook(int id,[FromBody] Book updatedBook)
      {
          var book = _context.Books.FirstOrDefault(b=>b.Id == id);

          if(book ==null)
             return BadRequest(new {message = "Böyle bir kitap bulunmamakta !"});
          book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
          book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
          book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
          book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
          _context.SaveChanges();
          return Ok(new {message ="kitap başarı ile güncellendi."});    
      }
      
      [HttpDelete]
      public IActionResult DeleteBook(int id)
      {
          var book = _context.Books.FirstOrDefault(b=>b.Id == id);

          if(book == null)
            return BadRequest(new {message = "Böyle bir kitap bulunmamakta !"});

          _context.Books.Remove(book);
          _context.SaveChanges();
          return Ok(new {message = "Kitap başarı ile silindi."});  
      }

    }
}
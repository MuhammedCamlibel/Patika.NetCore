using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
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
      public IActionResult GetById(int id)
      {
          BookDetailViewModel result;
          GetBookDetailQuery query = new GetBookDetailQuery(_context);
          query.BookId = id; 
          try
          {
              result = query.Handle();
          }
          catch (Exception ex)
          {
              
              return BadRequest(ex.Message);
          }   

          return Ok(result);
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
      public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updatedBook)
      {
          UpdateBookCommand command = new UpdateBookCommand(_context);
          command.BookId = id;
          command.model = updatedBook; 

          try
          {
               command.Handle();

          }
          catch (Exception ex)
          {
              
              return BadRequest(ex.Message);
          } 

          return Ok("Kitap güncellendi");  
      }
      
      [HttpDelete]
      public IActionResult DeleteBook(int id)
      {
          DeleteBookCommand command = new DeleteBookCommand(_context);
          command.BookId = id ;

          try
          {
              command.Handle();
          }
          catch (Exception ex)
          {
              
              return BadRequest(ex.Message);
          } 

          return Ok("Kitap Silindi");
      }

    }
}
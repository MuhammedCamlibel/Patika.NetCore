using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(IBookStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        

      [HttpGet] 
      public IActionResult GetBooks()
      {
          //return _context.Books.OrderBy(b=>b.Id).ToList<Book>();
          GetBooksQuery query = new GetBooksQuery(_context,_mapper);
          var result = query.Handle(); 
          return Ok(result); 

      }
       
      [HttpGet("{id}")] // route dan alırız 
      public IActionResult GetById(int id)
      {
          BookDetailViewModel result;
          GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
          query.BookId = id; 
          GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
          validator.ValidateAndThrow(query);
          result = query.Handle();
        //   try
        //   {
        //       validator.ValidateAndThrow(query);
        //       result = query.Handle();
        //   }
        //   catch (Exception ex)
        //   {
              
        //       return BadRequest(ex.Message);
        //   }   

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
         CreateBookCommand command = new CreateBookCommand(_context,_mapper);
         command.Model = newBook;
         CreateBookCommandValidator validator = new CreateBookCommandValidator();
         validator.ValidateAndThrow(command);
         command.Handle();
        
         
         
         
            // try
            // {
            //     validator.ValidateAndThrow(command);  
            //     command.Handle(); 
            // }
            // catch (Exception ex)
            // {
            //     return BadRequest(ex.Message);
            // }  
           
            
         return Ok(new {message = "Kitap başarı ile eklendi"});  
      } 

      [HttpPut("{id}")]
      public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updatedBook)
      {
          UpdateBookCommand command = new UpdateBookCommand(_context);
          command.BookId = id;
          command.model = updatedBook; 
          UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
          validator.ValidateAndThrow(command);
          command.Handle();
        //   try
        //   {
        //        validator.ValidateAndThrow(command);
        //        command.Handle();

        //   }
        //   catch (Exception ex)
        //   {
              
        //       return BadRequest(ex.Message);
        //   } 

          return Ok("Kitap güncellendi");  
      }
      
      [HttpDelete("{id}")]
      public IActionResult DeleteBook(int id)
      {
          DeleteBookCommand command = new DeleteBookCommand(_context);
          command.BookId = id ;
          DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
          validator.ValidateAndThrow(command);
          command.Handle();
        //   try
        //   {
        //       validator.ValidateAndThrow(command);
        //       command.Handle();
        //   }
        //   catch (Exception ex)
        //   {
              
        //       return BadRequest(ex.Message);
        //   } 

          return Ok("Kitap Silindi");
      }

    }
}
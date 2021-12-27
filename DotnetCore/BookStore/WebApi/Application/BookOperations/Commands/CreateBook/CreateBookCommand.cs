using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model;
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommand(IBookStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
             var book = _context.Books.FirstOrDefault(b=>b.Title == Model.Title);
             
             if(book != null)
                throw new InvalidOperationException("Kitap zaten mevcut");
              book = _mapper.Map<Book>(Model);  //new Book
            //  {
            //     Title = Model.Title,
            //     GenreId = Model.GenreId,
            //     PageCount = Model.PageCount,
            //     PublishDate = Model.PublishDate 
            //  };
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

using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int AuthorId ;
        public UpdateAuthorModel model;
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateAuthorCommand(BookStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(a => a.Id == AuthorId);
            if(author is null)
             throw new InvalidOperationException("Yazar Bulunamadı");

             author.Name =  model.Name == default ?  author.Name : model.Name; 
             _context.SaveChanges();
        }

    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }
    }
}
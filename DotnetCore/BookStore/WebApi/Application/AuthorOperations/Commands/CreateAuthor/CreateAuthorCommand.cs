using System;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel model;
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommand(BookStoreDbContext context,IMapper mapper)
        {
            _context = context; 
            _mapper = mapper;
        }

        public void Handle()
        {
            _context.Authors.Add(_mapper.Map<Author>(model));
            _context.SaveChanges();
        }

    }

    public class CreateAuthorModel
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }
}
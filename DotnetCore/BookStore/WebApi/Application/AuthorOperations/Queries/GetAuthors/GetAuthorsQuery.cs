using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorViewModel> Handle()
        {
            var authors = _context.Authors.ToList();
            return _mapper.Map<List<AuthorViewModel>>(authors);
        }


        
    }

   public class AuthorViewModel
   {
       public int Id { get; set; }
       public string Name { get; set; }
       public DateTime Birthday { get; set; }
   }

}
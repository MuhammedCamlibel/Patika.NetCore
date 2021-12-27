using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorId;
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        
        public GetAuthorDetailQuery(IBookStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public AuthorDetailViewModel Handle()
        {
            var author = _context.Authors.SingleOrDefault(a=> a.Id == AuthorId );

            if(author is null)
             throw new InvalidProgramException("Yazar Bulunmamakta");
            return _mapper.Map<AuthorDetailViewModel>(author); 
        }

    }

    public class AuthorDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }
}
using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDbContext _context;
        public int AuthorId;

        public DeleteAuthorCommand(IBookStoreDbContext contex)
        {
            _context = contex;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(a=> a.Id == AuthorId);
            if(author is null)
              throw new InvalidOperationException("Silinecek Yazar Bulunamadı");

            _context.Authors.Remove(author);
            _context.SaveChanges();  
        }
    }
}
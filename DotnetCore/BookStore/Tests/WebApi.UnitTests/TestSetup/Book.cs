using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(

                     new Book()
                    {
                       
                        Title = "Lord Of The Rings",
                        GenreId = 1,
                        PageCount = 150,
                        PublishDate = new DateTime(1998,03,31)
                    },
                    new Book()
                    {
                        
                        Title = "Hobbit",
                        GenreId = 2,
                        PageCount = 160,
                        PublishDate = new DateTime(1999,03,31)
                    },
                    new Book()
                    {
                       
                        Title = "Acı Hayat",
                        GenreId = 1,
                        PageCount = 170,
                        PublishDate = new DateTime(2000,03,31)
                    }
                    
                ); 
        }
    }
}
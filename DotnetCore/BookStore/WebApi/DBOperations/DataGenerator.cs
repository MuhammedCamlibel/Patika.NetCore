using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any())
                  return;

                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personal Growth"
                     },
                     new Genre
                     {
                         Name = "Science Fiction"
                     },
                     new Genre 
                     {
                         Name = "Romance"
                     }
                );

                context.Authors.AddRange(

                    new Author{
                        Name = "J.R.R Tolkien",
                        Birthday = new DateTime(1892,01,03)
                    },
                    new Author{
                        Name = "Muhammed Çamlıbel",
                        Birthday = new DateTime(1998,03,31)
                    }

                );

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

                context.SaveChanges();  
            }
        }
    }
}
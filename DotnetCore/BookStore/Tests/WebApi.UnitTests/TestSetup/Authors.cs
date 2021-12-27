using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
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
        }
    }
}
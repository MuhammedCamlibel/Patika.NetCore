using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)
            
            var book = new Book(){Title = "WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn" , PageCount = 100, PublishDate = new System.DateTime(1992,01,01),GenreId =1};
            _context.Books.Add(book);
            _context.SaveChanges();

            var command = new CreateBookCommand(_context,_mapper);
            command.Model = new CreateBookModel(){Title = book.Title};
            
            //act & assert (Çalıştırma - Doğrulama)

            FluentActions
                  .Invoking(()=> command.Handle())
                  .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut"); 

            //assert (Doğrulama)
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
           //arrange

           var command = new CreateBookCommand(_context,_mapper);
           var model = new CreateBookModel(){Title="Dune",PageCount=500,GenreId=1,PublishDate=DateTime.Now.AddYears(-2)};
           command.Model = model;

           //act

           FluentActions.Invoking(() => command.Handle()).Invoke();

           //assert

           var book = _context.Books.SingleOrDefault(b => b.Title == model.Title);

           book.Should().NotBeNull();
           book.PageCount.Should().Be(model.PageCount);
           book.Title.Should().Be(model.Title);
           book.PublishDate.Should().Be(model.PublishDate);
           book.GenreId.Should().Be(model.GenreId);
           book.Id.Should().BeGreaterThan(0);

        }
    }
}
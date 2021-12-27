using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Lord Of The Rings",0,0)]
        [InlineData("Lord Of The Rings",0,1)]
        [InlineData("Lord Of The Rings",150,0)]
        [InlineData("",150,0)]
        [InlineData("",0,0)]
        [InlineData("Lor",0,0)]
        [InlineData("Lor",1,0)]
        [InlineData("Lor",0,1)]
        [InlineData("Lord Of The Rings",0,150)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title,int genreId,int pageCount)
        {
            //arrange

            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model = new CreateBookModel()
            {
               Title=title,GenreId=genreId,PageCount=pageCount,PublishDate=System.DateTime.Now.AddYears(-1)
            };

            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [Fact]
        public void WhenDatetimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {

           //arrange

           CreateBookCommand command = new CreateBookCommand(null,null);
           command.Model = new CreateBookModel()
           {
              GenreId=1,
              PageCount=150,
              Title="Lord Of The Rings",
              PublishDate = System.DateTime.Now.Date
           };

           //act

           CreateBookCommandValidator validator = new CreateBookCommandValidator();
           var result = validator.Validate(command);


           //assert 

           result.Errors.Count.Should().BeGreaterThan(0);

        }

        //Tüm inputların doğru olması  
         [Fact]
        public void WhenValidInputsGiven_Validator_ShouldNotBeReturnError()
        {

           //arrange

           CreateBookCommand command = new CreateBookCommand(null,null);
           command.Model = new CreateBookModel()
           {
              GenreId=1,
              PageCount=150,
              Title="Lord Of The Rings",
              PublishDate = System.DateTime.Now.AddYears(-1)
           };

           //act

           CreateBookCommandValidator validator = new CreateBookCommandValidator();
           var result = validator.Validate(command);


           //assert 

           result.Errors.Count.Should().Equals(0);

        }

    }
}
using AutoMapper;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Entities;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel,Book>();
            CreateMap<Book,BookDetailViewModel>().ForMember(dest=>dest.Genre , opt=>opt.MapFrom(src=> src.Genre.Name));
            CreateMap<Book,BookViewModel>().ForMember(dest=>dest.Genre, opt=> opt.MapFrom(src=> src.Genre.Name));

            CreateMap<Genre,GenreViewModel>().ReverseMap();
            CreateMap<Genre,GenreDetailViewModel>().ReverseMap();
            CreateMap<Genre,CreateGenreModel>().ReverseMap();
        }
    }
}
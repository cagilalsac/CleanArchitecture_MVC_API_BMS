using Application.Common.Contexts.Bases;
using Application.Common.Handlers.Bases;
using Application.Common.Responses.Bases;
using Application.Features.Authors;
using Application.Features.Genres;
using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Books
{
    public record ReadBookApiRequest : IRequest<IQueryable<ReadBookApiResponse>>;
    public record ReadBookApiResponse : ResponseBase
    {
        public string Name { get; set; }
        public string? Isbn { get; set; }
        public short? NumberOfPages { get; set; }
        public DateTime? PublishDate { get; set; }
        public BookTypesEnum BookTypeEnum { get; set; }
        public string BookType { get; set; }
        public decimal? Price { get; set; }
        public bool IsTopSeller { get; set; }
        public ReadAuthorApiResponse Author { get; set; }
        public List<ReadGenreResponse> Genres { get; set; }
    }

    public class ReadBookApiHandler : HandlerBase, IRequestHandler<ReadBookApiRequest, IQueryable<ReadBookApiResponse>>
    {
        public ReadBookApiHandler(IDb db) : base(db)
        {
        }

        public Task<IQueryable<ReadBookApiResponse>> Handle(ReadBookApiRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_db.Books.Include(b => b.Author).Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)
                .OrderByDescending(b => b.IsTopSeller).ThenByDescending(b => b.PublishDate).ThenBy(b => b.Name)
                .Select(b => new ReadBookApiResponse()
                {
                    Id = b.Id,
                    Name = b.Name,
                    Isbn = b.Isbn,
                    NumberOfPages = b.NumberOfPages,
                    PublishDate = b.PublishDate,
                    BookTypeEnum = b.BookType,
                    BookType = ((b.BookType.HasFlag(BookTypesEnum.Print) ? BookTypesEnum.Print.ToString() + ", " : "")
                        + (b.BookType.HasFlag(BookTypesEnum.Digital) ? BookTypesEnum.Digital.ToString() + ", " : "")
                        + (b.BookType.HasFlag(BookTypesEnum.Audio) ? BookTypesEnum.Audio.ToString() : "")).TrimEnd(", ".ToCharArray()),
                    Price = b.Price,
                    IsTopSeller = b.IsTopSeller,
                    Author = new ReadAuthorApiResponse()
                    {
                        Id = b.Author.Id,
                        Name = b.Author.Name,
                        Surname = b.Author.Surname
                    },
                    Genres = b.BookGenres.Select(bg => new ReadGenreResponse()
                    {
                        Id = bg.Genre.Id,
                        Name = bg.Genre.Name
                    }).ToList()
                }));
        }
    }
}

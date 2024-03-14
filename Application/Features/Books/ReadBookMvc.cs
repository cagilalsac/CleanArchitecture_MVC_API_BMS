using Application.Common.Contexts.Bases;
using Application.Common.Handlers.Bases;
using Application.Features.Authors;
using Application.Features.Genres;
using Domain.Common;
using Domain.Entities.Bases;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Application.Features.Books
{
    public record ReadBookMvcRequest : IRequest<IQueryable<ReadBookMvcResponse>>;
    public record ReadBookMvcResponse : IRecord
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DisplayName("ISBN")]
        public string Isbn { get; set; }

        [DisplayName("Number of Pages")]
        public short? NumberOfPages { get; set; }

        [DisplayName("Publish Date")]
        public string PublishDate { get; set; }

        [DisplayName("Book Type")]
        public string BookType { get; set; }

        public string Price { get; set; }

        [DisplayName("Top Seller")]
        public string IsTopSeller { get; set; }

        public ReadAuthorMvcResponse Author { get; set; }
        public List<ReadGenreResponse> Genres { get; set; }
    }

    public class ReadBookMvcHandler : HandlerBase, IRequestHandler<ReadBookMvcRequest, IQueryable<ReadBookMvcResponse>>
    {
        public ReadBookMvcHandler(IDb db) : base(db)
        {
        }

        public Task<IQueryable<ReadBookMvcResponse>> Handle(ReadBookMvcRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_db.Books.Include(b => b.Author).Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)
                .OrderByDescending(b => b.IsTopSeller).ThenByDescending(b => b.PublishDate).ThenBy(b => b.Name)
                .Select(b => new ReadBookMvcResponse()
                {
                    Id = b.Id,
                    Name = b.Name,
                    Isbn = b.Isbn,
                    NumberOfPages = b.NumberOfPages,
                    PublishDate = b.PublishDate.HasValue ? b.PublishDate.Value.ToString("MM/dd/yyyy") : string.Empty,
                    BookType = ((b.BookType.HasFlag(BookTypesEnum.Print) ? BookTypesEnum.Print.ToString() + ", " : "")
                        + (b.BookType.HasFlag(BookTypesEnum.Digital) ? BookTypesEnum.Digital.ToString() + ", " : "")
                        + (b.BookType.HasFlag(BookTypesEnum.Audio) ? BookTypesEnum.Audio.ToString() : "")).TrimEnd(", ".ToCharArray()),
                    Price = (b.Price ?? 0).ToString("C2"),
                    IsTopSeller = b.IsTopSeller ? "Yes" : "No",
                    Author = new ReadAuthorMvcResponse()
                    {
                        Id = b.Author.Id,
                        FullName = b.Author.Name + " " + b.Author.Surname
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

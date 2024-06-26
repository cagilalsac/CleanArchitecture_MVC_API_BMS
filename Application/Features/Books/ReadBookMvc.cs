﻿using Application.Contexts.Bases;
using Application.Common.Handlers.Bases;
using Application.Features.Authors;
using Application.Features.Genres;
using Domain.Common.Records.Bases;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Books
{
    public record ReadBookMvcRequest : Record, IRequest<IQueryable<ReadBookMvcResponse>>;
    public record ReadBookMvcResponse : Record
    {
        public string Name { get; set; }
        public string? Isbn { get; set; }
        public short? NumberOfPages { get; set; }
        public string? PublishDate { get; set; }
        public string BookType { get; set; }
        public string? Price { get; set; }
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

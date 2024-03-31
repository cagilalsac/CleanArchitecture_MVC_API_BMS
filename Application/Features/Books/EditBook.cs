using Application.Common.Contexts.Bases;
using Application.Common.Handlers.Bases;
using Domain.Common;
using Domain.Common.Records.Bases;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Books
{
    public record EditBookRequest : Record, IRequest<UpdateBookRequest>;

    public class EditBookHandler : HandlerBase, IRequestHandler<EditBookRequest, UpdateBookRequest>
    {
        public EditBookHandler(IDb db) : base(db)
        {
        }

        public async Task<UpdateBookRequest> Handle(EditBookRequest request, CancellationToken cancellationToken)
        {
            Book entity = await _db.Books.Include(b => b.BookGenres).SingleOrDefaultAsync(b => b.Id == request.Id);
            if (entity is null)
                return null;
            List<int> bookTypes = new List<int>();
            if (entity.BookType.HasFlag(BookTypesEnum.Print))
                bookTypes.Add((int)BookTypesEnum.Print);
            if (entity.BookType.HasFlag(BookTypesEnum.Digital))
                bookTypes.Add((int)BookTypesEnum.Digital);
            if (entity.BookType.HasFlag(BookTypesEnum.Audio))
                bookTypes.Add((int)BookTypesEnum.Audio);
            List<int> genreIds = entity.BookGenres?.Select(bg => bg.GenreId).ToList();
            return new UpdateBookRequest()
            {
                Id = entity.Id,
                Name = entity.Name,
                Isbn = entity.Isbn,
                NumberOfPages = entity.NumberOfPages,
                PublishDate = entity.PublishDate,
                BookTypes = bookTypes,
                Price = entity.Price,
                IsTopSeller = entity.IsTopSeller,
                AuthorId = entity.AuthorId,
                GenreIds = genreIds
            };
        }
    }
}

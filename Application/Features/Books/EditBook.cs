using Application.Common.Contexts.Bases;
using Application.Common.Handlers.Bases;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Books
{
    public record EditBookRequest(int Id) : IRequest<UpdateBookRequest>;

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
            return new UpdateBookRequest(entity.Id, entity.Name, entity.Isbn, entity.NumberOfPages, entity.PublishDate,
                bookTypes, entity.Price, entity.IsTopSeller, entity.AuthorId, genreIds);
        }
    }
}

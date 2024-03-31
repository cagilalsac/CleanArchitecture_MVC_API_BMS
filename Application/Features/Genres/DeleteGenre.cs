using Application.Common.Contexts.Bases;
using Application.Common.Handlers.Bases;
using Application.Common.Responses;
using Application.Common.Responses.Bases;
using Domain.Common.Records.Bases;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Genres
{
    public record DeleteGenreRequest : Record, IRequest<Response>;

    public class DeleteGenreHandler : HandlerBase, IRequestHandler<DeleteGenreRequest, Response>
    {
        public DeleteGenreHandler(IDb db) : base(db)
        {
        }

        public async Task<Response> Handle(DeleteGenreRequest request, CancellationToken cancellationToken)
        {
            Genre entity = await _db.Genres.Include(g => g.BookGenres).SingleOrDefaultAsync(g => g.Id == request.Id);
            if (entity is null)
                return new ErrorResponse("Genre not found!");
            _db.BookGenres.RemoveRange(entity.BookGenres);
            _db.Genres.Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return new SuccessResponse("Genre deleted successfully.");
        }
    }
}

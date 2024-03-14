using Application.Common.Contexts.Bases;
using Application.Common.Handlers.Bases;
using Application.Common.Responses;
using Application.Common.Responses.Bases;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Books
{
    public record DeleteBookRequest(int Id) : IRequest<Response>;

    public class DeleteBookHandler : HandlerBase, IRequestHandler<DeleteBookRequest, Response>
    {
        public DeleteBookHandler(IDb db) : base(db)
        {
        }

        public async Task<Response> Handle(DeleteBookRequest request, CancellationToken cancellationToken)
        {
            Book entity = await _db.Books.Include(b => b.BookGenres).SingleOrDefaultAsync(b => b.Id == request.Id);
            if (entity is null)
                return new ErrorResponse("Book not found!");
            _db.BookGenres.RemoveRange(entity.BookGenres);
            _db.Books.Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return new SuccessResponse();
        }
    }
}

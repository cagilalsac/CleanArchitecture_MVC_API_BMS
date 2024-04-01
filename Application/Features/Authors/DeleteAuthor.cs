using Application.Contexts.Bases;
using Application.Common.Handlers.Bases;
using Application.Common.Responses;
using Application.Common.Responses.Bases;
using Domain.Common.Records.Bases;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Authors
{
    public record DeleteAuthorRequest : Record, IRequest<Response>;

    public class DeleteAuthorHandler : HandlerBase, IRequestHandler<DeleteAuthorRequest, Response>
    {
        public DeleteAuthorHandler(IDb db) : base(db)
        {
        }

        public async Task<Response> Handle(DeleteAuthorRequest request, CancellationToken cancellationToken)
        {
            Author entity = await _db.Authors.Include(a => a.Books).SingleOrDefaultAsync(a => a.Id == request.Id);
            if (entity is null)
                return new ErrorResponse("Author not found!");
            if (entity.Books is not null && entity.Books.Any())
                return new ErrorResponse("Author can't be deleted because author has relational books!");
            _db.Authors.Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return new SuccessResponse();
        }
    }
}

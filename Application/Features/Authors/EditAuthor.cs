using Application.Common.Contexts.Bases;
using Application.Common.Handlers.Bases;
using Domain.Entities;
using MediatR;

namespace Application.Features.Authors
{
    public record EditAuthorRequest(int Id) : IRequest<UpdateAuthorRequest>;

    public class EditAuthorHandler : HandlerBase, IRequestHandler<EditAuthorRequest, UpdateAuthorRequest>
    {
        public EditAuthorHandler(IDb db) : base(db)
        {
        }

        public async Task<UpdateAuthorRequest> Handle(EditAuthorRequest request, CancellationToken cancellationToken)
        {
            Author entity = await _db.Authors.FindAsync(request.Id);
            if (entity is null)
                return null;
            return new UpdateAuthorRequest(entity.Id, entity.Name, entity.Surname);
        }
    }
}

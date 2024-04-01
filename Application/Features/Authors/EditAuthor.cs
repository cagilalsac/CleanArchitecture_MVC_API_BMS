using Application.Contexts.Bases;
using Application.Common.Handlers.Bases;
using Domain.Common.Records.Bases;
using Domain.Entities;
using MediatR;

namespace Application.Features.Authors
{
    public record EditAuthorRequest : Record, IRequest<UpdateAuthorRequest>;

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
            return new UpdateAuthorRequest() { Id = entity.Id, Name = entity.Name, Surname = entity.Surname };
        }
    }
}

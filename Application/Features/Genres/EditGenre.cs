using Application.Contexts.Bases;
using Application.Common.Handlers.Bases;
using Domain.Common.Records.Bases;
using Domain.Entities;
using MediatR;

namespace Application.Features.Genres
{
    public record EditGenreRequest : Record, IRequest<UpdateGenreRequest>;

    public class EditGenreHandler : HandlerBase, IRequestHandler<EditGenreRequest, UpdateGenreRequest>
    {
        public EditGenreHandler(IDb db) : base(db)
        {
        }

        public async Task<UpdateGenreRequest> Handle(EditGenreRequest request, CancellationToken cancellationToken)
        {
            Genre entity = await _db.Genres.FindAsync(request.Id);
            if (entity is null)
                return null;
            return new UpdateGenreRequest() { Id = entity.Id, Name = entity.Name };
        }
    }
}

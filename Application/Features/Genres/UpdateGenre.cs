using Application.Common.Contexts.Bases;
using Application.Common.Handlers.Bases;
using Application.Common.Responses;
using Application.Common.Responses.Bases;
using Domain.Common.Records.Bases;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Genres
{
    public record UpdateGenreRequest : Record, IRequest<Response>
    {
        public string Name { get; set; }
    }

    public class UpdateGenreValidator : AbstractValidator<UpdateGenreRequest>
    {
        public UpdateGenreValidator()
        {
            RuleFor(cgr => cgr.Name)
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(30);
        }
    }

    public class UpdateGenreHandler : HandlerBase, IRequestHandler<UpdateGenreRequest, Response>
    {
        public UpdateGenreHandler(IDb db) : base(db)
        {
        }

        public async Task<Response> Handle(UpdateGenreRequest request, CancellationToken cancellationToken)
        {
            if (_db.Genres.Any(g => g.Id != request.Id && g.Name.ToLower() == request.Name.ToLower().Trim()))
                return new ErrorResponse("Genre with the same name exists!");
            Genre entity = await _db.Genres.FindAsync(request.Id);
            if (entity is null)
                return new ErrorResponse("Genre not found!");
            entity.Name = request.Name.Trim();
            _db.Genres.Update(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return new SuccessResponse("Genre updated successfully.", entity.Id);
        }
    }
}

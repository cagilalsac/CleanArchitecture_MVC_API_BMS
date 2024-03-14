using Application.Common.Contexts.Bases;
using Application.Common.Handlers.Bases;
using Application.Common.Responses;
using Application.Common.Responses.Bases;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Genres
{
    public record CreateGenreRequest(string Name) : IRequest<Response>;

    public class CreateGenreValidator : AbstractValidator<CreateGenreRequest>
    {
        public CreateGenreValidator()
        {
            RuleFor(cgr => cgr.Name)
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(30);
        }
    }

    public class CreateGenreHandler : HandlerBase, IRequestHandler<CreateGenreRequest, Response>
    {
        public CreateGenreHandler(IDb db) : base(db)
        {
        }

        public async Task<Response> Handle(CreateGenreRequest request, CancellationToken cancellationToken)
        {
            if (_db.Genres.Any(g => g.Name.ToLower() == request.Name.ToLower().Trim()))
                return new ErrorResponse("Genre with the same name exists!");
            Genre entity = new Genre()
            {
                Name = request.Name.Trim()
            };
            _db.Genres.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return new SuccessResponse("Genre created successfully.", entity.Id);
        }
    }
}

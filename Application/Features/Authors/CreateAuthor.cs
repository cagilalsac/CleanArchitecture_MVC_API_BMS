using Application.Contexts.Bases;
using Application.Common.Handlers.Bases;
using Application.Common.Responses;
using Application.Common.Responses.Bases;
using Domain.Common.Records.Bases;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Authors
{
    public record CreateAuthorRequest : Record, IRequest<Response>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class CreateAuthorValidator : AbstractValidator<CreateAuthorRequest>
    {
        public CreateAuthorValidator()
        {
            RuleFor(a => a.Name)
                .NotNull().WithMessage("Name is required!")
                .NotEmpty().WithMessage("Name is required!")
                .MaximumLength(50).WithMessage("Name must be maximum 50 characters!");
            RuleFor(a => a.Surname)
                .NotNull().WithMessage("Surname is required!")
                .MinimumLength(2).WithMessage("Surname must be minimum 2 characters!!")
                .MaximumLength(50).WithMessage("Surname must be maximum 50 characters!");
        }
    }

    public class CreateAuthorHandler : HandlerBase, IRequestHandler<CreateAuthorRequest, Response>
    {
        public CreateAuthorHandler(IDb db) : base(db)
        {
        }

        public async Task<Response> Handle(CreateAuthorRequest request, CancellationToken cancellationToken)
        {
            if (_db.Authors.Any(a => a.Name.ToLower() == request.Name.ToLower().Trim() && a.Surname.ToLower() == request.Surname.ToLower().Trim()))
                return new ErrorResponse("Author with the same name and surname exists!");
            Author entity = new Author()
            {
                Name = request.Name.Trim(),
                Surname = request.Surname.Trim()
            };
            _db.Authors.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return new SuccessResponse(entity.Id);
        }
    }
}

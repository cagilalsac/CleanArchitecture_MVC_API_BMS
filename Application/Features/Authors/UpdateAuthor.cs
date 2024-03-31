using Application.Common.Contexts.Bases;
using Application.Common.Handlers.Bases;
using Application.Common.Responses;
using Application.Common.Responses.Bases;
using Domain.Common.Records.Bases;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Authors
{
    public record UpdateAuthorRequest : Record, IRequest<Response>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class UpdateAuthorValidator : AbstractValidator<UpdateAuthorRequest>
    {
        public UpdateAuthorValidator()
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

    public class UpdateAuthorHandler : HandlerBase, IRequestHandler<UpdateAuthorRequest, Response>
    {
        public UpdateAuthorHandler(IDb db) : base(db)
        {
        }

        public async Task<Response> Handle(UpdateAuthorRequest request, CancellationToken cancellationToken)
        {
            if (_db.Authors.Any(a => a.Id != request.Id && a.Name.ToLower() == request.Name.ToLower().Trim() && a.Surname.ToLower() == request.Surname.ToLower().Trim()))
                return new ErrorResponse("Author with the same name and surname exists!");
            Author entity = await _db.Authors.FindAsync(request.Id);
            if (entity is null)
                return new ErrorResponse("Author not found!");
            entity.Name = request.Name.Trim();
            entity.Surname = request.Surname.Trim();
            _db.Authors.Update(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return new SuccessResponse(entity.Id);
        }
    }
}

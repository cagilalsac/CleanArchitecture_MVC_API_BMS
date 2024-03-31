using Application.Common.Contexts.Bases;
using Application.Common.Handlers.Bases;
using Application.Common.Responses;
using Application.Common.Responses.Bases;
using Domain.Common;
using Domain.Common.Records.Bases;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Books
{
    public record CreateBookRequest : Record, IRequest<Response>
    {
        public string Name { get; set; }
        public string? Isbn { get; set; }
        public short? NumberOfPages { get; set; }
        public DateTime? PublishDate { get; set; }
        public List<int> BookTypes { get; set; }
        public decimal? Price { get; set; }
        public bool IsTopSeller { get; set; }
        public int? AuthorId { get; set; }
        public List<int> GenreIds { get; set; }
    }

    public class CreateBookValidator : AbstractValidator<CreateBookRequest>
    {
        public CreateBookValidator()
        {
            RuleFor(b => b.Name)
                .NotNull().WithMessage("Name is required!")
                .NotEmpty().WithMessage("Name is required!")
                .MaximumLength(100).WithMessage("Name must be maximum 100 characters!");
            RuleFor(b => b.Isbn)
                .Length(13).WithMessage("ISBN must be 13 characters!");
            RuleFor(b => b.NumberOfPages)
                .GreaterThanOrEqualTo<CreateBookRequest, short>(0).WithMessage("Number Of Pages must be zero or positive!");
            RuleFor(b => b.PublishDate)
                .LessThan(DateTime.Today.AddDays(1)).WithMessage("Publish Date must be before " + DateTime.Today.AddDays(1).ToString("MM.dd.yyyy"));
            RuleFor(b => b.BookTypes)
                .NotEmpty().WithMessage("Book Type is required!");
            RuleFor(b => b.Price)
                .GreaterThan(0).WithMessage("Price must be positive!");
            RuleFor(b => b.AuthorId)
                .NotNull().WithMessage("Author is required!");
        }
    }

    public class CreateBookHandler : HandlerBase, IRequestHandler<CreateBookRequest, Response>
    {
        public CreateBookHandler(IDb db) : base(db)
        {
        }

        public async Task<Response> Handle(CreateBookRequest request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(request.Isbn) && _db.Books.Any(b => (b.Isbn ?? string.Empty).ToUpper() == (request.Isbn ?? string.Empty).ToUpper().Trim()))
                return new ErrorResponse("Book with the same ISBN exists!");
            Book entity = new Book()
            {
                Name = request.Name.Trim(),
                Isbn = request.Isbn?.Trim(),
                NumberOfPages = request.NumberOfPages,
                PublishDate = request.PublishDate,
                BookType = (BookTypesEnum)request.BookTypes.Sum(),
                Price = request.Price,
                IsTopSeller = request.IsTopSeller,
                AuthorId = request.AuthorId.Value,
                BookGenres = request.GenreIds?.Select(gId => new BookGenre()
                {
                    GenreId = gId
                }).ToList()
            };
            _db.Books.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return new SuccessResponse(entity.Id);
        }
    }
}

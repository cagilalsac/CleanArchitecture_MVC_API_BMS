using Application.Contexts.Bases;
using Application.Common.Handlers.Bases;
using Application.Common.Responses;
using Application.Common.Responses.Bases;
using Domain.Common.Records.Bases;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Books
{
    public record UpdateBookRequest : Record, IRequest<Response>
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

    public class UpdateBookValidator : AbstractValidator<UpdateBookRequest>
    {
        public UpdateBookValidator()
        {
            RuleFor(b => b.Name)
                .NotNull().WithMessage("Name is required!")
                .NotEmpty().WithMessage("Name is required!")
                .MaximumLength(100).WithMessage("Name must be maximum 100 characters!");
            RuleFor(b => b.Isbn)
                .Length(13).WithMessage("ISBN must be 13 characters!");
            RuleFor(b => b.NumberOfPages)
                .GreaterThanOrEqualTo<UpdateBookRequest, short>(0).WithMessage("Number Of Pages must be zero or positive!");
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

    public class UpdateBookHandler : HandlerBase, IRequestHandler<UpdateBookRequest, Response>
    {
        public UpdateBookHandler(IDb db) : base(db)
        {
        }

        public async Task<Response> Handle(UpdateBookRequest request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(request.Isbn) &&
                _db.Books.Any(b => b.Id != request.Id && (b.Isbn ?? string.Empty).ToUpper() == (request.Isbn ?? string.Empty).ToUpper().Trim()))
            {
                return new ErrorResponse("Book with the same ISBN exists!");
            }
            Book entity = await _db.Books.Include(b => b.BookGenres).SingleOrDefaultAsync(b => b.Id == request.Id);
            if (entity is null)
                return new ErrorResponse("Book not found!");
            _db.BookGenres.RemoveRange(entity.BookGenres);
            entity.Name = request.Name.Trim();
            entity.Isbn = request.Isbn?.Trim();
            entity.NumberOfPages = request.NumberOfPages;
            entity.PublishDate = request.PublishDate;
            entity.BookType = (BookTypesEnum)request.BookTypes.Sum();
            entity.Price = request.Price;
            entity.IsTopSeller = request.IsTopSeller;
            entity.AuthorId = request.AuthorId.Value;
            entity.BookGenres = request.GenreIds?.Select(gId => new BookGenre()
            {
                GenreId = gId
            }).ToList();
            _db.Books.Update(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return new SuccessResponse(entity.Id);
        }
    }
}

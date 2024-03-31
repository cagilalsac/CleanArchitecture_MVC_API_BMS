using Application.Common.Contexts.Bases;
using Application.Common.Handlers.Bases;
using Domain.Entities.Bases;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Authors
{
    public record ReadAuthorMvcRequest : IRequest<IQueryable<ReadAuthorMvcResponse>>;
    public record ReadAuthorMvcResponse : IRecord
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string BooksCount { get; set; }
        public string Books { get; set; }
    }

    public class ReadAuthorMvcHandler : HandlerBase, IRequestHandler<ReadAuthorMvcRequest, IQueryable<ReadAuthorMvcResponse>>
    {
        public ReadAuthorMvcHandler(IDb db) : base(db)
        {
        }

        public Task<IQueryable<ReadAuthorMvcResponse>> Handle(ReadAuthorMvcRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_db.Authors.Include(a => a.Books).OrderByDescending(a => a.Books.Count).ThenBy(a => a.Name).ThenBy(a => a.Surname)
                .Select(a => new ReadAuthorMvcResponse()
                {
                    Id = a.Id,
                    FullName = a.Name + " " + a.Surname,
                    BooksCount = a.Books.Count + " book(s)",
                    Books = string.Join("<br />", a.Books.OrderBy(b => b.Name).Select(b => b.Name))
                }));
        }
    }
}

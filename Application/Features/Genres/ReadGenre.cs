using Application.Contexts.Bases;
using Application.Common.Handlers.Bases;
using Domain.Common.Records.Bases;
using MediatR;

namespace Application.Features.Genres
{
    public record ReadGenreRequest : Record, IRequest<IQueryable<ReadGenreResponse>>;
    public record ReadGenreResponse : Record
    {
        public string Name { get; set; }
    }

    public class ReadGenreHandler : HandlerBase, IRequestHandler<ReadGenreRequest, IQueryable<ReadGenreResponse>>
    {
        public ReadGenreHandler(IDb db) : base(db)
        {
        }

        public Task<IQueryable<ReadGenreResponse>> Handle(ReadGenreRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_db.Genres.OrderBy(g => g.Name).Select(g => new ReadGenreResponse
            {
                Id = g.Id,
                Name = g.Name
            }));
        }
    }
}

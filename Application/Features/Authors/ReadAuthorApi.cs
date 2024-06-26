﻿using Application.Contexts.Bases;
using Application.Common.Handlers.Bases;
using Domain.Common.Records.Bases;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Authors
{
    public record ReadAuthorApiRequest : Record, IRequest<IQueryable<ReadAuthorApiResponse>>;

    public record ReadAuthorApiResponse : Record
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class ReadAuthorApiHandler : HandlerBase, IRequestHandler<ReadAuthorApiRequest, IQueryable<ReadAuthorApiResponse>>
    {
        public ReadAuthorApiHandler(IDb db) : base(db)
        {
        }

        public Task<IQueryable<ReadAuthorApiResponse>> Handle(ReadAuthorApiRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_db.Authors.Include(a => a.Books).OrderByDescending(a => a.Books.Count).ThenBy(a => a.Name).ThenBy(a => a.Surname)
                .Select(a => new ReadAuthorApiResponse()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Surname = a.Surname
                }));
        }
    }
}

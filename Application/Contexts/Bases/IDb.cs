using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Contexts.Bases
{
    public interface IDb
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

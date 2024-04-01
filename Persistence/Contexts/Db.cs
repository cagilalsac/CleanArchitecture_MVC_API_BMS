using Application.Contexts.Bases;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class Db : DbContext, IDb
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }

        public Db(DbContextOptions<Db> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region String Property Maximum Lengths
            modelBuilder.Entity<Book>().Property(b => b.Name).HasMaxLength(100);
            modelBuilder.Entity<Book>().Property(b => b.Isbn).HasMaxLength(13);

            modelBuilder.Entity<Author>().Property(a => a.Name).HasMaxLength(50);
            modelBuilder.Entity<Author>().Property(a => a.Surname).HasMaxLength(50);

            modelBuilder.Entity<Genre>().Property(a => a.Name).HasMaxLength(30);
            #endregion

            #region Relationships
            modelBuilder.Entity<Author>().HasMany(a => a.Books).WithOne(b => b.Author).HasForeignKey(b => b.AuthorId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BookGenre>().HasOne(bg => bg.Book).WithMany(b => b.BookGenres).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BookGenre>().HasOne(bg => bg.Genre).WithMany(g => g.BookGenres).OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region Unique Indices
            modelBuilder.Entity<BookGenre>().HasIndex(bg => new { bg.BookId, bg.GenreId }).IsUnique();
            #endregion
        }
    }
}

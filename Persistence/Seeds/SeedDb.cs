using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.Globalization;

namespace Persistence.Seeds
{
    public class SeedDb
    {
        public void Initialize()
        {
            var dbFactory = new DbFactory();
            var db = dbFactory.CreateDbContext([]);

            var bookGenres = db.BookGenres.ToList();
            var genres = db.Genres.ToList();
            var books = db.Books.ToList();
            var authors = db.Authors.ToList();

            db.BookGenres.RemoveRange(bookGenres);
            db.Genres.RemoveRange(genres);
            db.Books.RemoveRange(books);
            db.Authors.RemoveRange(authors);

            if (bookGenres.Count > 0)
            {
                db.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('BookGenres', RESEED, 0)");
            }
            if (genres.Count > 0)
            {
                db.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('Genres', RESEED, 0)");
            }
            if (books.Count > 0)
            {
                db.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('Books', RESEED, 0)");
            }
            if (authors.Count > 0)
            {
                db.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('Authors', RESEED, 0)");
            }

            db.Genres.Add(new Genre()
            {
                Name = "Mystery"
            });
            db.Genres.Add(new Genre()
            {
                Name = "Children's fiction"
            });
            db.Genres.Add(new Genre()
            {
                Name = "Fantasy"
            });

            db.SaveChanges();

            db.Books.Add(new Book()
            {
                Name = "Harry Potter and the Philosopher's Stone",
                NumberOfPages = 223,
                Price = 10.44m,
                BookType = BookTypesEnum.Print | BookTypesEnum.Digital | BookTypesEnum.Audio,
                IsTopSeller = true,
                PublishDate = new DateTime(1997, 6, 26),
                Author = new Author()
                {
                    Name = "J. K.",
                    Surname = "Rowling"
                },
                BookGenres = new List<BookGenre>()
                {
                    new BookGenre()
                    {
                        GenreId = db.Genres.SingleOrDefault(g => g.Name == "Fantasy").Id
                    },
                    new BookGenre()
                    {
                        GenreId = db.Genres.SingleOrDefault(g => g.Name == "Children's fiction").Id
                    }
                }
            });
            db.Books.Add(new Book()
            {
                Name = "The Da Vinci Code",
                Isbn = "0-385-50420-9",
                NumberOfPages = 489,
                Price = 9.99M,
                BookType = BookTypesEnum.Audio,
                IsTopSeller = true,
                PublishDate = DateTime.Parse("2003/03/18", new CultureInfo("en-US")),
                Author = new Author()
                {
                    Name = "Dan",
                    Surname = "Brown"
                },
                BookGenres = new List<BookGenre>()
                {
                    new BookGenre()
                    {
                        GenreId = db.Genres.SingleOrDefault(g => g.Name == "Mystery").Id
                    }
                }
            });
            db.Books.Add(new Book()
            {
                Name = "The Alchemist",
                NumberOfPages = 208,
                Price = 14.24m,
                BookType = BookTypesEnum.Print | BookTypesEnum.Digital,
                PublishDate = DateTime.Parse("2014/04/15", new CultureInfo("en-US")),
                Author = new Author()
                {
                    Name = "Paulo",
                    Surname = "Coelho"
                },
                BookGenres = new List<BookGenre>()
                {
                    new BookGenre()
                    {
                        GenreId = db.Genres.SingleOrDefault(g => g.Name == "Fantasy").Id
                    }
                }
            });

            db.SaveChanges();
        }
    }
}

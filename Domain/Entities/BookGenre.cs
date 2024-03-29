using Domain.Entities.Bases;

namespace Domain.Entities
{
    public class BookGenre : Record
    {
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}

using Domain.Entities.Bases;

namespace Domain.Entities
{
    public class Genre : Record
    {
        public string Name { get; set; } = null!;

        public List<BookGenre>? BookGenres { get; set; }
    }
}

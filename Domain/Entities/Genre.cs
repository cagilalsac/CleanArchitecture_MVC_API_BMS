using Domain.Common.Entities.Bases;

namespace Domain.Entities
{
    public class Genre : Entity
    {
        public string Name { get; set; } = null!;

        public List<BookGenre>? BookGenres { get; set; }
    }
}

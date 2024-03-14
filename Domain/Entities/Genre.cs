using Domain.Entities.Bases;

namespace Domain.Entities
{
    public class Genre : IRecord
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public List<BookGenre> BookGenres { get; set; }
    }
}

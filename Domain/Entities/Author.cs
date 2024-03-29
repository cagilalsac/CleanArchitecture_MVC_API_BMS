using Domain.Entities.Bases;

namespace Domain.Entities
{
    public class Author : Record
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;

        public List<Book>? Books { get; set; }
    }
}
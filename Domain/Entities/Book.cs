﻿using Domain.Common.Entities.Bases;
using Domain.Enums;

namespace Domain.Entities
{
    public class Book : Entity
    {
        public string Name { get; set; } = null!;
        public string? Isbn { get; set; }
        public short? NumberOfPages { get; set; }
        public DateTime? PublishDate { get; set; }
        public BookTypesEnum BookType { get; set; }
        public decimal? Price { get; set; }
        public bool IsTopSeller { get; set; }

        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        public List<BookGenre>? BookGenres { get; set; }
    }
}

using LibrarySystem.Entities.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Entities.Models
{
    public class Book : IEntity
    {
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int PublishedYear { get; set; }
        public string Isbn { get; set; } = null!;
        public BookGenre Genre { get; set; } = BookGenre.None;

        public ICollection<Author> Authors { get; set; } = [];
    }
}

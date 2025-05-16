using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Entities.Models
{
	public class Author : IEntity
	{
		public string Name { get; set; } = null!;
		public string Code { get; set; } = null!;

		public ICollection<Book> Books { get; set; } = [];
	}
}

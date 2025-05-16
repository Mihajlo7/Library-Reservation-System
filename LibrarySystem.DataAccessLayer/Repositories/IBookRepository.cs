using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Entities.Models;

namespace LibrarySystem.DataAccessLayer.Repositories
{
	public interface IBookRepository
	{
		public Book? GetBookById(Guid id);
		public IEnumerable<Book> GetBooks();
		public Book CreateBook(Book book);
		public Book? UpdateBook(Guid id,Book book);
		public bool DeleteBook(Guid id);
	}
}

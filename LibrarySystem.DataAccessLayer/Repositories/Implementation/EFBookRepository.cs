using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.DataAccessLayer.Persistence;
using LibrarySystem.Entities.Models;

namespace LibrarySystem.DataAccessLayer.Repositories.Implementation
{
	public sealed class EFBookRepository : IBookRepository
	{
		private readonly LibraryContext _context;
        public EFBookRepository(LibraryContext context)
        {
            _context = context;
        }
        public Book CreateBook(Book book)
		{
			_context.Books.Add(book);
			_context.SaveChanges();
			return book;
		}

		public bool DeleteBook(Guid id)
		{
			var foundBook = GetBookById(id);

			if (foundBook != null)
			{
				_context.Books.Remove(foundBook);
				_context.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}

		public Book? GetBookById(Guid id)
		{
			var foundBook = _context.Books.Find(id);
			return foundBook;
		}

		public IEnumerable<Book> GetBooks()
		{
			return _context.Books;
		}

		public Book? UpdateBook(Guid id, Book book)
		{
			
			var foundEntity = GetBookById(id);
			if (foundEntity != null)
			{
				foundEntity.Author = book.Author;
				foundEntity.Title = book.Title;
				foundEntity.PublishedYear = book.PublishedYear;
				foundEntity.Genre = book.Genre;

				_context.Entry(foundEntity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

				_context.SaveChanges();
			}

			return foundEntity;
			
		}
	}
}

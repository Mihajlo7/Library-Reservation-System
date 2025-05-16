using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.DataAccessLayer.Persistence;
using LibrarySystem.DataAccessLayer.Repositories.Implementation;
using LibrarySystem.Entities.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace LibrarySystem.Tests.DataAccessLayer.Books
{
	[TestFixture]
	internal class EFBookRepositotyTests
	{

		private LibraryContext _context;
		private EFBookRepository _bookRepository;

		[OneTimeSetUp]
		public void SetUpDatabase()
		{
			var options = new DbContextOptionsBuilder<LibraryContext>()
				.UseInMemoryDatabase(databaseName: "TestDB").Options;

			_context = new LibraryContext(options);
			_context.Database.EnsureCreated();
			_bookRepository = new EFBookRepository(_context);
		}

		[OneTimeTearDown]
		public void TearDownDatabase() 
		{
			_context.Database.EnsureDeleted();
			_context.Dispose();
		}

		[TearDown]
		public void CleanDatabase()
		{
			_context.Books.RemoveRange(_context.Books);
			_context.SaveChanges();
		}

		[Test]
		public void Create_Book_Succesfull()
		{
			var book = new Book
			{
				Id = Guid.NewGuid(),
				Title = "Test Book",
				Author = "Test Author",
				Genre = Entities.Enumerations.BookGenre.Classic,
				PublishedYear = 2020,
				Isbn = "1111-1111-1111"
			};

			var result = _bookRepository.CreateBook(book);

			Assert.That(result, Is.Not.Null);
			Assert.That(result.Id, Is.Not.Null);
			Assert.That(result.Isbn, Is.EqualTo(book.Isbn));
			Assert.That(result.Title, Is.EqualTo(book.Title));
			Assert.That(result.Author, Is.EqualTo(book.Author));
		}

		[Test]
		[Ignore("Test is ignore because InMemoryDb does not enforce Unique constraints")]
		public void Create_Book_Failed_Same_Isbn()
		{
			var book1 = new Book
			{
				Title = "Test Book",
				Author = "Test Author",
				Genre = Entities.Enumerations.BookGenre.Classic,
				PublishedYear = 2020,
				Isbn = "1111-1111-1111"
			};

			var book2 = new Book
			{
				Title = "Test Book",
				Author = "Test Author",
				Genre = Entities.Enumerations.BookGenre.Classic,
				PublishedYear = 2020,
				Isbn = "1111-1111-1111"
			};

			_bookRepository.CreateBook(book1);
			

			var ex =Assert.Throws<DbUpdateException>(() => _bookRepository.CreateBook(book2));
			Assert.That(ex, Is.Not.Null);
		}
	}
}

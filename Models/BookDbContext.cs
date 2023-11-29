using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BooksHub.Models
{
	public class BookDbContext: DbContext
	{
		public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
		{ }

		public DbSet<Book> Books { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<BookTag> BookTags { get; set; }
	}
}

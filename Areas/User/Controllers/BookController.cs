using BooksHub.Extensions;
using BooksHub.Helpers;
using BooksHub.Models;
using BooksHub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using static System.Reflection.Metadata.BlobBuilder;

namespace BooksHub.Controllers
{
	public class BookController : Controller
	{
		private readonly BookDbContext bookDbContext;
		public BookController(BookDbContext bookDbContext)
		{
			this.bookDbContext = bookDbContext;
            
        }
        [HttpGet]
        public IActionResult Index(int? categoryId = null, int? tagId = null, int page = 1)
        {

            var books = bookDbContext.Books.Include(x => x.BookTags).ThenInclude(x => x.Tag).Include(x => x.Category).OrderByDescending(x => x.Id);

            if (tagId != null && categoryId != null)
            {
                books = (IOrderedQueryable<Book>)books.Where(x => x.BookTags.Any(x => x.TagId == tagId));
                books = (IOrderedQueryable<Book>)books.Where(x => x.CategoryId == categoryId);
            }

            else if (categoryId != null)
            {
                books = (IOrderedQueryable<Book>)books.Where(x => x.CategoryId == categoryId);
            }

            else if (tagId != null)
            {
                books = (IOrderedQueryable<Book>)books.Where(x => x.BookTags.Any(x => x.TagId == tagId));
            }
            var model = new IndexViewModel();

            int totalP = (int)Math.Ceiling(books.Count() / (double)model.LimitPage);
            books = (IOrderedQueryable<Book>)books.Skip((page - 1) * model.LimitPage).Take(model.LimitPage);


            model.Categories = bookDbContext.Categories;
            model.Books = books;
            model.Tags = bookDbContext.Tags;
            model.RecentBooks = bookDbContext.Books.OrderByDescending(x => x.Id).Take(model.LimitPage);
            model.CurrentPages = page;
            model.TotalPages = totalP;
            model.SelectedCategoryId = categoryId;
            model.SelectedTagId = tagId;
            

            return View(model);
        }
                

        [HttpGet]
        public IActionResult Details(int id)
        {
            var book = bookDbContext.Books
                .Include(x => x.BookTags).ThenInclude(x => x.Tag)
                .Include(x => x.Category)
                .FirstOrDefault(book => book.Id == id);
            return View(book);
        }
    }
}

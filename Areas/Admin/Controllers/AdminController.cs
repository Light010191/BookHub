using BooksHub.Extensions;
using BooksHub.Helpers;
using BooksHub.Models;
using BooksHub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BooksHub.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly BookDbContext bookDbContext;
        public AdminController(BookDbContext bookDbContext)
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
        public IActionResult Add()
        {
            ViewBag.categories = new SelectList(bookDbContext.Categories, "Id", "Name");
            ViewBag.tags = new MultiSelectList(bookDbContext.Tags, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Book book, IFormFile Image, int[] tags)
        {
            book.PosterUrl = await FileUploadHelper.UploadAsync(Image);
            if (Image != null)
            {
                var filename = $"{Guid.NewGuid()}{Path.GetExtension(Image.FileName)}";
                using var fs = new FileStream(@$"wwwroot/uploads/{filename}", FileMode.Create);
                await Image.CopyToAsync(fs);
                book.PosterUrl = @$"/uploads/{filename}";

            }

            TempData["status"] = "New post added!";
            book.CreatedAt = DateTime.Now;
            await bookDbContext.Books.AddAsync(book);
            await bookDbContext.SaveChangesAsync();


            bookDbContext.BookTags.AddRange(tags.Select(x => new BookTag { BookId = book.Id, TagId = x }));

            await bookDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var books = bookDbContext.Books.Find(id);

            ViewBag.categories = new SelectList(bookDbContext.Categories, "Id", "Name");

            var selectedTagIds = bookDbContext.BookTags.Where(x => x.BookId == id).Select(x => x.TagId);
            ViewBag.tags = new MultiSelectList(bookDbContext.Tags, "Id", "Name", selectedTagIds);

            return View(books);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book book, IFormFile Image, int[] tags)
        {

            if (Image != null)
            {
                var path = await FileUploadHelper.UploadAsync(Image);
                book.PosterUrl = path;
            }

            book.CreatedAt = DateTime.Now;

            bookDbContext.Books.Update(book);
            await bookDbContext.SaveChangesAsync();


            var bookWithTags = bookDbContext.Books.Include(x => x.BookTags).FirstOrDefault(x => x.Id == book.Id);

            bookDbContext.UpdateManyToMany(
                bookWithTags.BookTags,
                tags.Select(x => new BookTag { BookId = book.Id, TagId = x }),
                x => x.TagId
                );
            await bookDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
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

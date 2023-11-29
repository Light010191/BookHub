using BooksHub.Models;

namespace BooksHub.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Book> Books { get; set;}
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<Book> RecentBooks { get; set; }
        public int CurrentPages { get; set; }
        public int? SelectedCategoryId { get; set; }
        public int? SelectedTagId { get; set; }
        public int TotalPages { get; set; }
        public int LimitPage { get; set; } = 3;

    }
}

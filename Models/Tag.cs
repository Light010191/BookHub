namespace BooksHub.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<BookTag> BookTags { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BooksHub.Models
{
	public class Book
	{
		public int Id { get; set; }
		[Required (ErrorMessage ="Введите название!")]
		[MaxLength(50)]
		public string Title { get; set; }
        [Required(ErrorMessage = "Введите описание!")]
        [MaxLength(200)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Укажите автора книги!")]
        [MaxLength(50)]
        public string Author { get; set; }
        [Required(ErrorMessage = "Укажите адрес картинки!")]
        [MaxLength(200)]
        public string PosterUrl { get; set; }
        [Display(Name = "Date of create")]
        [DataType(DataType.Date)]        
		public DateTime? CreatedAt { get; set; }       
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<BookTag> BookTags { get; set; }
	}
}

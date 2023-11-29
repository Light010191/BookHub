using System.ComponentModel.DataAnnotations;

namespace BooksHub.ViewModels
{
	public class LoginViewModel
	{
		
		[Required]
		[MinLength(1)]
		[MaxLength(30)]
		public string Login { get; set; }

		[Required]
		[MinLength(1)]
		[MaxLength(30)]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public bool RememberMe { get; set; }

	}
}

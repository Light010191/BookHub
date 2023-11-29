using BooksHub.Models;

namespace BooksHub.Services
{
	public interface IUserManager
	{
		 bool Login (string userName, string password);

		 UserCrededantials GetUserCrededantials();

		 UserCrededantials CurrentUser { get; set; }

		
	}
}

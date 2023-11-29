using Microsoft.AspNetCore.Components.Web;

namespace BooksHub.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public Boolean IsAdmin { get; set; }    

    }
}

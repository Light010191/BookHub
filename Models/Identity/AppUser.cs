using Microsoft.AspNetCore.Identity;

namespace BooksHub.Models.Identity
{
    public class AppUser :IdentityUser
    {
        public string FullName { get; set; }
        public int Age { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BooksHub.Models.Identity;

namespace BooksHub.Models
{
    public class UserDbContext : IdentityDbContext<AppUser/*, IdentityRole, string*/>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }


        public DbSet<User> users { get; set; }
    }
}

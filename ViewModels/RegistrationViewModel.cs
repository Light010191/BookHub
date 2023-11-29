using System.ComponentModel.DataAnnotations;
namespace BooksHub.ViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string RepeatPassword { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public int Year { get; set; }
       
    }
}

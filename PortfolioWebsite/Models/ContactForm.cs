using System.ComponentModel.DataAnnotations;

namespace PortfolioWebsite.Models
{
    public class ContactForm
    {
        [Required(ErrorMessage ="Name field cannot be empty!")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Email cannot be empty!")]
        [EmailAddress(ErrorMessage ="please enter the valid email!")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Message is required!!")]
        public string Message { get; set; }
    }
}

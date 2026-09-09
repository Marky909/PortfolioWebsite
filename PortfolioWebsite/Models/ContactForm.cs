using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace PortfolioWebsite.Models
{
    public class ContactForm
    {
        [ValidateNever]
        public int ContactFormId { get; set; }

        [ValidateNever]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Name field cannot be empty!")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email cannot be empty!")]
        [EmailAddress(ErrorMessage = "Please enter a valid email!")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Message is required!!")]
        public string Message { get; set; } = string.Empty;
    }
}
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RealEstateApp.Models
{
    public class RegisterModel : IValidatableObject
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(Email, emailPattern))
            {
                yield return new ValidationResult("The Email field is not a valid e-mail address.");
            }
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RealEstateApp.Models
{
    public class LoginModel : IValidatableObject
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

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

using Shared.VaildationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record UserForRegistrationDTO
    {
        [Required(ErrorMessage = "First Name is required")]
        [CapitalizedFirstLetter]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        [CapitalizedFirstLetter]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Birth day is required")]
        public DateTime BirthDay { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [NightMail]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Passwords must be the same")]
        public string ConfirmPassword { get; set; }
    }
}

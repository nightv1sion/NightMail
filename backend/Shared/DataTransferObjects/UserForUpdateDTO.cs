using Shared.VaildationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record UserForUpdateDTO
    {
        [Required(ErrorMessage = "First Name is required")]
        [CapitalizedFirstLetter]
        public string? FirstName { get; init; }
        [Required(ErrorMessage = "Last Name is required")]
        [CapitalizedFirstLetter]
        public string? LastName { get; init; }
        [Required(ErrorMessage = "Birth day is required")]
        public DateTime Birthday { get; init; }
    }
}

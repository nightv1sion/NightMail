using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record TokenDTO
    {
        public string? AccessToken { get; init; }
        public string? RefreshToken { get; init; }
        public DateTime Expiration { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public UserProfileImage? UserProfileImage { get; set; }
        public List<Mail> ReceivedMails { get; set; }
        public List<Mail> SendedMails { get; set; }
        public List<Folder> Folders { get; set; }
    }
}

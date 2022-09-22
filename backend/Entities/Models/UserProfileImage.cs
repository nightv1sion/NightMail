using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class UserProfileImage
    {
        [Key]
        public Guid UserProfilePictureId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageData { get; set; }
    }
}

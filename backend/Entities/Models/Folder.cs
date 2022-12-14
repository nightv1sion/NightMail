using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Folder
    {
        [Key]
        public Guid FolderId { get; set; }
        public string Name { get; set; }
        public List<MailFolder> MailFolders { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }

    }
}

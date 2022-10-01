using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class MailFolder
    {
        public Guid MailId { get; set; }
        public Mail Mail { get; set; }
        public Guid FolderId { get; set; }
        public Folder Folder { get; set; }
    }
}

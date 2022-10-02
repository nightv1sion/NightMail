using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Mail
    {
        [Key]
        public Guid MailId { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public DateTime CreationDateTime { get; set; }
        public List<MailFolder> MailFolders { get; set; }
        public User Sender { get; set; }
        public Guid SenderId { get; set; }
        public User Receiver { get; set; }
        public Guid ReceiverId { get; set; }
        public bool IsSpam { get; set; }
    }
}

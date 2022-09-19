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
        public Guid Id { get; set; }
        public User Receiver { get; set; }
        public User Sender { get; set; }
        public string Subject { get; set; }
        public string Topic { get; set; }
        public DateTime DateTime { get; set; }
    }
}

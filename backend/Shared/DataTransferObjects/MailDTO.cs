using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record MailDTO
    {
        public UserDTO Receiver { get; set; }
        public UserDTO Sender { get; set; }
        public string Subject { get; set; }
        public string Topic { get; set; }
        public DateTime DateTime { get; set; }
    }
}

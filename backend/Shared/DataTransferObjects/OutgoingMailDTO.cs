using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public class OutgoingMailDTO
    {
        public string Text { get; set; }
        public string Subject { get; set; }
        public string ReceiverMail { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}

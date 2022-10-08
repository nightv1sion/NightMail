using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ExceptionModels
{
    public class MailNotFoundException : NotFoundException
    {
        public MailNotFoundException(Guid mailId) : base($"Mail with id: {mailId}")
        {
        }
    }
}

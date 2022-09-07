using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ExceptionModels
{
    public class UserDoesNotExist : BadRequestException
    {
        public UserDoesNotExist(string email) : base($"User with email: {email} does not exist") {}
    }
}

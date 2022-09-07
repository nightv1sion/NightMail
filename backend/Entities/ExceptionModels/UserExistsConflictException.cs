using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ExceptionModels
{
    public class UserExistsConflictException : ConflictException
    {
        public UserExistsConflictException(string email) : base($"User with email {email} exists yet")
        {
        }
    }
}

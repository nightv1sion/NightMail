using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ExceptionModels
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string email) : base($"User with email: {email} does not exist") {}
        public UserNotFoundException(Guid id) : base($"User with id: {id} does not exist") {}
    }
}

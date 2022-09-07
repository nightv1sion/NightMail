using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ExceptionModels
{
    public class UserIncorrectPasswordNonAuthorizedException : NonAuthorizedException
    {
        public UserIncorrectPasswordNonAuthorizedException() : base("Email or password is incorrect")
        {
        }
    }
}

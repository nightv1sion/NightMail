using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ExceptionModels
{
    public class NonAuthorizedException : Exception
    {
        public NonAuthorizedException(string message) : base(message)
        {
        }
    }
}

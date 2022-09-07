using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ExceptionModels
{
    public class InvalidTokenBadRequestException : BadRequestException
    {
        public InvalidTokenBadRequestException() : base("JWT Token is invalid")
        {
        }
    }
}

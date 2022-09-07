﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ExceptionModels
{
    public class PasswordsDontMatchBadRequestException : BadRequestException
    {
        public PasswordsDontMatchBadRequestException() : base("Passwords don't match")
        {
        }
    }
}

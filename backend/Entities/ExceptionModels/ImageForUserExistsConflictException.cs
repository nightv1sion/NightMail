using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ExceptionModels
{
    public class ImageForUserExistsConflictException : ConflictException
    {
        public ImageForUserExistsConflictException(Guid userId) : base($"Profile image for user with id: {userId} exists yet")
        {
        }
    }
}

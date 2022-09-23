using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ExceptionModels
{
    public class UserProfileImageNotFoundException : NotFoundException
    {
        public UserProfileImageNotFoundException(Guid userId) : base($"Image for user with id: {userId} not found")
        {
        }
    }
}

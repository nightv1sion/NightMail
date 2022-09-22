using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserProfileImageRepository : RepositoryBase<UserProfileImage>, IUserProfileImageRepository
    {
        public UserProfileImageRepository(RepositoryContext context) : base(context)
        {
        }

        public UserProfileImage GetImageByUserId(Guid userId, bool trackChanges) => FindByCondition(img => img.UserId == userId, trackChanges)
            .FirstOrDefault();
    }
}

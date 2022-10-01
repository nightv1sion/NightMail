using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext context) : base(context)
        {
        }
        public User GetUserById(Guid id, bool trackChanges) => 
            FindByCondition(u => u.Id == id, trackChanges).FirstOrDefault();
        public User GetUserByEmail(string email, bool trackChanges) => 
            FindByCondition(u => u.Email == email, trackChanges).FirstOrDefault();
        public void UpdateUser(User user) => Update(user);

        
    }
}

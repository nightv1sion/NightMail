using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserRepository
    {
        User GetUserById(Guid id, bool trackChanges);
        void UpdateUser(User user);
        User GetUserByEmail(string email, bool trackChanges);

    }
}

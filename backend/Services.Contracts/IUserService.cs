﻿using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IUserService
    {
        UserDTO GetUserById(Guid id, bool trackChanges);
        Task UpdateUserAsync(UserForUpdateDTO userDto);
    }
}

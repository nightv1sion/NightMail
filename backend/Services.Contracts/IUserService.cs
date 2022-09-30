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
        T GetUserById<T>(Guid id, bool trackChanges);
        Task UpdateUserAsync(Guid userId, UserForUpdateDTO userDto);

    }
}

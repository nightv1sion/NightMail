using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IUserProfileImageService
    {
        UserProfileImage GetImageByUserId(Guid userId, bool trackChanges);
        Task CreateImageForUserAsync(Guid userId, IFormFile image);
        Task DeleteImageForUserAsync(Guid userId);
    }
}

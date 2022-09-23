using Contracts;
using Entities.ExceptionModels;
using Entities.Models;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserProfileImageService : IUserProfileImageService
    {
        private IRepositoryManager _repository;
        public UserProfileImageService(IRepositoryManager repository)
        {
            _repository = repository;
        }
        public UserProfileImage GetImageByUserId(Guid userId, bool trackChanges)
        {
            var user = _repository.UserRepository.GetUserById(userId, trackChanges);
            if (user == null)
                throw new UserNotFoundException(userId);

            var img = _repository.UserProfileImageRepository.GetImageByUserId(userId, trackChanges);

            if (img == null)
                throw new UserProfileImageNotFoundException(userId);

            return img;
        }
    }
}

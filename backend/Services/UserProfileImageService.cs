using Contracts;
using Entities.ExceptionModels;
using Entities.Models;
using Microsoft.AspNetCore.Http;
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
            var user = GetUserAndCheckIfItExists(userId, trackChanges);

            var img = _repository.UserProfileImage.GetImageByUserId(userId, trackChanges);

            if (img == null)
                throw new UserProfileImageNotFoundException(userId);

            return img;
        }

        public async Task CreateImageForUserAsync(Guid userId, IFormFile image)
        {

            var user = GetUserAndCheckIfItExists(userId, false);

            var imgEntity = _repository.UserProfileImage.GetImageByUserId(userId, false);

            if (imgEntity != null)
                throw new ImageForUserExistsConflictException(userId);

            UserProfileImage newImage = new UserProfileImage() { ImageName = image.FileName, UserId = userId};

            using (var stream = new MemoryStream())
            {
                image.CopyTo(stream);
                newImage.ImageData = stream.ToArray();
            }

            _repository.UserProfileImage.CreateImageForUser(newImage);
            await _repository.SaveAsync();
        }

        public async Task DeleteImageForUserAsync(Guid userId)
        {
            var user = GetUserAndCheckIfItExists(userId, false);

            var image = _repository.UserProfileImage.GetImageByUserId(userId, false);
            if (image == null)
                throw new UserProfileImageNotFoundException(userId);

            _repository.UserProfileImage.DeleteImageForUser(image);

            await _repository.SaveAsync();
        }

        private User GetUserAndCheckIfItExists(Guid userId, bool trackChanges)
        {
            var user = _repository.User.GetUserById(userId, trackChanges);
            if (user == null)
                throw new UserNotFoundException(userId);
            
            return user;
        }

        
    }
}

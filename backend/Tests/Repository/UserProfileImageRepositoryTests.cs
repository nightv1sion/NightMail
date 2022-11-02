using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Repository
{
    public class UserProfileImageRepositoryTests
    {
        private readonly IRepositoryManager _repository;
        private readonly Guid _userId;
        public UserProfileImageRepositoryTests()
        {
            var contextOptions = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _userId = Guid.NewGuid();
            var context = new RepositoryContext(contextOptions);
            SeedData(context);
            _repository = new RepositoryManager(context);
        }
        private void SeedData(RepositoryContext context)
        {
            var user1 = new User() { Id = _userId, FirstName = "Danila", LastName = "Uprivanov" };
            var user2 = new User() { Id = Guid.NewGuid(), FirstName = "Danila", LastName = "Uprivanov" };
            var user3 = new User() { Id = Guid.NewGuid(), FirstName = "Danila", LastName = "Uprivanov" };

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write("Some content for test");
            writer.Flush();
            var image1 = new UserProfileImage() { UserId = user1.Id, 
                UserProfilePictureId = Guid.NewGuid(),
                ImageData = stream.ToArray(),
                ImageName = "SomeFile1"};

            var image2 = new UserProfileImage()
            {
                UserId = user2.Id,
                UserProfilePictureId = Guid.NewGuid(),
                ImageData = stream.ToArray(),
                ImageName = "SomeFile2"
            };

            var image3 = new UserProfileImage()
            {
                UserId = user3.Id,
                UserProfilePictureId = Guid.NewGuid(),
                ImageData = stream.ToArray(),
                ImageName = "SomeFile3"
            };

            context.UserProfileImages.AddRange(image1, image2, image3);

            context.SaveChanges();
        }
        [Fact]
        public async Task UserProfileImageRepository_GetImageByUserId_ReturnsUserProfileImage()
        {
            // Arrange
            var expectedImageName = "SomeFile1";
            // Act
            var result = _repository.UserProfileImage.GetImageByUserId(_userId, false);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserProfileImage>(result);
            Assert.Equal(expectedImageName, result.ImageName);
        }
        [Fact]
        public async Task UserProfileImageRepository_CreateImageForUser_CreatesImage()
        {
            // Arrange
            var newUserId = Guid.NewGuid();
            var newImageName = "NewImage";
            var user = new User() { Id = newUserId, FirstName = "Danila", LastName = "Uprivanov" };

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write("Some content for test");
            writer.Flush();
            var image1 = new UserProfileImage()
            {
                UserId = user.Id,
                UserProfilePictureId = Guid.NewGuid(),
                ImageData = stream.ToArray(),
                ImageName = newImageName
            };
            // Act
            _repository.UserProfileImage.CreateImageForUser(image1);
            await _repository.SaveAsync();
            var result = _repository.UserProfileImage.GetImageByUserId(newUserId, false);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserProfileImage>(result);
            Assert.Equal(newImageName, result.ImageName);
        }
        [Fact]
        public async Task UserProfileImageRepository_DeleteImageForUser_DeletesImage()
        {
            // Arrange
            var image = _repository.UserProfileImage.GetImageByUserId(_userId, true);

            // Act
            _repository.UserProfileImage.DeleteImageForUser(image);
            await _repository.SaveAsync();
            var result = _repository.UserProfileImage.GetImageByUserId(_userId, false);

            // Assert
            Assert.Null(result);
        }
    }
}

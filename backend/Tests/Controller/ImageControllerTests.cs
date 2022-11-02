using backend.Controllers;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Controller
{
    public class ImageControllerTests
    {
        private readonly Mock<IServiceManager> _service;
        private readonly ImageController _controller;
        public ImageControllerTests()
        {
            _service = new Mock<IServiceManager>();
            _controller = new ImageController(_service.Object);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", Guid.NewGuid().ToString())
                }))
            };
        }
        [Fact]
        public async Task ImageController_GetImageForUser_ReturnsFileContentResult()
        {
            // Arrange
            var img = new UserProfileImage()
            {
                ImageData = Encoding.UTF8.GetBytes("SuperImage"),
                ImageName = "SomeImage"
            };
            _service.Setup(s => s.UserProfileImageService
                .GetImageByUserId(It.IsAny<Guid>(), false))
                .Returns(img);

            // Act
            var result = await _controller.GetImageForUser();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<FileContentResult>(result);
        }
        [Fact]
        public async Task ImageController_PostImageForUser_ReturnsOkResult()
        {
            // Arrange
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write("Some content for testing");
            writer.Flush();
            stream.Position = 0;
            var fileName = "test.txt";

            IFormFile image = new FormFile(stream, 0, 0, "from_form", fileName);
            _service.Setup(s => s.UserProfileImageService
                .CreateImageForUserAsync(It.IsAny<Guid>(), image));
            // Act
            var result = await _controller.PostImageForUser(image);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task ImageController_DeleteImageForUser_ReturnsOkResult()
        {
            // Arrange
            _service.Setup(s => s.UserProfileImageService.DeleteImageForUserAsync(It.IsAny<Guid>()));

            // Act
            var result = await _controller.DeleteImageForUser();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
    }
}

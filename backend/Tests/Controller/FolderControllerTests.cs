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
    public class FolderControllerTests
    {
        private readonly Mock<IServiceManager> _service;
        private readonly FolderController _controller;
        public FolderControllerTests()
        {
            _service = new Mock<IServiceManager>();
            _controller = new FolderController(_service.Object);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", Guid.NewGuid().ToString())
                }))
            };

        }
        [Fact]
        public async Task FolderController_GetFolders_ReturnsOkObjectResult()
        {
            // Arrange
            var folders = new List<Folder>();
            _service.Setup(s => s.FolderService.GetFoldersAsync(It.IsAny<Guid>(), false));

            // Act
            var result = await _controller.GetFolders();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task FolderController_CreateFolder_ReturnsOkResult()
        {
            // Arrange
            var folders = new List<Folder>();
            var name = "SomeName";
            _service.Setup(s => s.FolderService.CreateFolderAsync(It.IsAny<Guid>(), name));

            // Act
            var result = await _controller.CreateFolder(name);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task FolderController_DeleteFolder_ReturnsOkResult()
        {
            // Arrange
            var folderId = Guid.NewGuid();
            _service.Setup(s => s.FolderService.DeleteFolderAsync  (It.IsAny<Guid>(), folderId));

            // Act
            var result = await _controller.DeleteFolder(folderId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task FolderController_UpdateFolder_ReturnsOkResult()
        {
            // Arrange
            var folderId = Guid.NewGuid();
            var newName = "SomeNewName";
            _service.Setup(s => s.FolderService.UpdateFolderAsync(It.IsAny<Guid>(), folderId, newName));

            // Act
            var result = await _controller.UpdateFolder(folderId, newName);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task FolderController_IncludeMail_ReturnsOkResult()
        {
            // Arrange
            var folderId = Guid.NewGuid();
            var mailId = Guid.NewGuid();
            var newName = "SomeNewName";
            _service.Setup(s => s.FolderService.IncludeMailAsync(It.IsAny<Guid>(), folderId, mailId));

            // Act
            var result = await _controller.IncludeMail(folderId, mailId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
    }
}

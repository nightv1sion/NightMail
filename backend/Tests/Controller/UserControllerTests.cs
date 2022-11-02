using backend.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Controller
{
    public class UserControllerTests
    {
        private readonly Mock<IServiceManager> _service;
        private readonly UserController _controller;
        public UserControllerTests()
        {
            _service = new Mock<IServiceManager>();
            _controller = new UserController(_service.Object);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", Guid.NewGuid().ToString())
                }))
            };
        }
        [Fact]
        public async Task UserController_GetUser_ReturnsOkObjectResult() 
        {
            // Arrange
            var user = new UserDTO();
            _service.Setup(s => s.UserService.GetUserById<UserDTO>(It.IsAny<Guid>(), false))
                .Returns(user);

            // Act
            var result = await _controller.GetUser();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UserController_GetUserForEdit_ReturnsOkObjectResult()
        {
            // Arrange
            var user = new UserForUpdateDTO();
            _service.Setup(s => s.UserService.GetUserById<UserForUpdateDTO>(It.IsAny<Guid>(), false))
                .Returns(user);

            // Act
            var result = await _controller.GetUserForEdit();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UserController_UpdateUser_ReturnsOktResult()
        {
            // Arrange
            var userDto = new UserForUpdateDTO();
            _service.Setup(s => s.UserService.UpdateUserAsync(It.IsAny<Guid>(), userDto));

            // Act
            var result = await _controller.UpdateUser(userDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
    }
}

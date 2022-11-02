using backend.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class AuthControllerTests
    {
        private readonly AuthController _controller;
        private readonly Mock<IServiceManager> _service;
        private readonly Mock mock;
        public AuthControllerTests()
        {
            _service = new Mock<IServiceManager>();
            _controller = new AuthController(_service.Object);
        }
        [Fact]
        public async Task AuthController_RegisterUser_ReturnsBadRequestObjectResult_IfModelStateIsNotValid()
        {
            // Arrange
            var user = new UserForRegistrationDTO();

            // Act
            _controller.ModelState.AddModelError("someerror", "someerror");
            var result = await _controller.RegisterUser(user);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task AuthController_RegisterUser_ReturnsBadRequestObjectResult_IfIdentityResultIsFailed()
        {
            // Arrange
            var user = new UserForRegistrationDTO();

            // Act
            _service.Setup(s => s.AuthenticationService.RegisterUserAsync(user))
                .ReturnsAsync(IdentityResult.Failed());
            var result = await _controller.RegisterUser(user);
            
            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task AuthController_RegisterUser_ReturnsOkResult_IfIdentityResultIsSuccess()
        {
            // Arrange
            var user = new UserForRegistrationDTO();

            // Act
            _service.Setup(s => s.AuthenticationService.RegisterUserAsync(user))
                .ReturnsAsync(IdentityResult.Success);
            var result = await _controller.RegisterUser(user);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task AuthController_LoginUser_ReturnsBadRequestObjectResult_IfModelStateIsNotValid()
        {
            // Arrange
            var user = new UserForLoginDTO();

            // Act
            _controller.ModelState.AddModelError("SomeError", "someError");
            var result = await _controller.LoginUser(user);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task AuthController_LoginUser_ReturnsOkObjectResult_IfModelStateIsValid()
        {
            // Arrange
            var user = new UserForLoginDTO();
            var token = new TokenDTO();
            // Act
            _service.Setup(s => s.AuthenticationService.LoginUserAsync(user))
                .ReturnsAsync(token);
            var result = await _controller.LoginUser(user);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task AuthController_RefreshToken_ReturnsOkObjectResult()
        {
            // Arrange
            var token = new TokenDTO();
            var newToken = new TokenDTO();
            // Act
            _service.Setup(s => s.AuthenticationService.RefreshTokenAsync(token))
                .ReturnsAsync(token);
            var result = await _controller.RefreshToken(token);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task AuthController_Revoke_ReturnsNoContentResult()
        {
            // Arrange
            var email = "some@mail.ru";
            // Act
            _service.Setup(s => s.AuthenticationService.RevokeUserAsync(email))
                .Returns(Task.CompletedTask);
            var result = await _controller.Revoke(email);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task AuthController_RevokeAll_ReturnsNoContentResult()
        {
            // Arrange
            // Act
            _service.Setup(s => s.AuthenticationService.RevokeAllAsync())
                .Returns(Task.CompletedTask);
            var result = await _controller.RevokeAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task AuthController_CheckPassword_ReturnsOkResult_IfIdentityResultIsSuccess()
        {
            // Arrange
            var password = "SomePassword";
            Guid userId = new Guid();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim("UserId", userId.ToString()) 
                    }))
            };

            // Act
            _service.Setup(s => s.AuthenticationService.ConfirmPasswordAsync(userId, password))
                .ReturnsAsync(true);
            var result = await _controller.CheckPassword(password);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task AuthController_CheckPassword_ReturnsUnauthorizedResult_IfIdentityResultIsFailed()
        {
            // Arrange
            var password = "SomePassword";
            Guid userId = new Guid();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim("UserId", userId.ToString())
                    }))
            };

            // Act
            _service.Setup(s => s.AuthenticationService.ConfirmPasswordAsync(userId, password))
                .ReturnsAsync(false);
            var result = await _controller.CheckPassword(password);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}

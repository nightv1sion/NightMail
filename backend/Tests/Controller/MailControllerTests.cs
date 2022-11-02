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
using System.Web.Http.Results;

namespace Tests.Controller
{
    public class MailControllerTests
    {
        private readonly Mock<IServiceManager> _service;
        private readonly MailController _controller;
        public MailControllerTests()
        {
            _service = new Mock<IServiceManager>();
            _controller = new MailController(_service.Object);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", Guid.NewGuid().ToString())
                }))
            };
        }
        [Fact]
        public async Task MailController_CreateMail_ReturnsOkResult()
        {
            // Arrange 
            var mailDto = new MailForCreateDTO();
            _service.Setup(s => s.MailService.CreateMailAsync(It.IsAny<Guid>(), mailDto));

            // Act
            var result = await _controller.CreateMail(mailDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Microsoft.AspNetCore.Mvc.OkResult>(result);
        }
        [Fact]
        public async Task MailController_GetIncomingMails_ReturnsOkObjectResult()
        {
            // Arrange 
            _service.Setup(s => s.MailService.GetIncomingMailsAsync(It.IsAny<Guid>(), false));

            // Act
            var result = await _controller.GetIncomingMails();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task MailController_GetOutcomingMails_ReturnsOkObjectResult()
        {
            // Arrange 
            _service.Setup(s => s.MailService.GetOutgoingMailsAsync(It.IsAny<Guid>(), false));

            // Act
            var result = await _controller.GetOutgoingMails();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task MailController_GetMailsFromCreatedFolder_ReturnsOkObjectResult()
        {
            // Arrange 
            var folderId = Guid.NewGuid();
            _service.Setup(s => s.MailService.GetMailsForFolderAsync(It.IsAny<Guid>(), folderId, false));

            // Act
            var result = await _controller.GetMailsFromCreatedFolder(folderId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}

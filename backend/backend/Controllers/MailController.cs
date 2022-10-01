using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObjects;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IServiceManager _service;

        public MailController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> CreateMail(MailDTO mailDto)
        {
            Guid userId = GetUserId();
            await _service.MailService.CreateMailAsync(userId, mailDto);

            return Ok(); 
        }

        [HttpGet("incoming")]
        public async Task<ActionResult> GetIncomingMails()
        {
            Guid userId = GetUserId();
            var mails = await _service.MailService.GetIncomingMailsAsync(userId, false);

            return Ok(mails);
        }

        [HttpGet("outgoing")]
        public async Task<ActionResult> GetOutgoingMails()
        {
            Guid userId = GetUserId();
            var mails = await _service.MailService.GetOutgoingMailsAsync(userId, false);

            return Ok(mails);
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public MailController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }
    }
}

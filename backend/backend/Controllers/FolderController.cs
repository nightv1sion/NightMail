using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FolderController : ControllerBase
    {
        private readonly IServiceManager _service;

        public FolderController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetFoldersForUser()
        {
            var userId = GetUserId();
            var folders = await _service.FolderService.GetFoldersForUserAsync(userId, false);

            return Ok(folders);
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
        }
    }
}

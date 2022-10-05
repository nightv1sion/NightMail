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

        [HttpGet("all")]
        public async Task<ActionResult> GetFolders()
        {
            var userId = GetUserId();
            var folders = await _service.FolderService.GetFoldersAsync(userId, false);

            return Ok(folders);
        }

        [HttpPost]
        public async Task<ActionResult> CreateFolder([FromBody] string name)
        {
            var userId = GetUserId();
            await _service.FolderService.CreateFolderAsync(userId, name);

            return Ok();
        }

        [HttpDelete("{folderId}")]
        public async Task<ActionResult> DeleteFolder(Guid folderId)
        {
            var userId = GetUserId();
            await _service.FolderService.DeleteFolderAsync(userId, folderId);

            return Ok();
        }

        [HttpPut("{folderId}")]
        public async Task<ActionResult> UpdateFolder(Guid folderId, [FromBody] string newName)
        {
            var userId = GetUserId();
            await _service.FolderService.UpdateFolderAsync(userId, folderId, newName);

            return Ok();
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IServiceManager _service;

        public ImageController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet("user")]
        public async Task<ActionResult> GetImageForUser()
        {
            Guid userId = Guid.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value);
            
            var img = _service.UserProfileImageService.GetImageByUserId(userId, false);
            
            return File(img.ImageData, "application/octet-stream", img.ImageName);
        }

    }
}

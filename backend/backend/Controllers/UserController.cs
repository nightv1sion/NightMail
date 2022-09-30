using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObjects;

namespace backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IServiceManager _service;
        public UserController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetUser()
        {
            Guid userId = GetCurrentUserId();
            var user = _service.UserService.GetUserById<UserDTO>(userId, false);
            return Ok(user);
        }

        [HttpGet("for-edit")]
        public async Task<ActionResult> GetUserForEdit()
        {
            Guid userId = GetCurrentUserId();
            var user = _service.UserService.GetUserById<UserForUpdateDTO>(userId, false);
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(UserForUpdateDTO userDto)
        {
            Guid userId = GetCurrentUserId();
            await _service.UserService.UpdateUserAsync(userId, userDto);

            return Ok();
        }

        private Guid GetCurrentUserId()
        {
            return Guid.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value);
        }
    }
}

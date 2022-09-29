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
            Guid userId = Guid.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value);
            var user = _service.UserService.GetUserById<UserDTO>(userId, false);
            return Ok(user);
        }

        [HttpGet("for-edit")]
        public async Task<ActionResult> GetUserForEdit()
        {
            Guid userId = Guid.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value);
            var user = _service.UserService.GetUserById<UserForEditDTO>(userId, false);
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromForm]UserForUpdateDTO userFromForm)
        {
            await _service.UserService.UpdateUserAsync(userFromForm);

            return Ok();
        }
    }
}

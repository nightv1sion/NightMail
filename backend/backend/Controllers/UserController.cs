using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObjects;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IServiceManager _service;
        public UserController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetUser(Guid id)
        {
            var user = _service.UserService.GetUserById(id, false);
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

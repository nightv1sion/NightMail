using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObjects;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public AuthController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _serviceManager.AuthenticationService.RegisterUserAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Register User", error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok();
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserForLoginDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest(modelState: ModelState);

            var token = await _serviceManager.AuthenticationService.LoginUserAsync(user);
            return Ok(token);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenDTO token)
        {
            var newToken = await _serviceManager.AuthenticationService.RefreshTokenAsync(token);
            return Ok(newToken);
        }

        [HttpPost("revoke/{email}")]
        public async Task<IActionResult> Revoke(string email)
        {
            await _serviceManager.AuthenticationService.RevokeUserAsync(email);

            return NoContent();
        }

        [HttpPost("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            await _serviceManager.AuthenticationService.RevokeAllAsync();

            return NoContent();
        }
        [Authorize]
        [HttpPost("confirm-user")]
        public async Task<IActionResult> CheckPassword([FromBody]string password)
        {
            Guid userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);

            bool result = await _serviceManager.AuthenticationService.ConfirmPasswordAsync(userId, password);
            if (result)
                return Ok();
            else
                return Unauthorized();
        } 
    }
}

using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUserAsync(UserForRegistrationDTO userDto);
        Task<TokenDTO> LoginUserAsync(UserForLoginDTO userDto);
        Task RevokeUserAsync(string username);
        Task RevokeAllAsync();
        Task<TokenDTO> RefreshTokenAsync(TokenDTO token);
        Task<bool> ConfirmPasswordAsync(Guid id, string password);
    }
}

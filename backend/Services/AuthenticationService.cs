using AutoMapper;
using Entities.ExceptionModels;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Services.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthenticationService(UserManager<User> userManager, IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<IdentityResult> RegisterUserAsync(UserForRegistrationDTO userDto)
        {
            var user = await _userManager.FindByEmailAsync(userDto.Email);
            if (user != null)
                throw new UserExistsConflictException(user.Email);

            User userEntity = _mapper.Map<User>(userDto);
            var status = await _userManager.CreateAsync(userEntity, userDto.Password);
            return status;
        }

        public async Task<TokenDTO> LoginUserAsync(UserForLoginDTO userDto)
        {
            var user = await _userManager.FindByEmailAsync(userDto.Email);
            if (user == null)
                throw new UserNotFoundException(userDto.Email);

            if(await _userManager.CheckPasswordAsync(user, userDto.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim("UserId", user.Id.ToString()),
                };

                var token = GetToken(authClaims);
                var refreshToken = GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                if (userDto.RememberMe)
                    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(int.Parse(_configuration["JWT:RefreshTokenValidityInDays"]));
                else
                    user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(60);

                await _userManager.UpdateAsync(user);

                return new TokenDTO { AccessToken = new JwtSecurityTokenHandler().WriteToken(token), RefreshToken = refreshToken, Expiration = token.ValidTo };
            }

            throw new UserIncorrectPasswordNonAuthorizedException();
        }

        public async Task<TokenDTO> RefreshTokenAsync(TokenDTO token)
        {
            if (token is null)
                throw new InvalidTokenBadRequestException();

            string? accessToken = token.AccessToken;
            string? refreshToken = token.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
                throw new InvalidTokenBadRequestException();

            string email = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            if (string.IsNullOrEmpty(email))
                throw new InvalidTokenBadRequestException();

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new InvalidTokenBadRequestException();

            var newAccessToken = GetToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(int.Parse(_configuration["JWT:RefreshTokenValidityInDays"]));
            await _userManager.UpdateAsync(user);

            return new TokenDTO { AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken), RefreshToken = newRefreshToken, Expiration = newAccessToken.ValidTo };
        }

        public async Task RevokeUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new UserNotFoundException(email);

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = default;
            await _userManager.UpdateAsync(user);
        }

        public async Task RevokeAllAsync()
        {
            foreach(var user in _userManager.Users.ToList())
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiryTime = default;
                await _userManager.UpdateAsync(user);
            }
        }
        private JwtSecurityToken GetToken(List<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT").GetSection("Secret").Value));
            var expiresAt = DateTime.Now.AddMinutes(int.Parse(_configuration["JWT:TokenValidityInMinutes"]));
            
            var token = new JwtSecurityToken(issuer: _configuration["JWT:validIssuer"],
                audience: _configuration["JWT:validAudience"],
                expires: expiresAt,
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return token;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, 
                out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid Token");

            return principal;
        }
    }
}
